namespace XCore.Common.Data.Entity;
/// <summary>
/// The entity creation tracker.
/// </summary>
/// <remarks>
/// This interface is used to mark a class as a database entity with creation tracking.
/// </remarks>
/// <example>
///     <code>
///         [Index(nameof(RowGuid), IsUnique = true)]
///         public class Person : IEntityCreationTracker
///         {
///             [Key]
///             [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
///             [Column("Id", Order = 0)]
///             public int Id { get; set; }
/// 
///             public Guid RowGuid { get; set; }
///
///             public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
///
///             public string CreatedBy { get; set; }
///         }
///     </code>
/// </example>
public interface IEntityCreationTracker : IEntityId
{
    /// <summary>
    ///     Gets or sets the created at.
    /// </summary>
    /// <remarks>
    ///     The created at is the date and time when the object was created.
    /// </remarks>
    DateTime CreatedAt { get; set; }

    /// <summary>
    ///     Gets or sets the created by.
    /// </summary>
    /// <remarks>
    ///     The created by is the user who created the object.
    /// </remarks>
    string? CreatedBy { get; set; }
}