using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MoviesApp.Filters
{
    public class CheckingTheActorForTheAgeOfSevenToNinetyNineYears: Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var age = DateTime.Parse(context.HttpContext.Request.Form["DateOfBirth"]);
            if ((DateTime.Now.Year - age.Year) < 7 || (DateTime.Now.Year - age.Year) > 99)
            {
                context.Result=new BadRequestResult();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}