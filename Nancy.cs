using System;
using System.Collections.Generic;
using System.Text;
using Nancy;
using Nancy.Hosting.Self;


namespace ReverseCoffee
{
    public class Nancy : NancyModule
    {
        public Nancy()
        {
            Get("/", args =>
            {
                Console.WriteLine($"new visitor: {Context.Request.UserHostAddress}");
                PageData model = new PageData()
                {
                    code = McDonald.Instance.GetPromoCode(),
                    currentPoints = McDonald.Instance.GetTotalPoints()
                };
                return View["index.html", model];
            });
        }
    }
}
