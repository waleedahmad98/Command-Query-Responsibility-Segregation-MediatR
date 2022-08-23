using CQRS_using_MediatR.Models;
using MediatR;

namespace CQRS_using_MediatR.Commands
{
    public class addNewsCommand:IRequest<NewsModel>
    {
        public addNewsCommand(string title, string content)
        {
            this.title = title;
            this.content = content;
        }

        public string title { get; set; }
        public string content { get; set; }
    }
}
