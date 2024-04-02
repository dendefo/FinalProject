
using Renderer;

namespace Core.Commands
{
    public class CommandSystem
    {
        private CancellationTokenSource cancelToken;
        private Func<string> currentAction;
        public List<Command> Commands { get; private set; }
        public HelpCommand HelpCommand { get; set; }
        private static CommandSystem instance;
        public static CommandSystem Instance
        {
            get
            {
                if (instance == null) instance = new();
                return instance;
            }
            private set { instance = value; }
        }
        public TileObject SelectedObject;
        public CommandSystem()
        {
            Commands = new();
            HelpCommand = new("Help", "Displays all available commands", default);
            Commands.Add(HelpCommand);
            Instance = this;
        }
        public void AddCommand(Command command)
        {
            Commands.Add(command);
        }
        public void RemoveCommand(Command command)
        {
            Commands.Remove(command);
        }
        async public void StartListeningAsync(Func<string> action)
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
        public void Listen(Func<string> action)
        {
            if (currentAction != null) return;

            currentAction = action;
            action += (currentAction = null);
            var value = action();

            int activated = 0;
            string[] parameters = value.Split(' ');
            if (parameters.Length == 0) return;

            foreach (var command in Commands)
            {
                if (parameters[0] == command.Prompt)
                {
                    activated++;
                    command.Activate(parameters);
                }
            }
            if (activated == 0)
            {
                HelpCommand.Activate(parameters);
            }
        }
        public void StopListening() => cancelToken.Cancel();

    }
}
