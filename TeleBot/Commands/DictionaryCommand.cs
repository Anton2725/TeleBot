using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.EnglishTrainer.Model;

namespace TelegramBot.Commands
{
    public class DictionaryCommand : AbstractCommand, IChatTextCommandWithAction
    {

        private ITelegramBotClient botClient;

        public DictionaryCommand(ITelegramBotClient botClient)
        {
            CommandText = "/dictionary";

            this.botClient = botClient;
        }

        private async Task SendCommandText(string text, long chat)
        {
            await botClient.SendTextMessageAsync(chatId: chat, text: text);
        }

        public async void DoAction(Conversation chat)
        {
            string text = "Словарь: слово на английском / русском / тематика";
            await SendCommandText(text, chat.GetId());

            foreach (var d in chat.dictionary)
            {
                text = $"{d.Value.English} / {d.Value.Russian} / {d.Value.Theme}";
                await SendCommandText(text, chat.GetId());
            }
        }
    }
}
