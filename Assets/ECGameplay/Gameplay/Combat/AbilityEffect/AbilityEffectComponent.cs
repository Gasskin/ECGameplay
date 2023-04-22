﻿using System.Collections.Generic;
using cfg.Skill;

namespace ECGameplay
{
    public class AbilityEffectComponent : Component
    {
        /// 默认不激活
        public override bool DefaultEnable { get; set; } = false;

        /// 效果列表，一个能力可以有多个效果
        public List<AbilityEffect> AbilityEffects { get; set; } = new List<AbilityEffect>();

        public override void Awake(object initData)
        {
            // 技能配表
            if (initData is SkillConfig skillConfig)
            {
                // 创建该技能的所有效果
                foreach (var skillEffect in skillConfig.AttachEffect_Ref)
                {
                    var abilityEffect = Entity.AddChild<AbilityEffect>(skillEffect);
                    AbilityEffects.Add(abilityEffect);
                }
            }
        }

        public override void OnEnable()
        {
            foreach (var abilityEffect in AbilityEffects)
            {
                abilityEffect.EnableEffect();
            }
        }

        public override void OnDisable()
        {
            foreach (var abilityEffect in AbilityEffects)
            {
                abilityEffect.DisableEffect();
            }
        }

        public EffectAssignActionExecution CreateAssignActionByIndex(Entity targetEntity, int index)
        {
            if (AbilityEffects.Count <= index)
                return null;
            return AbilityEffects[index].CreateAssignAction(targetEntity);;
        }

        public List<EffectAssignActionExecution> CreateAssignActions(Entity targetEntity)
        {
            var list = new List<EffectAssignActionExecution>();
            foreach (var abilityEffect in AbilityEffects)
            {
                var effectAssign = abilityEffect.CreateAssignAction(targetEntity);
                if (effectAssign != null)
                {
                    list.Add(effectAssign);
                }
            }
            return list;
        }
    }
}