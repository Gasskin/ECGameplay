using UnityEngine;

namespace ECGameplay
{
    public class StatusLifetimeComponent : Component
    {
        public override bool DefaultEnable { get; set; } = true;
        
        public GameTimer LifeTimer { get; set; }

        public override void Awake()
        {
            var lifeTime = GetEntity<StatusAbility>().StatusConfig.Duration / 1000f;
            LifeTimer = new GameTimer(lifeTime, OnFinish);
        }

        public override void Update()
        {
            if (LifeTimer.IsRunning)
            {
                LifeTimer.UpdateAsFinish(Time.deltaTime);
            }
        }

        private void OnFinish()
        {
            
        }
    }
}