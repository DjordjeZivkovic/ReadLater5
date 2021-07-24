using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ReadLater5.Presentation.Filters
{
    public class ActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                context.ModelState.Clear();

            if (!context.ModelState.IsValid)
            {
                var controller = context.Controller as Controller;

                var model = context.ActionArguments.Any() ? context.ActionArguments.First().Value : null;

                context.Result = (IActionResult)controller?.View(model) ?? new BadRequestResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
