﻿
using Renderer;
using System.Text.RegularExpressions;

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

        static public bool TryParsePosition(out Position2D position, params string[] parameters)
        {
            int pos; uint charvalue = 0;
            if (parameters.Length <= 1)
            {
                ShowMessage("No parameters provided");
                position = default;
                return false;
            }
            else if (parameters.Length == 2)
            {
                var match = Regex.Matches(parameters[1], @"[a-zA-Z]+|[0-9]+(?:[0-9]+|)").ToArray();
                if (match.Length != 2) 
                { 
                    ShowMessage("Invalid parameters"); 
                    position = default;
                    return false; 
                }
                int temp;
                if (int.TryParse(match[0].Value, out pos) && !int.TryParse(match[1].Value, out temp))
                {
                    var en = match[1].Value.GetEnumerator();
                    for (int i = 0; en.MoveNext(); i++)
                    {
                        charvalue += (uint)(((((byte)en.Current) | 96) - 96) * (uint)Math.Pow(26, i));
                    }
                    charvalue--;
                    //ShowMessage("Position X:" + pos + " Y:" + charvalue);
                }
                else if (int.TryParse(match[1].Value, out pos) && !int.TryParse(match[0].Value, out temp))
                {
                    var en = match[0].Value.GetEnumerator();
                    for (int i = 0; en.MoveNext(); i++)
                    {
                        charvalue += (uint)(((((byte)en.Current) | 96) - 96) * (uint)Math.Pow(26, i));
                    }
                    charvalue--;
                    //ShowMessage("Position X:" + pos + " Y:" + charvalue);
                }
                else
                {
                    ShowMessage("Invalid parameters, they should look like a4 or 4a or 4 a");
                    position = default;
                    return false;
                }
            }
            else if (parameters.Length == 3)
            {
                int temp;
                if (int.TryParse(parameters[1], out pos) && !int.TryParse(parameters[2], out temp))
                {
                    var en = parameters[2].GetEnumerator();
                    for (int i = 0; en.MoveNext(); i++)
                    {
                        charvalue += (uint)(((((byte)en.Current) | 96) - 96) * (uint)Math.Pow(26, i));
                    }
                    charvalue--;
                    //ShowMessage("Position X:" + pos + " Y:" + charvalue);
                }
                else if (int.TryParse(parameters[2], out pos) && !int.TryParse(parameters[1], out temp))
                {
                    var en = parameters[1].GetEnumerator();
                    for (int i = 0; en.MoveNext(); i++)
                    {
                        charvalue += (uint)(((((byte)en.Current) | 96) - 96) * (uint)Math.Pow(26, i));
                    }
                    charvalue--;
                    //ShowMessage("Position X:" + pos + " Y:" + charvalue);
                }
                else
                {
                    ShowMessage("Invalid parameters, they should look like a4 or 4a or 4 a");
                    position = default;
                    return false;
                }
            }
            else 
            { 
                ShowMessage("Too Much Parameters"); 
                position = default;
                return false; 
            }
            if (charvalue >= CurrentScene.Width || pos >= CurrentScene.Height)
            {
                ShowMessage("Invalid parameters, out of bounds");
                position = default;
                return false;
            }
            position = new(pos, (int)charvalue);
            return true;
        }
    }
}
