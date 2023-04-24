using cfg.Effect;
using UnityEngine;

namespace ECGameplay
{
    public class EffectDamageComponent : Component
    {
        public override bool DefaultEnable { get; set; } = false;

        public EffectConfig EffectConfig => Entity.As<EffectAbility>().EffectConfig;
        
        public override void OnEnable()
        {
            Debug.LogError(EffectConfig.ValueFormula);
        }
    }
}