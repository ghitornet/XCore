namespace XCore.Common.Data.Entity;

/// <summary>
///     The entity import tracking.
/// </summary>
/// <remarks>
///     This interface is used to mark an entity with import tracking.
/// </remarks>
public interface IEntityImportTracking : IEntity
{
    /// <summary>
    ///     Gets or sets the last imported at.
    /// </summary>
    /// <remarks>
    ///     The last imported at is the date and time the entity was last imported.
    /// </remarks>
    DateTime? LastImportedAt { get; set; }

    /// <summary>
    ///     Gets or sets the last imported by.
    /// </summary>
    /// <remarks>
    ///     The last imported by is the user who last imported the entity.
    /// </remarks>
    string? LastImportedBy { get; set; }
}