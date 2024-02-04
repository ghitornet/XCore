# XCore
XCore is a project that contains libraries to better manage Entity Franework.

[![XCore](logo.png)](https://github.com/ghitornet/XCore)
XCore By GhitorNET

# HOW TO INSTALL

You can install the package via nuget package manager. Just search for XCore and install it.

```bash
dotnet add package XCore.Common.Data
```

# HOW TO USE

The advice is to create one or more base classes to identify the properties that your entities should have.

```csharp
public class EntityBase : IEntityId, IEntityModificationTracker, IEntityDeletionTracker, IEntityImportTracking,
    IEntityExportTracking
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public Guid RowGuid { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? LastModifiedAt { get; set; }

    public string? LastModifiedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public bool IsReadyToExport { get; set; } = false;

    public DateTime? LastExportedAt { get; set; }

    public string? LastExportedBy { get; set; }

    public DateTime? LastImportedAt { get; set; }

    public string? LastImportedBy { get; set; }
}
```

Then you can create your entities by inheriting from the base class.
Don't forget to add the context attribute that binds the entity to the data context you're going to create.

```csharp
[Context("DataContext")]
public class Person : EntityBase
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public DateTime BirthDate { get; set; }
}
```

Create a data context that derives from the BaseDbContext base class.

```csharp
public class DataContext : BaseDbContext
{
	public DataContext(DbContextOptions<DataContext> options, ILoggerFactory loggerFactory) 
    : base(options, loggerFactory.CreateLogger<BaseDbContext>())
	{
	}
}
```

Save data using BaseDbContext:

If the entity derives from the IEntityExportTracking interface, when saving the file, you can automatically set the IsReadyToExport property.
```csharp
public class PersonService
{
	private readonly DataContext _context;

	public PersonService(DataContext context)
	{
		_context = context;
	}

	public async Task<Person> CreatePerson(Person person)
	{
		_context.Persons.Add(person);
        var setEntityReadyToExport = true;
		await _context.SaveAllChangesAsync(setEntityReadyToExport);
		return person;
	}
}
```

If you want to save a piece of data to the database that has been imported, you can use the SaveImport methods. In this case it is possible to decide whether to import the data in case it is already marked to be exported.

```csharp
public class PersonService
{
	private readonly DataContext _context;

	public PersonService(DataContext context)
	{
		_context = context;
	}

	public async Task<Person> ImportPerson(Person person)
	{
		_context.Persons.Add(person);
		var ignoreExportTracking = true;
		await _context.SaveImportChangesAsync(ignoreExportTracking);
		return person;
	}
}
```