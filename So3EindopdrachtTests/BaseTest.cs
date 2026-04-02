using System;
using System.IO;

namespace So3EindopdrachtTests
{
    public abstract class BaseTest : IDisposable
    {
        private readonly TextWriter _originalOut;

        protected BaseTest()
        {
            _originalOut = Console.Out;
            // We zetten de console standaard op een 'safe' writer voor de setup
            Console.SetOut(new StringWriter());
        }

        public void Dispose()
        {
            // Na elke test herstellen we de console naar de echte output
            Console.SetOut(_originalOut);
        }
    }
}