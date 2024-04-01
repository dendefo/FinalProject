using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Core.Commands
{
    public class CommandSystem<T>
    {
        private CancellationTokenSource cancelToken;
        private Func<T> currentAction;
        public List<Command<T>> Commands { get; private set; }
        public HelpCommand<T> HelpCommand { get; set; }
        public CommandSystem()
        {
            Commands = new();
            HelpCommand = new("Help", "Displays all available commands", default);
            Commands.Add(HelpCommand);
        }
        public void AddCommand(Command<T> command)
        {
            Commands.Add(command);
        }
        public void RemoveCommand(Command<T> command)
        {
            Commands.Remove(command);
        }
        async public void StartListeningAsync(Func<T> action)
        {
            cancelToken = new CancellationTokenSource();

            var _task = await Task.Run(action, cancelToken.Token);
            int activated = 0;
            foreach (var command in Commands)
            {
                if (_task.Equals(command.Prompt))
                {
                    activated++;
                    command.Activate();
                }
            }
            if (activated == 0)
            {
                HelpCommand.Activate();
            }
        }
        public void Listen(Func<T> action)
        {
            T value;
            if (currentAction != null) return;

            currentAction = action;
            action += (currentAction = null);
            value = action();

            int activated = 0;
            foreach (var command in Commands)
            {
                if (value.Equals(command.Prompt))
                {
                    activated++;
                    command.Activate();
                }
            }
            if (activated == 0)
            {
                HelpCommand.Activate();
            }
        }
        public void StopListening() => cancelToken.Cancel();

    }
}
