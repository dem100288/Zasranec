using Robocode.TankRoyale.BotApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zasranec
{
    public class FireManager
    {
        private Zasranec _bot;
        private BotOpponent targetBot;

 private double MinDifDistToChange = 100;

        public FireManager(Zasranec bot)
        {
            _bot = bot;
        }

        public double GetPowerFire(BotOpponent target)
        {
            //TODO что то не работает формула. Должна увеличивать мощность выстрела с приближением цели
            var power = Math.Max(0.5, -Math.Log2(_bot.DistanceTo(target.X, target.Y)) * 1000d + 1000d);
            //Console.WriteLine(power);
            return power;
        }

        public double CanFire()
        {
            if (targetBot == null) return 0;
            if (_bot.GunHeat == 0)
            {
                return GetPowerFire(targetBot);
            }

            return 0;
        }

        public double TurnGunToTarget()
        {
            if (targetBot == null) return 0;

            var new_direction = _bot.DirectionTo(targetBot.X, targetBot.Y) - _bot.GunDirection;
            //TODO Есть проблема при врашении на некоторые углы, точнее при переходе через ноль
            return Math.Abs(new_direction) > 180 ? (360d - Math.Abs(new_direction)) * (Math.Sign(new_direction) * -1) : new_direction;
        }

        public void botDeath(BotOpponent bot)
        {
            if (bot == targetBot) targetBot = _bot.opponentManager.ClosestBot();
        }

        public void DetectBot(BotOpponent bot)
        {
            var closest = _bot.opponentManager.ClosestBot();
            if (closest == null) return;
            if (targetBot == null)
            {
                targetBot = closest;
            }
            else if (Math.Abs(targetBot.Direction - closest.Distance) > MinDifDistToChange)
            {
                targetBot = closest;
            }
        }
    }
}
