namespace XCore.Common.Data.Entity;
/// <summary>
/// The entity id.
/// </summary>
/// <remarks>
/// This interface is used to mark a class as a database entity with an id.
/// </remarks>
/// <example>
///     <code>
///         [Index(nameof(RowGuid), IsUnique = true)]
///         public class Person : IEntityId
///         {
///             [Key]
///             [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
///             [Column("Id", Order = 0)]
///             public int Id { get; set; }
/// 
///             public Guid RowGuid { get; set; }
/// 
///             public string Name { get; set; }
///         }
///     </code>
/// </example>
public interface IEntityId : IEntity
{
    /// <summary>
    ///     Gets or sets the id.
    /// </summary>
    /// <remarks>
    ///     The id is the primary key of the entity.
    ///     It is automatically assigned by the database when the object is created.
    /// </remarks>
    int Id { get; set; }

    /// <summary>
    ///     Gets or sets the row guid.
    /// </summary>
    /// <remarks>
    ///     The row guid is used to uniquely identify a row in a table.
    ///     This field is not automatically assigned by the database but can be assigned directly when the object is created.
    ///     It is useful in case of searching for single data not to have a reference within the url.
    /// </remarks>
    Guid RowGuid { get; set; }
}