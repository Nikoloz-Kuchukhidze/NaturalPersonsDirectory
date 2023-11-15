using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace NaturalPersonsDirectory.API.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected ISender Sender;

    public BaseController(ISender sender)
    {
        Sender = sender;
    }
}
