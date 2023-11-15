using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NaturalPersonsDirectory.API.Contracts.NaturalPersons;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.AddRelation;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Create;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Delete;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.RemoveRelation;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Update;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.UploadImage;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.Get;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.GetById;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.GetRelationsCount;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared;
using NaturalPersonsDirectory.Domain.Common.Paging;

namespace NaturalPersonsDirectory.API.Controllers;

[Route("api/natural-persons")]
public sealed class NaturalPersonsController : BaseController
{
    public NaturalPersonsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(IPagedList<NaturalPersonResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNaturalPersons(
        [FromQuery] GetNaturalPersonsQuery request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(NaturalPersonResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNaturalPersonById(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var query = new GetNaturalPersonByIdQuery(id);
        var result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(NaturalPersonResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CreateNaturalPerson(
        [FromBody] CreateNaturalPersonCommand request,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetNaturalPersonById),
            new { id = result.Id },
            result);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateNaturalPerson(
        [FromRoute] long id,
        [FromBody] UpdateNaturalPersonRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<UpdateNaturalPersonCommand>();
        command.Id = id;
        await Sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPut("{id}/upload-image")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UploadNaturalPersonImage(
        [FromRoute] long id,
        [FromForm] UploadNaturalPersonImageRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UploadNaturalPersonImageCommand(id, request.Image);
        await Sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpPost("{id}/relations")]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddRelation(
        [FromRoute] long id,
        [FromBody] AddRelationRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.Adapt<AddRelationCommand>();
        command.NaturalPersonId = id;

        await Sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}/relations/{relatedNaturalPersonId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveRelation(
        [FromRoute] long id,
        [FromRoute] long relatedNaturalPersonId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveRelationCommand(id, relatedNaturalPersonId);
        await Sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNaturalPerson(
        [FromRoute] long id,
        CancellationToken cancellationToken)
    {
        var command = new DeleteNaturalPersonCommand(id);
        await Sender.Send(command, cancellationToken);

        return NoContent();
    }

    [HttpGet("relations-count")]
    [ProducesResponseType(typeof(IEnumerable<NaturalPersonWithRelationsCountResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNaturalPersonsWithRelationsCount(
        CancellationToken cancellationToken)
    {
        var query = new GetNaturalPersonsWithRelationsCountQuery();
        var result = await Sender.Send(query, cancellationToken);

        return Ok(result);
    }
}
