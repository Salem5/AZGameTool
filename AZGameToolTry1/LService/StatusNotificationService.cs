using AZGameToolTry1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZGameToolTry1.LService
{
    public class StatusNotificationService : IStatusNotificationService
    {
        public void SendMessage(Notification message)
        {
            MessageSent?.Invoke(message, new EventArgs());
        }

        public void ClearBadgeCount()
        {
            BadgeCountCleared?.Invoke(new EventArgs());
        }

        public event MessageSentHandler MessageSent;
        public event ClearBadgeCountHandler BadgeCountCleared;
    }
}