using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointSystem.Services
{
    public class TimeService
    {
        public TimeService()
        {
            Time = System.DateTime.Now.ToString("yyyyMMddTHH:hh:mm:ss");
            DtNow = System.DateTime.Now;
        }
        public string Time { get; }
        public DateTime DtNow { get; }

    }
}
