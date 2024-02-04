namespace XCore.Common.Data.Tests.Data.Configurations;

/// <summary>
///     The blog configuration.
/// </summary>
public class BlogConfiguration : IEntityTypeConfiguration<Blog>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Blog> builder)
    {
        builder.Property(x => x.Id).HasColumnName("BlogId");
    }
}