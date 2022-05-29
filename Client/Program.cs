using DiLib;
using System;

namespace Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var container = new DIContainer();

            container.AddSingleton(() => new Logger("hh-mm-ss"));
            container.AddTransition<Random>();
            container.AddTransition<StringGenerator>();
            container.AddTransition<Spamer>();

            var d = container.GetService<Spamer>();
            d.Spam();

            Console.ReadKey();
        }
    }
}
