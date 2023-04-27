using System.Collections.Generic;
using UnityEngine;

namespace ECGameplay
{
    public class SkillSpellAction : Entity, IAction
    {
        public CombatEntity OwnerEntity {get => GetParent<CombatEntity>();set{}}
        public bool Enable { get; set; }

        public bool TryMakeAction(out SkillSpellActionExecution actionExecution)
        {
            if (!Enable)
            {
                actionExecution = null;
            }
            else
            {
                actionExecution = new SkillSpellActionExecution();
                actionExecution.Action = this;
                actionExecution.Creator = OwnerEntity;
            }
            return Enable;
        }
    }

    public class SkillSpellActionExecution : Entity, IActionExecution
    {
        public IAction Action { get; set; }
        public CombatEntity Creator { get; set; }
        public CombatEntity Target { get; set; }

        public List<Component> Targets { get; set; } = new List<Component>();
        
        public Vector3 Point { get; set; }
        
        public float InputDirection { get; set; }

        public SkillAbilityExecution SkillAbilityExecution { get; set; }
        public SkillAbility SkillAbility { get; set; }
        

        public void SpellSkill()
        {
            Creator.TriggerActionPoint(ActionPointType.BeforeSpell, this);
            
            SkillAbilityExecution = SkillAbility.CreateExecution() as SkillAbilityExecution;
            SkillAbilityExecution.ActionExecution = this;
            SkillAbilityExecution.BeginExecute();
        }
        
        public void FinishAction()
        {
            Creator.TriggerActionPoint(ActionPointType.AfterSpell, this);
            Destroy(this);
        }
    }
}