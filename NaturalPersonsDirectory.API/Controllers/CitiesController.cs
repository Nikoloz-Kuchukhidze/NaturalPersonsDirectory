using MediatR;
using Microsoft.AspNetCore.Mvc;
using NaturalPersonsDirectory.Application.Features.Cities.Commands.Create;
using NaturalPersonsDirectory.Application.Features.Cities.Queries.GetById;
using NaturalPersonsDirectory.Application.Features.Cities.Shared;

namespace NaturalPersonsDirectory.API.Controllers;

[Route("api/[controller]")]
public sealed class CitiesController : BaseController
{
    public CitiesController(ISender sender) 
        : base(sender)
    {
    }

    [HttpPost]
    [ProducesResponseType(typeof(CityResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCity(
        [FromBody] CreateCityCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetCityById),
            new { id = result.Id }, 
            result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCityById(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        var query = new GetCityByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
}
