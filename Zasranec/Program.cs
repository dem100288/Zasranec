using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Robocode.TankRoyale.BotApi;
using Robocode.TankRoyale.BotApi.Events;

namespace Zasranec
{
    public class Zasranec : Bot
    {
        static async Task Main(string[] args)
        {
            // Для запуска симулятором
            new Zasranec().Start();
            // Для запуска из студии. Должен быть задан в симуляторе соответствующий пароль
            //new Zasranec(true).Start();
        }

        internal OpponentManager opponentManager;
        internal FireManager fireManager;
        internal MoveManager moveManager;
        private double MaxTurnRadar = 90;
        private bool Death;

        Zasranec() : base(BotInfo.FromFile("Zasranec.json"))
        {
            AfterCreate();
        }

        Zasranec(bool withRemote) : base(BotInfo.FromFile("Zasranec.json"), new Uri("ws://localhost:7654"), "0000")
        {
            AfterCreate();
        }

        private void AfterCreate()
        {
            opponentManager = new OpponentManager(this);
            fireManager = new FireManager(this);
            moveManager = new MoveManager(this);
        }

        public override void Run()
        {
            BulletColor = Color.Red;
            BodyColor = Color.Fuchsia;
            while (IsRunning && !Death)
            {
                opponentManager.TurnUpdate(TurnNumber);
                SetTurnLeft(moveManager.GetTurn());
                SetTurnGunLeft(fireManager.TurnGunToTarget());
                SetTurnRadarLeft(RadarTurnRemaining == 0 ? TurnRadarEnd() : RadarTurnRemaining);
                SetForward(moveManager.GetMove());
                SetFire(fireManager.CanFire());
                Go();
            }
        }

        private double TurnRadarEnd()
        {
            var turnRadar = MaxTurnRadar;
            return turnRadar;
        }

        #region Events
        public override void OnRoundEnded(RoundEndedEvent roundEndedEvent)
        {
            opponentManager.ClearInfoOpponents();
        }

        public override void OnBotDeath(DeathEvent botDeathEvent)
        {
            opponentManager.BotDeath(botDeathEvent.VictimId);
        }

        public override void OnDeath(DeathEvent botDeathEvent)
        {
            Death = true;
        }

        public override void OnRoundStarted(RoundStartedEvent roundStatedEvent)
        {
            Death = false;
        }

        public override void OnScannedBot(ScannedBotEvent scannedBotEvent)
        {
            opponentManager.BotDetect(scannedBotEvent);
        }
        #endregion
    }
}
