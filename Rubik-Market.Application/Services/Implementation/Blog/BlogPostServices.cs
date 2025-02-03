using Rubik_Market.Application.Services.Contracts.Blog;
using Rubik_Market.Domain.Repo.Contracts;

namespace Rubik_Market.Application.Services.Implementation.Blog;

public class BlogPostServices : IBlogPostServices
{
    #region Constructor

    private readonly IBlogRepository _blogRepository;

    public BlogPostServices(IBlogRepository blogRepository)
    {
        _blogRepository = blogRepository;
    }

    #endregion


}