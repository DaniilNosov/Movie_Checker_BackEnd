using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movie_Checker_BackEnd.Services;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Movie_Checker_BackEnd.Controllers;
[Route("api/[controller]")]
[ApiController]
public class MovieController : ControllerBase
{
    private readonly CheckerService _service;

    public MovieController(CheckerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies(string movieName)
    {
        var jsonDataString = await _service.CheckMovieAsync(movieName);
        return Ok(jsonDataString);
    }
}
