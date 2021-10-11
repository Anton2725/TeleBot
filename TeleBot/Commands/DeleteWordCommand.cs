using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramBot.Commands
{
    public class DeleteWordCommand : ChatTextCommandOption, IChatTextCommandWithAction
    {
        private ITelegramBotClient botClient;
        public DeleteWordCommand(ITelegramBotClient botClient)
        {
            CommandText = "/deleteword";
            this.botClient = botClient;
        }

        public async void DoAction(Conversation chat)
        {
            var message = chat.GetLastMessage();

            var text = ClearMessageFromCommand(message);

            if (!(text == "") && chat.dictionary.ContainsKey(text))
            {
                chat.dictionary.Remove(text);
                text = "Слово удалено из словаря.";
            }
            else
            {
                text = "Укажите слово для удаления, например /deleteword слово";
            }

            await SendCommandText(text, chat.GetId());

        }
        private async Task SendCommandText(string text, long chat)
        {
            await botClient.SendTextMessageAsync(chatId: chat, text: text);
        }

    }
}
