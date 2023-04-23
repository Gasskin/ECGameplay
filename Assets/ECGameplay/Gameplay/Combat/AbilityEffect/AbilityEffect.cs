using cfg.Skill;
using cfg.Skill.Enum;
using cfg.Status.Enum;

namespace ECGameplay
{
    public interface IAbilityEffectComponent
    {
        // void OnApplyEffect(IActionExecution execution);
        void OnApplyEffect(IAbilityExecution execution, AbilityEffect effect);
    }

    
    [DrawProperty]
    public class AbilityEffect : Entity
    {
        public bool Enable { get; set; }

        /// 持有能力效果的Entity，自然是能力本身
        public IAbility OwnerAbility => Parent as IAbility;

        /// 效果配表
        public SkillEffectConfig SkillEffectConfig { get; set; }


        public override void Awake(object initData)
        {
            SkillEffectConfig = initData as SkillEffectConfig;
            if (SkillEffectConfig == null) 
                return;
            switch (SkillEffectConfig.EffectType)
            {
                case EffectType.Damage:
                    AddComponent<DamageAbilityEffectComponent>();
                    break;
                case EffectType.Cure:
                    AddComponent<CureAbilityEffectComponent>();
                    break;
            }

            if (SkillEffectConfig.AttachStatus_Ref!= null)
            {
                switch (SkillEffectConfig.AttachStatus_Ref.StatusType)
                {
                    case StatusType.Cure:
                        break;
                    case StatusType.Damage:
                        break;
                    case StatusType.ActionForbid:
                        break;
                    case StatusType.PropertyModify:
                        break;
                }
            }
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
            foreach (var comp in Components.Values)
            {
                if (comp is IAbilityEffectComponent effectAssignComponent)
                {
                    effectAssignComponent.OnApplyEffect(execution, this);
                }
            }
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
            return $"触发概率：{SkillEffectConfig.Probability * 100}%\n"
                   + $"效果类型：{SkillEffectConfig.EffectType}\n"
                   + $"数值公式：{SkillEffectConfig.ValueFormula}\n"
                   + $"类型：{SkillEffectConfig.ValueType}";
        }
#endif
    }
}