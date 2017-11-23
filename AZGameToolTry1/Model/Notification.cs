using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZGameToolTry1.Model
{
    public class Notification : IEquatable<Notification>
    {
        public DateTime Time { get; set; }
        public NotificationKind Kind { get; set; }
        public String Title { get; set; }
        public String Message { get; set; }

        public override string ToString()
        {
            // NOTE: A char instead of string, does the compiler already optimize it, or would this actually have an effect.
            return Kind.ToString() + ':' + Title + Environment.NewLine + Message;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(Notification other)
        {
            return other != null &&
                   Time == other.Time &&
                   Kind == other.Kind &&
                   Title == other.Title &&
                   Message == other.Message;
        }

        public override int GetHashCode()
        {
            var hashCode = 580559756;
            hashCode = hashCode * -1521134295 + Time.GetHashCode();
            hashCode = hashCode * -1521134295 + Kind.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Title);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Message);
            return hashCode;
        }

        public static bool operator ==(Notification notification1, Notification notification2)
        {
            return EqualityComparer<Notification>.Default.Equals(notification1, notification2);
        }

        public static bool operator !=(Notification notification1, Notification notification2)
        {
            return !(notification1 == notification2);
        }
    }
}