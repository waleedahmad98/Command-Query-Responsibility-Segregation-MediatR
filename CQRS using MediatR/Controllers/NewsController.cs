using CQRS_using_MediatR.Commands;
using CQRS_using_MediatR.Models;
using CQRS_using_MediatR.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_using_MediatR.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private readonly IMediator _mediator;

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<NewsModel>> Get()
        {
            return await _mediator.Send(new getAllNewsQuery());
        }

        [HttpPost]
        public async Task<NewsModel> Post([FromBody] NewsModel dto)
        {
            return await _mediator.Send(new addNewsCommand(dto.title, dto.content));
        }
    }
}
