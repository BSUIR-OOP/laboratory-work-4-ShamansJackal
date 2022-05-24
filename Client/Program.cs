using DiLib;
using System;

namespace Client
{
    internal class Program
    {
        public class TestClass
        {
            private string name;
            public TestClass()
            {
                name = "dsa";
            }

            public void Print()
            {
                Console.WriteLine(name);
            }
        }

        public class TestClass2
        {
            private TestClass testClass;
            public TestClass2(TestClass testClass)
            {
                this.testClass = testClass;
            }

            public void P() => testClass.Print();
        }

        static void Main(string[] args)
        {
            var container = new DIContainer();

            container.AddTransition<TestClass>();
            container.AddTransition<TestClass2>();

            var d = container.GetService<TestClass2>();
            d.P();

            Console.ReadKey();
            Console.WriteLine("Hello World!");
        }
    }
}
