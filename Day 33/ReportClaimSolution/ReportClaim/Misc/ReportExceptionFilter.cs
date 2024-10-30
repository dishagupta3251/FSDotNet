using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ReportClaim.Models.DTO;

namespace ReportClaim.Misc
{
    public class ReportExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            context.Result = new BadRequestObjectResult(new ErrorReponseDTO
            {
                ErrorCode = 500,
                ErrorMessage = context.Exception.Message
            });
        }
    }
}
