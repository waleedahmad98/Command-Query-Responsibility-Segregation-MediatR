using CQRS_using_MediatR.Models;

namespace CQRS_using_MediatR.Data
{
    public class DataRepo : IDataRepo
    {
        private List<NewsModel> _news = new();

        public DataRepo()
        {
            _news.Add(new NewsModel { title = "News 1", content = "This is News 1" });
        }

        public NewsModel addNews(string title, string content)
        {
            NewsModel temp = new() { title = title, content = content };
            _news.Add(temp);
            return temp;
        }

        public List<NewsModel> getAllNews()
        {
            return _news;
        }
    }
}
