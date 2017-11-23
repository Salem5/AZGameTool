using AZGameToolTry1.Model;

namespace AZGameToolTry1.ViewModel
{
    public class NotificationItemViewModel : BaseViewModel
    {
        private Notification model;

        public Notification Model
        {
            get => model;
            set => SetValue(value, ref model);
        }

        private bool expandNotification;
        public bool ExpandNotification
        {
            get => expandNotification;
            set => SetValue<bool>(value, ref expandNotification);
        }

        private bool seen;
        public bool Seen
        {
            get => seen;
            set => SetValue<bool>(value, ref seen);
        }
    }
}