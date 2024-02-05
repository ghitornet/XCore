namespace XCore.Common.Data.Repository.Tests.Data.Repositories;

/// <summary>
///     The post repository.
/// </summary>
public interface IPostRepository : IRepositoryAsync<Post>
{
}

/// <summary>
///     The post repository.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="PostRepository" /> class.
/// </remarks>
/// <param name="context">The context.</param>
/// <param name="setEntityReadyToExport">If true, set entity ready to export.</param>
/// <param name="ignoreExportTracking">If true, ignore export tracking.</param>
public class PostRepository(
    TestDbContext context,
    bool? setEntityReadyToExport = null,
    bool? ignoreExportTracking = null)
    : RepositoryAsync<TestDbContext, Post>(context, setEntityReadyToExport, ignoreExportTracking), IPostRepository
{
}