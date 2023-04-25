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
            
        }

        public void Reset()
        {
            LifeTimer.Reset();
        }

        public override void Update()
        {
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