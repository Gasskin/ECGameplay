using cfg.Effect;
using cfg.Effect.Enum;
using UnityEngine;

namespace ECGameplay
{
    [DrawProperty]
    public class EffectDamageComponent : Component, IEffectComponent
    {
        public override bool DefaultEnable { get; set; } = false;

        private EffectAbility EffectAbility => Entity.As<EffectAbility>();
        private IGameTimer Timer { get; set; }

        public override void OnEnable()
        {
            switch (EffectAbility.EffectConfig.EffectTiming)
            {
                case EffectTiming.Immediate:
                    ApplyDamage();
                    EffectAbility.EndAbility();
                    break;
                case EffectTiming.Duration:
                    break;
                case EffectTiming.Interval:
                    Timer = new IntervalTimer(EffectAbility.EffectConfig.Duration / 1000f,
                        EffectAbility.EffectConfig.Interval / 1000f, ApplyDamage, EffectAbility.EndAbility);
                    break;
            }
        }

        public override void OnDisable()
        {
            Debug.LogError("伤害结束");
        }

        public override void Update()
        {
            Timer?.Update(Time.deltaTime);
        }

        public void Reset()
        {
        }

        private void ApplyDamage()
        {
            if (EffectAbility.AddEffectActionExecution.Creator.DamageAction.TryMakeAction(out var actionExecution))
            {
                actionExecution.Target = EffectAbility.OwnerEntity;
                actionExecution.EffectAbility = EffectAbility;
                actionExecution.ApplyDamage();
            }
        }

#if UNITY_EDITOR
        public override string ToString()
        {
            var config = EffectAbility.EffectConfig;
            return
                $"触发时机: {config.EffectTiming}\n" +
                $"持续时间: {config.Duration}\n" +
                $"触发间隔: {config.Interval}\n" +
                $"触发概率: {config.Probability}\n" +
                $"数值公式: {config.ValueFormula}";
        }
#endif
    }
}