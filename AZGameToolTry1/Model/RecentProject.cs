using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZGameToolTry1.Model
{
    public class RecentProject : IEquatable<RecentProject>, IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{ Name }; {Date}; {Location}";
        }

        public override bool Equals(object obj)
        {
            if (obj is RecentProject sameTypeObject)
            {
                return base.Equals(sameTypeObject);
            }
            return false;
        }

        public bool Equals(RecentProject other)
        {
            return other != null &&
                   Name == other.Name &&
                   Location == other.Location &&
                   Date == other.Date;
        }

        public override int GetHashCode()
        {
            var hashCode = -2026861419;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Location);
            hashCode = hashCode * -1521134295 + Date.GetHashCode();
            return hashCode;
        }

        /// <summary>
        /// Compares the date of the projects to decide what's newer.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is RecentProject otherProject)
            {
                return this.Date.CompareTo(otherProject.Date);
            }
            throw new ArgumentException($"Object from parameter is a { obj.GetType()} and not a {this.GetType()}");
        }

        public static bool operator ==(RecentProject project1, RecentProject project2)
        {
            return EqualityComparer<RecentProject>.Default.Equals(project1, project2);
        }

        public static bool operator !=(RecentProject project1, RecentProject project2)
        {
            return !(project1 == project2);
        }
    }
}