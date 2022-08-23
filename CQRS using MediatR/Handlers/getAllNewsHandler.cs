using CQRS_using_MediatR.Data;
using CQRS_using_MediatR.Models;
using CQRS_using_MediatR.Queries;
using MediatR;

namespace CQRS_using_MediatR.Handlers
{
    public class getAllNewsHandler : IRequestHandler<getAllNewsQuery, List<NewsModel>> //{HANDLER_NAME: IRequestHandler<{QUERY_NAME}, {RETURN_TYPE}> }
    {
        private readonly IDataRepo _DataRepo; // inject

        public getAllNewsHandler(IDataRepo DataRepo)
        {
            _DataRepo = DataRepo;
        }

        public Task<List<NewsModel>> Handle(getAllNewsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_DataRepo.getAllNews());
        }
    }
}
