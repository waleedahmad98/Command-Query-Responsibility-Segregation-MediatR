using CQRS_using_MediatR.Models;
using MediatR;

namespace CQRS_using_MediatR.Queries
{
    public class getAllNewsQuery:IRequest<List<NewsModel>> // {QUERY_NAME} : IRequest<{RETURN_TYPE}>
    {

    }
}
