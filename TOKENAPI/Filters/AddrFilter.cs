using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Text.RegularExpressions;
using TOKENAPI.CQRS;

namespace TOKENAPI.Filters
{
    public class AddrFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var addrGet = context.ActionArguments["addr"] as string;
            var addrPost = context.ActionArguments["addr"] as AddrPost;
            string? addr = addrGet ?? addrPost?.Addr;


            Regex _regex = new Regex("[^a-zA-Z0-9]", RegexOptions.Compiled);
            if(_regex.IsMatch(addr))
            {
                context.Result = new BadRequestObjectResult("Bad Request");
            }
            base.OnActionExecuting(context);
        }
    }
}
