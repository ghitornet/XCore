namespace XCore.Common.Data.Repository.Tests.Data;

/// <summary>
///     TestDbContext
/// </summary>
public class TestDbContext(DbContextOptions contextOptions, ILoggerFactory loggerFactory) : BaseDbContext(contextOptions, loggerFactory.CreateLogger<BaseDbContext>())
{
}