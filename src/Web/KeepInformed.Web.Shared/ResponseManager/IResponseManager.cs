using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeepInformed.Web.Shared.ResponseManager;

public interface IResponseManager
{
    Task<IActionResult> SendCommand(IRequest command);
    Task<IActionResult> SendQuery<T>(IRequest<T> query);
}
