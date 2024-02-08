namespace XCore.Common.Data.Repository.Tests.Data.Repositories;

/// <summary>
///     The blog repository.
/// </summary>
public interface IBlogRepository : IRepositoryAsync<Blog>
{
}

/// <summary>
///     The blog repository.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="BlogRepository" /> class.
/// </remarks>
/// <param name="context">The context.</param>
/// <param name="loggerFactory">The logger factory.</param>
/// <param name="setEntityReadyToExport">If true, set entity ready to export.</param>
/// <param name="ignoreExportTracking">If true, ignore export tracking.</param>
public class BlogRepository(
    TestDbContext context,
    ILoggerFactory loggerFactory,
    bool? setEntityReadyToExport = null,
    bool? ignoreExportTracking = null)
    : RepositoryAsync<TestDbContext, Blog>(context, loggerFactory, setEntityReadyToExport, ignoreExportTracking), IBlogRepository
{
}