namespace XCore.Common.Data.Tests.Data;

/// <summary>
///     TestDbContext
/// </summary>
public class TestDbContext(DbContextOptions contextOptions, ILoggerFactory loggerFactory) : BaseDbContext(contextOptions, loggerFactory)
{
}