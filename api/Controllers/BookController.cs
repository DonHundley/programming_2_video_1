using System.ComponentModel.DataAnnotations;
using infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using service;

namespace library.Controllers;

[ApiController]
public class BookController : ControllerBase
{

    private readonly ILogger<BookController> _logger;
    private readonly BookService _bookService;

    public BookController(ILogger<BookController> logger,
        BookService bookService)
    {
        _logger = logger;
        _bookService = bookService;
    }



    [HttpGet] // client wants to get something
    [Route("/api/books")]
    public ResponseDto Get()
    {
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a book",
            ResponseData = _bookService.GetBooksForFeed()
        };
    }

    [HttpPost] // client wants to add something
    [Route("/api/book")]
    public ResponseDto Post([FromBody] CreateBookRequestDto dto)
    {
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;
        return new ResponseDto()
        {
            MessageToClient = "Successfully created a book",
            ResponseData = _bookService.CreateBook(dto.BookTitle)
        };

    }
}

public class ResponseDto
{
    public string MessageToClient { get; set; }
    public Object? ResponseData { get; set; }
}

public class CreateBookRequestDto
{
    [MinLength(5)]
    public string BookTitle { get; set; }
    
}
