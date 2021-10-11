using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramBot.Commands
{
    interface IChatTextCommandWithAction
    {
        void DoAction(Conversation chat);
    }
}
