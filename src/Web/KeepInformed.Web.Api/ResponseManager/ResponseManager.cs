using FluentValidation;
using KeepInformed.Common.Exceptions;
using KeepInformed.Web.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeepInformed.Web.Api.ResponseManager;

public class ResponseManager : IResponseManager
{
    private readonly IMediator _mediator;

    public ResponseManager(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> SendCommand(IRequest command)
    {
        try
        {
            await _mediator.Send(command);
            var response = new Response(null);

            return new JsonResult(response);
        }
        catch (ValidationException validationException)
        {
            var response = new Response(validationException.Errors, "VALIDATION_EXCEPTION");

            return new BadRequestObjectResult(response);
        }
        catch (DomainException domainException)
        {
            var response = new Response(domainException.Message);

            return new BadRequestObjectResult(response);
        }
        catch (Exception)
        {
            var response = new Response("UNHANDLED_EXCEPTION");

            return new ObjectResult(response)
            {
                StatusCode = 500
            };
        }
    }

    public async Task<IActionResult> SendQuery<T>(IRequest<T> query)
    {
        try
        {
            var result = await _mediator.Send(query);
            var response = new Response(result);

            return new JsonResult(response);
        }
        catch (DomainException domainException)
        {
            var response = new Response(domainException.Message);

            return new BadRequestObjectResult(response);
        }
        catch (Exception exception)
        {
            var response = new Response(exception.Message);

            return new ObjectResult(response)
            {
                StatusCode = 500
            };
        }
    }
}
