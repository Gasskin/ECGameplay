using cfg.Effect;
using cfg.Effect.Enum;
using UnityEngine;

namespace ECGameplay
{
    public class EffectLifeTimerComponent : Component
    {
        public override bool DefaultEnable { get; set; } = true;
        public IGameTimer LifeTimer { get; set; }
        
        public EffectConfig EffectConfig { get; private set; }

        public override void Awake()
        {
            EffectConfig = Entity.As<EffectAbility>().EffectConfig;

            switch (EffectConfig.EffectTiming)
            {
                case EffectTiming.Immediate:
                    break;
                case EffectTiming.Duration:
                    LifeTimer = new DurationTimer(EffectConfig.Duration);
                    break;
            }
        }

        public void Reset()
        {
            LifeTimer.Reset();
        }

        public override void Update()
        {
            LifeTimer.Update(Time.deltaTime, OnFinish);
        }

        public void SetEffectConfig(EffectConfig effectConfig)
        {
            EffectConfig = effectConfig;
        }

        private void OnFinish()
        {
            
        }
    }
}