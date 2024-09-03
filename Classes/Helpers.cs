using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lunacod.Classes
{
    internal class Helpers
    {
        public static TimeOnly TimeNow()
        {
            TimeOnly timeNow = TimeOnly.FromDateTime(DateTime.Now);
            return timeNow;
        }
    }
}
