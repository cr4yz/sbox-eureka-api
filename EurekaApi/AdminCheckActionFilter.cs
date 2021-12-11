using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EurekaApi
{
    public class AdminCheckActionFilter : ActionFilterAttribute
    {

        private readonly ILogger _logger;
        private readonly string _safelist;

        public AdminCheckActionFilter(string safelist, ILogger logger)
        {
            _safelist = safelist;
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var adminIds = _safelist.Split(';');
            var badIp = true;

            if (!context.HttpContext.Request.Headers.TryGetValue("steamid", out var steamId))
            {
                badIp = true;
            }
            else
            {
                foreach (var id in adminIds)
                {
                    if (id.Equals(steamId))
                    {
                        badIp = false;
                        break;
                    }
                }
            }

            if (badIp)
            {
                _logger.LogWarning("Forbidden Request from SteamID: {SteamId}", steamId);
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }

    }
}
