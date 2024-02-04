using Microsoft.Extensions.DependencyInjection;

namespace XCore.Common.Data.Tests;

/// <summary>
///     The base db context test.
/// </summary>
public class BaseDbContextTest
{
    /// <summary>
    ///     BaseDbContext must load all defined entities correctly.
    /// </summary>
    [Fact]
    public void BaseDbContext_must_load_all_defined_entities_correctly()
    {
        // ARRANGE
        var provider = new ServiceCollection()
            .AddLogging()
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            })
            .BuildServiceProvider();
        
        var context = provider.GetRequiredService<TestDbContext>();

        // ACT
        var entities = context.Model.GetEntityTypes().ToList();

        // ASSERT
        entities.Should().NotBeEmpty();
        entities.Should().HaveCount(2);
    }

    /// <summary>
    ///     BaseDbContext should load all configurations correctly.
    /// </summary>
    [Fact]
    public void BaseDbContext_should_load_all_configurations_correctly()
    {
        // ARRANGE
        var provider = new ServiceCollection()
            .AddLogging()
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            })
            .BuildServiceProvider();

        var context = provider.GetRequiredService<TestDbContext>();

        // ACT
        var entity = context.Model.FindEntityType(typeof(Blog))!;

        // ASSERT
        entity.GetProperties().Single(x => x.Name == nameof(Blog.Id)).GetColumnName().Should().Be("BlogId");
    }

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
            .BuildServiceProvider();

        var context = provider.GetRequiredService<TestDbContext>();

        var blog = new Blog
        {
            Url = "Url Test Blog"
        };

        // ACT
        context.Set<Blog>().Add(blog);
        context.SaveAllChanges(true);

        // ASSERT
        blog.Id.Should().NotBe(0);
        blog.CreatedAt.Should().NotBe(default);
    }

    /// <summary>
    /// Bases the db context_saves_an_entity_it_must_set_the_ is ready to export_field.
    /// </summary>
    [Fact]
    public void BaseDbContext_saves_an_entity_it_must_set_the_IsReadyToExport_field()
    {
        // ARRANGE
        var provider = new ServiceCollection()
            .AddLogging()
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            })
            .BuildServiceProvider();

        var context = provider.GetRequiredService<TestDbContext>();

        var blog = new Blog
        {
            Url = "Url Test Blog"
        };

        // ACT
        context.Set<Blog>().Add(blog);
        context.SaveAllChanges(true);

        // ASSERT
        blog.Id.Should().NotBe(0);
        blog.IsReadyToExport.Should().Be(true);
    }

    /// <summary>
    /// Bases the db context_saves_an_entity_it_must_no_set_the_ is ready to export_field.
    /// </summary>
    [Fact]
    public void BaseDbContext_saves_an_entity_it_must_no_set_the_IsReadyToExport_field()
    {
        // ARRANGE
        var provider = new ServiceCollection()
            .AddLogging()
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            })
            .BuildServiceProvider();

        var context = provider.GetRequiredService<TestDbContext>();

        var blog = new Blog
        {
            Url = "Url Test Blog"
        };

        // ACT
        context.Set<Blog>().Add(blog);
        context.SaveAllChanges(false);

        // ASSERT
        blog.Id.Should().NotBe(0);
        blog.IsReadyToExport.Should().Be(false);
    }

    [Fact]
    public void S()
    {
        // ARRANGE
        var provider = new ServiceCollection()
            .AddLogging()
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            })
            .BuildServiceProvider();

        var context = provider.GetRequiredService<TestDbContext>();

        var blog = new Blog
        {
            Url = "Url Test Blog"
        };

        context.Set<Blog>().Add(blog);
        context.SaveAllChanges(true);

        blog.Url = "Url Test Blog 2";
        context.Set<Blog>().Update(blog);

        // ACT
        context.SaveImportChanges(false);
        var blogFromDb = context.Set<Blog>().Find(blog.Id)!;

        // ASSERT
        blog.Id.Should().NotBe(0);
        blogFromDb.Url.Should().Be("Url Test Blog 2");
        blog.IsReadyToExport.Should().Be(true);
    }
    [Fact]
    public void S2()
    {
        // ARRANGE
        var provider = new ServiceCollection()
            .AddLogging()
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            })
            .BuildServiceProvider();

        var context = provider.GetRequiredService<TestDbContext>();

        var blog = new Blog
        {
            Url = "Url Test Blog"
        };

        context.Set<Blog>().Add(blog);
        context.SaveAllChanges(true);

        blog.Url = "Url Test Blog 2";
        context.Set<Blog>().Update(blog);

        // ACT
        context.SaveImportChanges(true);
        var blogFromDb = context.Set<Blog>().Find(blog.Id)!;

        // ASSERT
        blog.Id.Should().NotBe(0);
        blogFromDb.Url.Should().Be("Url Test Blog");
        blog.IsReadyToExport.Should().Be(true);
    }
}