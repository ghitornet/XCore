namespace XCore.Common.Data.Entity;

/// <summary>
///     The entity modification tracker.
/// </summary>
/// <remarks>
///     This interface is used to mark a class as a database entity with modification tracking.
/// </remarks>
public interface IEntityModificationTracker : IEntityCreationTracker
{
    /// <summary>
    ///     Gets or sets the last modified at.
    /// </summary>
    /// <remarks>
    ///     The last modified at is the date and time when the object was last modified.
    /// </remarks>
    DateTime? LastModifiedAt { get; set; }

    /// <summary>
    ///     Gets or sets the last modified by.
    /// </summary>
    /// <remarks>
    ///     The last modified by is the user who last modified the object.
    /// </remarks>
    string? LastModifiedBy { get; set; }
}