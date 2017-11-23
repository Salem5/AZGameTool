using AZGameToolTry1.Model;
using System;

namespace AZGameToolTry1.LService
{
    public delegate void MessageSentHandler(Notification n, EventArgs e);
    public delegate void ClearBadgeCountHandler(EventArgs e);

    public interface IStatusNotificationService
    {
        event MessageSentHandler MessageSent;
        event ClearBadgeCountHandler BadgeCountCleared;

        void SendMessage(Notification message);
        void ClearBadgeCount();
    }
}