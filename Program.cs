using System;
using System.Threading;
using Nancy.Hosting.Self;

namespace ReverseCoffee
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reversing for Coffee!");
            McDonald mcdo = new McDonald();
            Console.WriteLine("Starting up nancy at 8787");
            NancyHost host = new NancyHost(new Uri("http://localhost:8787"));
            host.Start();
            Console.WriteLine("Nancy is ready");
            Thread.Sleep(-1);
        }
    }
}
