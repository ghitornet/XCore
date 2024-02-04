namespace XCore.Common.Data.Entity;

/// <summary>
///     The entity export tracking.
/// </summary>
/// <remarks>
///     This interface is used to mark an entity with export tracking.
/// </remarks>
public interface IEntityExportTracking : IEntity
{
    /// <summary>
    ///     Gets or sets a value indicating whether ready to is export.
    /// </summary>
    /// <remarks>
    ///     A value indicating whether the entity is ready to be exported.
    /// </remarks>
    bool IsReadyToExport { get; set; }

    /// <summary>
    ///     Gets or sets the last exported at.
    /// </summary>
    /// <remarks>
    ///     The date and time the entity was last exported.
    /// </remarks>
    DateTime? LastExportedAt { get; set; }

    /// <summary>
    ///     Gets or sets the last exported by.
    /// </summary>
    /// <remarks>
    ///     The user who last exported the entity.
    /// </remarks>
    string? LastExportedBy { get; set; }
}