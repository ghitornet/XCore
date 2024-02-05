namespace XCore.Common.Data.Repository.Tests.Data.Repositories;

/// <summary>
///     The blog repository.
/// </summary>
public interface IBlogRepository : IRepository<Blog>
{
}

/// <summary>
///     The blog repository.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="BlogRepository" /> class.
/// </remarks>
/// <param name="context">The context.</param>
/// <param name="setEntityReadyToExport">If true, set entity ready to export.</param>
/// <param name="ignoreExportTracking">If true, ignore export tracking.</param>
public class BlogRepository(
    TestDbContext context,
    bool? setEntityReadyToExport = null,
    bool? ignoreExportTracking = null)
    : Repository<TestDbContext, Blog>(context, setEntityReadyToExport, ignoreExportTracking), IBlogRepository
{
}