namespace XCore.Common.Data.Entity;

/// <summary>
///     Represents a database entity.
/// </summary>
/// <remarks>
///     This interface is used to mark a class as a database entity.
/// </remarks>
/// <example>
///     <code>
///         [Key]
///         [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
///         [Column("Id", Order = 0)]
///         public class Person : IEntity
///         {
///             public int Id { get; set; }
/// 
///             public string Name { get; set; }
///         }
///     </code>
/// </example>
public interface IEntity;