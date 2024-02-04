namespace XCore.Common.Data.Entity;

/// <summary>
///     The entity deletion tracker.
/// </summary>
public interface IEntityDeletionTracker : IEntityId
{
    /// <summary>
    ///     Gets or sets the deleted at.
    /// </summary>
    /// <remarks>
    ///     The date and time the entity was deleted.
    /// </remarks>
    DateTime? DeletedAt { get; set; }

    /// <summary>
    ///     Gets or sets the deleted by.
    /// </summary>
    /// <remarks>
    ///     The user who deleted the entity.
    /// </remarks>
    string? DeletedBy { get; set; }
}