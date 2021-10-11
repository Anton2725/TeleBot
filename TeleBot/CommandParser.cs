using System.Collections.Generic;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Commands;

namespace TelegramBot
{
    public class CommandParser
    {
        private List<IChatCommand> Command;

        private AddingController addingController;

        public CommandParser()
        {
            Command = new List<IChatCommand>();
            addingController = new AddingController();
        }

        public void AddCommand(IChatCommand chatCommand)
        {
            Command.Add(chatCommand);
        }

        public bool IsMessageCommand(string message)
        {
            return Command.Exists(x => x.CheckMessage(message));
        }

        public bool IsTextCommandWithAction(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            return command is IChatTextCommandWithAction;
        }

        public bool IsButtonCommand(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            return command is IKeyBoardCommand;
        }

        public string GetMessageText(string message, Conversation chat)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            if (command is IChatTextCommandWithAction)
            {
                var com = (IChatTextCommandWithAction)command;
                com.DoAction(chat);
            }

            return "команда выполнена";
        }
        public void DoAction(string message, Conversation chat)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            if (command is IChatTextCommandWithAction)
            {
                var com = (IChatTextCommandWithAction)command;
                com.DoAction(chat);
            }
        }

        public string GetInformationalMeggase(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;

            return command.InformationalMessage();
        }

        public InlineKeyboardMarkup GetKeyBoard(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;

            return command.ReturnKeyBoard();
        }

        public void AddCallback(string message, Conversation chat)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;
            command.AddCallBack(chat);
        }

        public bool IsAddingCommand(string message)
        {
            var command = Command.Find(x => x.CheckMessage(message));

            return command is AddWordCommand;
        }

        public void StartAddingWord(string message, Conversation chat)
        {
            var command = Command.Find(x => x.CheckMessage(message)) as AddWordCommand;

            addingController.AddFirstState(chat);
            command.StartProcessAsync(chat);

        }

        public void NextStage(string message, Conversation chat)
        {
            var command = Command.Find(x => x is AddWordCommand) as AddWordCommand;

            command.DoForStageAsync(addingController.GetStage(chat), chat, message);

            addingController.NextStage(message, chat);

        }
    }
}
