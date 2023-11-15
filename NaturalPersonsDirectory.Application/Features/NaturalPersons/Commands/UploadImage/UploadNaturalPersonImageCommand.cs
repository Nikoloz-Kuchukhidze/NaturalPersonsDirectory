using MediatR;
using Microsoft.AspNetCore.Http;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.UploadImage;

public sealed record UploadNaturalPersonImageCommand(long Id, IFormFile Image) : IRequest;