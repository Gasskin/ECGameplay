using cfg.Skill;
using cfg.Skill.Enum;
using ECGamePlay;
using UnityEngine;

namespace ECGameplay
{
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
                    AddComponent<EffectDamageComponent>();
                    break;
                case EffectType.Cure:
                    break;
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

        public EffectAssignActionExecution CreateAssignAction(Entity target)
        {
            if (OwnerAbility.OwnerEntity.EffectAssignAction.TryMakeAction(out var effectAssignActionExecution))
            {
                effectAssignActionExecution.AbilityEffect = this;
                effectAssignActionExecution.AssignTarget = target;
            }

            return effectAssignActionExecution;
        }

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