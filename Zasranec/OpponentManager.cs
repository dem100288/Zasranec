using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

namespace Zasranec
{
    public class OpponentManager
    {
        private Zasranec _bot;
        private Dictionary<int, BotOpponent> Opponents = new Dictionary<int, BotOpponent>();

        public OpponentManager(Zasranec bot)
        {
            _bot = bot;
        }

        public void ClearInfoOpponents()
        {
            Opponents.Clear();
        }

        public void BotDetect(ScannedBotEvent scannedBotEvent)
        {
            if (!Opponents.ContainsKey(scannedBotEvent.ScannedBotId))
            {
                Opponents.Add(scannedBotEvent.ScannedBotId, new BotOpponent() { ScannedBotId = scannedBotEvent.ScannedBotId });
            }

            var bot = Opponents[scannedBotEvent.ScannedBotId];
            bot.ScannedByBotId = scannedBotEvent.ScannedByBotId;
            bot.Direction = scannedBotEvent.Direction;
            bot.Energy = scannedBotEvent.Energy;
            bot.Speed = scannedBotEvent.Speed;
            bot.X = scannedBotEvent.X;
            bot.Y = scannedBotEvent.Y;
            bot.Distance = _bot.DistanceTo(bot.X, bot.Y);
            bot.TurnUpdate = scannedBotEvent.TurnNumber;
            _bot.fireManager.DetectBot(bot);
        }

        public void TurnUpdate(int currentTurn)
        {
            //TODO обновление положения бота по его прошлой позиции, прошедшего времени, направления и скорости
        }

        public BotOpponent ClosestBot()
        {
            return Opponents.Count > 0 ? Opponents.Values.Min() : null;
        }

        public void BotDeath(int id)
        {
            if (Opponents.Remove(id, out var removeBot))
            {
                _bot.fireManager.botDeath(removeBot);
            }
        }
    }
}
