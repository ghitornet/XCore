namespace XCore.Common.Data.Entity.Tests;

/// <summary>
///     The entity base.
/// </summary>
[Index(nameof(RowGuid), IsUnique = true)]
public class EntityBase : IEntityId, IEntityModificationTracker, IEntityDeletionTracker, IEntityImportTracking,
    IEntityExportTracking
{
    /// <inheritdoc />
    public DateTime? DeletedAt { get; set; }

    /// <inheritdoc />
    public string? DeletedBy { get; set; }

    /// <inheritdoc />
    public bool IsReadyToExport { get; set; } = false;

    /// <inheritdoc />
    public DateTime? LastExportedAt { get; set; }

    /// <inheritdoc />
    public string? LastExportedBy { get; set; }

    /// <inheritdoc />
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <inheritdoc />

    [Required]
    public Guid RowGuid { get; set; }

    /// <inheritdoc />
    public DateTime? LastImportedAt { get; set; }

    /// <inheritdoc />
    public string? LastImportedBy { get; set; }

    /// <inheritdoc />

    [Required]
    public DateTime CreatedAt { get; set; }

    /// <inheritdoc />
    public string? CreatedBy { get; set; }

    /// <inheritdoc />
    public DateTime? LastModifiedAt { get; set; }

    /// <inheritdoc />
    public string? LastModifiedBy { get; set; }
}