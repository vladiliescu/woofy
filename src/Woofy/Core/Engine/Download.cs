using System;

namespace Woofy.Core.Engine
{
    public class Download : IStatement
    {
        public string Regex { get; set; }

        public void Run(Context context)
        {
            Console.WriteLine("Downloading strip on {0}.".FormatTo(context.CurrentAddress));
        }
    }
}