using CQRS_using_MediatR.Commands;
using CQRS_using_MediatR.Data;
using CQRS_using_MediatR.Models;
using MediatR;

namespace CQRS_using_MediatR.Handlers
{
    public class addNewsHandler : IRequestHandler<addNewsCommand, NewsModel>
    {
        private readonly IDataRepo _DataRepo;
        public addNewsHandler(IDataRepo DataRepo)
        {
            _DataRepo = DataRepo;
        }
        public Task<NewsModel> Handle(addNewsCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_DataRepo.addNews(request.title, request.content));
        }
    }
}
