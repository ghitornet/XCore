namespace XCore.Common.Data.Entity.Tests;

/// <summary>
///     The person.
/// </summary>
[Context("NoDbContext")]
[Index(nameof(RowGuid), IsUnique = true)]
public class Person : EntityBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Person" /> class.
    /// </summary>
    public Person()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Person" /> class.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    [SetsRequiredMembers]
    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    ///     Gets or sets the first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    ///     Gets or sets the last name.
    /// </summary>
    public required string LastName { get; set; }
}