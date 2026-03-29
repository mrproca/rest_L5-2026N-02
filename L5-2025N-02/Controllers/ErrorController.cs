using L5_2025N_02.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace L5_2025N_02.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController: ControllerBase
{
    [Route("/error")]
    public IActionResult HandleError()
    {
        var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = feature?.Error;

        return exception switch
        {
            BadRequestException ex => BadRequest(new { error = ex.Message }),
            NotFoundException ex => NotFound(new { error = ex.Message }),
            _ => StatusCode(500, new { error = "Wystąpił nieoczekiwany błąd serwera." })
        };
    }
}