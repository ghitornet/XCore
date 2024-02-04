namespace XCore.Common.Data.Entity.Attributes;

/// <summary>
///     The context attribute.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="ContextAttribute" /> class.
/// </remarks>
/// <param name="contextName">
///     The context name.
/// </param>
[AttributeUsage(AttributeTargets.Class)]
public class ContextAttribute(string contextName) : Attribute
{
    /// <summary>
    ///     Gets the context name.
    /// </summary>
    /// <remarks>
    ///     The context name is used to identify the context of the entity.
    /// </remarks>
    public string ContextName { get; } = contextName;
}