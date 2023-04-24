using cfg.Effect;
using cfg.Effect.Enum;

namespace ECGameplay
{
    public interface IEffectComponent
    {
        // void OnApplyEffect(IActionExecution execution);
        void OnApplyEffect(IAbilityExecution execution);
    }

    
    [DrawProperty]
    public class AbilityEffect : Entity
    {
        public bool Enable { get; set; }

        /// 持有能力效果的Entity，自然是能力本身
        public IAbility OwnerAbility => Parent as IAbility;

        /// 效果配表
        public EffectConfig EffectConfig { get; set; }


        public override void Awake(object initData)
        {
            EffectConfig = initData as EffectConfig;
        }

        public override void OnDestroy()
        {
            DisableEffect();
        }

        public void EnableEffect()
        {
            Enable = true;
            foreach (var comp in Components.Values)
            {
                comp.Enable = true;
            }
        }

        public void DisableEffect()
        {
            Enable = false;
            foreach (var comp in Components.Values)
            {
                comp.Enable = false;
            }
        }

        public void AssignEffect(IAbilityExecution execution)
        {
            // foreach (var comp in Components.Values)
            // {
            //     if (comp is IEffectComponent effectAssignComponent)
            //     {
            //         effectAssignComponent.OnApplyEffect(execution);
            //     }
            // }
        }

        // public EffectAssignActionExecution CreateAssignAction(Entity target)
        // {
        //     if (OwnerAbility.OwnerEntity.EffectAssignAction.TryMakeAction(out var effectAssignActionExecution))
        //     {
        //         effectAssignActionExecution.AbilityEffect = this;
        //         effectAssignActionExecution.AssignTarget = target;
        //     }
        //
        //     return effectAssignActionExecution;
        // }

#if UNITY_EDITOR
        public override string ToString()
        {
            return $"触发概率：{EffectConfig.Probability * 100}%\n"
                   + $"效果类型：{EffectConfig.EffectType}\n"
                   + $"数值公式：{EffectConfig.ValueFormula}\n"
                   + $"类型：{EffectConfig.ValueType}";
        }
#endif
    }
}