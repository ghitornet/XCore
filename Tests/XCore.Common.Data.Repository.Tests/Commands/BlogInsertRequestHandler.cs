using XCore.Common.Data.Command;

namespace XCore.Common.Data.Repository.Tests.Commands;

public class BlogInsertRequestHandler(IRepositoryAsync<Blog> repository) : InsertRequestHandler<Blog>(repository)
{
    
}