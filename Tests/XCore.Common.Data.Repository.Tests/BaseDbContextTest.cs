using MediatR;
using XCore.Common.Data.Command;

namespace XCore.Common.Data.Repository.Tests;

/// <summary>
///     The base db context test.
/// </summary>
public class BaseDbContextTest
{
    /// <summary>
    ///     When BaseDbContext saves an entity, it must save the CreatedAt field.
    /// </summary>
    [Fact]
    public void BaseDbContext_saves_an_entity_it_must_save_the_CreatedAt_field()
    {
        // ARRANGE
        var provider = new ServiceCollection()
            .AddLogging()
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            })
            .AddRepository<TestDbContext>(setEntityReadyToExport: true, ignoreExportTracking: false)
            .AddCommandData()
            .BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();
        var blogRepository = provider.GetRequiredService<IRepositoryAsync<Blog>>();

        var blog = new Blog
        {
            Url = "Url Test Blog"
        };

        var post = new Post("Title Test Post", blog.Id);

        blog.Posts.Add(post);

        var blogBuilder = new RequestBuilder<Blog>().Add(blog).InsertRequest();

        // ACT
        mediator.Send(blogBuilder);

        var blogFromDb = blogRepository.Get().Select(x => new { x.Id, x.Url, x.Posts, x.CreatedAt }).ToList();

        // ASSERT
        blog.Id.Should().NotBe(0);
        blog.CreatedAt.Should().NotBe(default);
    }
}