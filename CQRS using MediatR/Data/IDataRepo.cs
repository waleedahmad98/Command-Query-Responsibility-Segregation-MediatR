using CQRS_using_MediatR.Models;

namespace CQRS_using_MediatR.Data
{
    public interface IDataRepo
    {
        List<NewsModel> getAllNews();
        NewsModel addNews(string title, string content);
    }
}
