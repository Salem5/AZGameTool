using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AZGameToolTry1.Model
{
   public class ReadMe : BaseMd
    {
        public string Text { get; set; }
        public string FullFileName { get; private set; }
        public DateTime LastUpdate { get; private set; }
    }
}
