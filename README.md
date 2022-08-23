# CQRS using MediatR

## CQRS Theory

Command Query Responsibility Segregation is an API design pattern that de-couples **read** and **write** operations. A traditional API uses the CRUD approach to send/retrieve data from a source, but CQRS separates ‘R’ from ‘CUD’ in CRUD. 

## Mediator Pattern

This pattern reduces communicational complexity between components by providing a *mediator* class that handles all the communication.

## MediatR

MediatR is a library that provides methods to implement CQRS practices in .NET applications. MediatR provides *IMediator, IRequest* and *IRequestHandler* interfaces that help the controllers handle user requests.

Our program relies on three further files than traditional .NET APIs. These are *Handlers, Queries (for READ operations), Commands (for WRITE operations).*

### Tutorial

- **MODELS:** Start by creating a **Models** folder and add a new model. For example, *NewsModel.cs*

```csharp
namespace CQRS_using_MediatR.Models
{
    public class NewsModel
    {
        public string title { get; set; }
        public string content { get; set; }
    }
}
```

- **CONTROLLERS:** Create a simple [ASP.NET](http://ASP.NET) Core Web API and create a folder named **Controllers**. In our example, we are creating a *NewsController.cs*

```csharp
namespace CQRS_using_MediatR.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private readonly IMediator _mediator; // mediator injected in Program.cs

        public NewsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<NewsModel>> Get() // GET call
        {
            return await _mediator.Send(new getAllNewsQuery());
						// Do not worry about getAllNewsQuery here, we will discuss it later on.
        }

        [HttpPost]
        public async Task<NewsModel> Post([FromBody] NewsModel dto) // POST call
        {
            return await _mediator.Send(new addNewsCommand(dto.title, dto.content));
						// Do not worry about addNewsCommand here, we will discuss it later on.
        }
    }
}
```

- **QUERIES:** Create a new folder called **Queries** and in that folder create a class called *getAllNewsQuery.cs.* The class implements the IRequest interface provided by the MediatR library. The datatype supported is the same as the return type of the query (ie, List<NewsModel>)

```csharp
namespace CQRS_using_MediatR.Queries
{
    public class getAllNewsQuery:IRequest<List<NewsModel>> {} // {QUERY_NAME} : IRequest<{RETURN_TYPE}>
}
```

- **COMMANDS:** Similarly create another folder called **Commands** and add a class called *addNewsCommand.cs*

```csharp
namespace CQRS_using_MediatR.Commands
{
    public class addNewsCommand:IRequest<NewsModel>
    {
				public string title { get; set; }
        public string content { get; set; }
        
				public addNewsCommand(string title, string content)
        {
            this.title = title;
            this.content = content;
        }
    }
}
```

- Now that we have our Queries and Command Requests setup, we need to add a method to allow mediator to *HANDLE* these requests using the *IRequestHandler* interface. To do that make a folder called **Handlers** and add two new handlers for each of these requests called *addNewsHandler.cs* and *getNewsHandler.cs*.

```csharp
//getNewsHandler.cs

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

//addNewsHandler.cs

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

```

- Finally the *Program.cs* file.

```csharp
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DEPENDENCY INJECTION
builder.Services.AddSingleton<IDataRepo, DataRepo>(); // inject db
builder.Services.AddMediatR(typeof(DataRepo).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

Github Repo for the code: https://github.com/waleedahmad98/Command-Query-Responsibility-Segregation-MediatR

Reference: [https://www.c-sharpcorner.com/article/cqrs-pattern-using-mediatr-in-net-5/](https://www.c-sharpcorner.com/article/cqrs-pattern-using-mediatr-in-net-5/)
