using System.Diagnostics;
using Woofy.Core.Infrastructure;

namespace Woofy.Core
{
    public class StartProcess : ICommand
    {
        public string Process { get; private set; }

        public StartProcess(string process)
        {
            Process = process;
        }
    }

    public class StartProcessHandler : ICommandHandler<StartProcess>
    {
        public void Handle(StartProcess command)
        {
            Process.Start(command.Process);
        }
    }
}