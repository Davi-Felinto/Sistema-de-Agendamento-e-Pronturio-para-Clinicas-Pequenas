using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClinicaApp.Api.Seguranca;

/// <summary>
/// RQ11: Converte exceções de regra de negócio em respostas amigáveis,
/// sem devolver HTTP 500 nem detalhes internos (stack trace) ao cliente.
/// </summary>
public class ErrosDeNegocioFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is ArgumentException or InvalidOperationException)
        {
            context.Result = new BadRequestObjectResult(new { erro = context.Exception.Message });
            context.ExceptionHandled = true;
        }
    }
}
