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
                return View["index.html", McDonald.Instance.GetPromoCode()];
            });
        }
    }
}
