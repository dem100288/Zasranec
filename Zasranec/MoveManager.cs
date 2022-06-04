using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zasranec
{
    //TODO тут вообще все плохо, почти не работает весь класс
    public class MoveManager
    {
        private Random rnd = new Random();
        private Zasranec _bot;
        private Point TargetPoint;
        private int MinDistanceToWall = 100;
        private int MinDostanceToTarget = 50;
        private bool NotMove => TargetPoint == Point.Empty || DistanceToTarget() < MinDostanceToTarget;
        public MoveManager(Zasranec bot)
        {
            _bot = bot;
            TargetPoint = Point.Empty;
        }

        public double DistanceToTarget()
        {
            return _bot.DistanceTo(TargetPoint.X, TargetPoint.Y);
        }

        public double GetTurn()
        {
            if (NotMove) NewTargetPoint();
            var new_direction = _bot.DirectionTo(TargetPoint.X, TargetPoint.Y) - _bot.GunDirection;

            return Math.Abs(new_direction) > 180 ? (360d - Math.Abs(new_direction)) * (Math.Sign(new_direction) * -1) : new_direction;
        }

        public double GetMove()
        {
            if (NotMove) NewTargetPoint();
            return DistanceToTarget();
        }

        public void NewTargetPoint()
        {
            TargetPoint.X = rnd.Next(MinDistanceToWall, _bot.ArenaWidth - MinDistanceToWall);
            TargetPoint.Y = rnd.Next(MinDistanceToWall, _bot.ArenaHeight - MinDistanceToWall);
        }
    }
}
