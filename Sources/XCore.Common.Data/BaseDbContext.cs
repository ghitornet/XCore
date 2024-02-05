namespace XCore.Common.Data;

/// <summary>
///     The base db context.
/// </summary>
/// <remarks>
///     This class is used to provide a base db context for all the other db contexts.
/// </remarks>
/// <remarks>
///     Initializes a new instance of the <see cref="BaseDbContext" /> class.
/// </remarks>
/// <param name="contextOptions">The context options.</param>
/// <param name="logger">The system logger.</param>
public abstract partial class BaseDbContext(DbContextOptions contextOptions, ILogger<BaseDbContext> logger)
    : DbContext(contextOptions)
{
    /// <summary>
    ///     Ons the model creating.
    /// </summary>
    /// <remarks>
    ///     This method is used to configure all entities that derive from the <see cref="IEntity" /> interface and that
    ///     contain the <see cref="ContextAttribute" /> defined for the specific <see cref="DbContext" />.
    ///     The entity configuration is loaded by searching for all classes that contain
    ///     <see cref="IEntityTypeConfiguration{IEntity}" /> interfaces.
    ///     The method also sets the query filter for all entities that derive from the <see cref="IEntityDeletionTracker" />
    ///     interface.
    /// </remarks>
    /// <param name="modelBuilder">
    ///     The model builder.
    /// </param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        try
        {
            var entityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !string.IsNullOrWhiteSpace(x.FullName) &&
                            !x.FullName.Contains("Microsoft", StringComparison.CurrentCultureIgnoreCase))
                .SelectMany(x => x.GetTypes()).Where(type =>
                    type is { IsAbstract: false, IsClass: true, IsInterface: false } &&
                    typeof(IEntity).IsAssignableFrom(type)).Where(x =>
                    x.GetCustomAttribute<ContextAttribute>() != null &&
                    x.GetCustomAttribute<ContextAttribute>()!.ContextName == GetType().Name).ToList();

            foreach (var entityType in entityTypes.Distinct())
            {
                if (modelBuilder.Model.FindEntityType(entityType) != null) continue;

                modelBuilder.Model.AddEntityType(entityType);
                logger.LogTrace("The {Name} entity has been added to the data context.", entityType.Name);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while adding the entities to the data context.");
            throw;
        }

        try
        {
            var configurationEntityTypes = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => !string.IsNullOrWhiteSpace(x.FullName) &&
                            !x.FullName.Contains("Microsoft", StringComparison.CurrentCultureIgnoreCase))
                .SelectMany(x => x.GetTypes()).Where(type =>
                    type is { IsAbstract: false, IsClass: true, IsInterface: false })
                .Where(type => type.GetInterfaces().Any(x =>
                    x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

            foreach (var assembly in configurationEntityTypes.Select(x => x.Assembly).Distinct())
            {
                logger.LogTrace(
                    "The configurations of the entities that are in this assembly {FullName} have been loaded.", assembly.FullName);
                modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while adding the entity configurations to the data context.");
            throw;
        }

        try
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                if (typeof(IEntityDeletionTracker).IsAssignableFrom(entityType.ClrType) || entityType.BaseType != null)
                {
                    var param = Expression.Parameter(entityType.ClrType, "entity");
                    var prop = Expression.PropertyOrField(param, nameof(IEntityDeletionTracker.DeletedAt));
                    var entityNotDeleted = Expression.Lambda(Expression.Equal(prop, Expression.Constant(null)), param);

                    entityType.SetQueryFilter(entityNotDeleted);
                    logger.LogTrace(
                        "You have configured the base query to delete the upload of records marked as deleted for the entity {Name}.", entityType.Name);
                }
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while adding the query filter to the data context.");
            throw;
        }
    }

    /// <summary>
    ///     Gets the entity to export.
    /// </summary>
    /// <remarks>
    ///     This method is used to get the entities that are ready to be exported.
    /// </remarks>
    /// <returns>An IQueryable.</returns>
    public IQueryable<TEntity> GetEntityToExport<TEntity>() where TEntity : class, IEntityExportTracking
    {
        return Set<TEntity>().Where(x => x.IsReadyToExport == true);
    }

    /// <summary>
    ///     Ons the before save changes.
    /// </summary>
    public virtual void OnBeforeSaveChanges()
    {
    }

    /// <summary>
    ///     Ons the after save changes.
    /// </summary>
    /// <param name="entities">The entities.</param>
    public virtual void OnAfterSaveChanges(IEnumerable<object> entities)
    {
    }

    /// <summary>
    ///     Ons the before save changes.
    /// </summary>
    /// <returns>A list of DataHistoryEntries.</returns>
    private List<EntityEntry> BeforeSaveChanges(bool setEntityReadyToExport)
    {
        ChangeTracker.DetectChanges();
        //var auditEntries = new List<DataHistoryEntry>();
        var dateTimeNow = DateTime.UtcNow;
        var entities = ChangeTracker.Entries().ToList();
        foreach (var entry in entities)
        {
            if (entry is { State: EntityState.Added, Entity: IEntityCreationTracker })
            {
                var entity = (IEntityCreationTracker)entry.Entity;
                entity.CreatedAt = dateTimeNow;
                if (setEntityReadyToExport && entry.Entity is IEntityExportTracking entityExport)
                    entityExport.IsReadyToExport = true;
            }

            if (entry is { State: EntityState.Modified, Entity: IEntityModificationTracker })
            {
                var entity = (IEntityModificationTracker)entry.Entity;
                entity.LastModifiedAt = dateTimeNow;
                if (setEntityReadyToExport && entry.Entity is IEntityExportTracking entityExport)
                    entityExport.IsReadyToExport = true;
            }

            if (entry is { State: EntityState.Deleted, Entity: IEntityDeletionTracker })
            {
                var entity = (IEntityDeletionTracker)entry.Entity;
                entity.DeletedAt = dateTimeNow;
                entry.State = EntityState.Modified;
            }
        }

        return entities;
    }

    /// <summary>
    ///     Before the save import changes.
    /// </summary>
    /// <returns>
    ///     A list of EntityEntries.
    /// </returns>
    private List<EntityEntry> BeforeSaveImportChanges(bool ignoreExportTracking)
    {
        ChangeTracker.DetectChanges();
        //var auditEntries = new List<DataHistoryEntry>();
        var dateTimeNow = DateTime.UtcNow;
        var entities = ChangeTracker.Entries().ToList();
        foreach (var entry in entities)
        {
            if (ignoreExportTracking && entry is { Entity: IEntityExportTracking })
            {
                var entity = (IEntityExportTracking)entry.Entity;
                if (entity.IsReadyToExport)
                    entry.State = EntityState.Unchanged;
            }

            if (entry is { Entity: IEntityImportTracking })
            {
                var entity = (IEntityImportTracking)entry.Entity;
                entity.LastImportedAt = dateTimeNow;
            }

            if (entry is { State: EntityState.Added, Entity: IEntityCreationTracker })
            {
                var entity = (IEntityCreationTracker)entry.Entity;
                entity.CreatedAt = dateTimeNow;
            }

            if (entry is { State: EntityState.Modified, Entity: IEntityModificationTracker })
            {
                var entity = (IEntityModificationTracker)entry.Entity;
                entity.LastModifiedAt = dateTimeNow;
            }

            if (entry is { State: EntityState.Deleted, Entity: IEntityDeletionTracker })
            {
                var entity = (IEntityDeletionTracker)entry.Entity;
                entity.DeletedAt = dateTimeNow;
                entry.State = EntityState.Modified;
            }
        }

        return entities;
    }

    /// <summary>
    ///     Before the save export changes.
    /// </summary>
    /// <returns>
    ///     A list of EntityEntries.
    /// </returns>
    private void BeforeSaveExportChanges(IEnumerable<object> entities)
    {
        ChangeTracker.DetectChanges();
        var dateTimeNow = DateTime.UtcNow;
        foreach (var entity in entities)
            if (entity is IEntityExportTracking entityExport)
            {
                entityExport.LastExportedAt = dateTimeNow;
                entityExport.IsReadyToExport = false;
                Entry(entity).State = EntityState.Modified;
            }
    }
}