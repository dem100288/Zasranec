using Robocode.TankRoyale.BotApi.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zasranec
{
    public class BotOpponent : IComparable<BotOpponent>
    {
        public double Distance { get; set; }
        public int ScannedByBotId { get; set; }
        public int ScannedBotId { get; set; }
        public double Energy { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Direction { get; set; }
        public double Speed { get; set; }
        public int TurnUpdate { get; set; }

        int IComparable<BotOpponent>.CompareTo(BotOpponent other)
        {
            if (other.Distance > this.Distance)
                return -1;
            else if (other.Distance == this.Distance)
                return 0;
            else
                return 1;
        }
    }
}
