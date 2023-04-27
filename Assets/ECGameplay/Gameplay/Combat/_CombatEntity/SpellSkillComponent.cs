using UnityEngine;

namespace ECGameplay
{
    public class SpellSkillComponent : Component
    {
        private CombatEntity CombatEntity => GetEntity<CombatEntity>();
        public override bool DefaultEnable { get; set; } = true;
        
        public void SpellWithTarget(SkillAbility spellSkill, CombatEntity targetEntity)
        {
            if (CombatEntity.SkillAbility != null)
                return;

            if (CombatEntity.SkillSpellAction.TryMakeAction(out var actionExecution))
            {
                actionExecution.SkillAbility = spellSkill;
                actionExecution.Target = targetEntity;
                actionExecution.Point = targetEntity.Position;
                spellSkill.OwnerEntity.Rotation = Quaternion.LookRotation(targetEntity.Position - spellSkill.OwnerEntity.Position);
                actionExecution.InputDirection = spellSkill.OwnerEntity.Rotation.eulerAngles.y;
                actionExecution.SpellSkill();
            }
        }

        public void SpellWithPoint(SkillAbility spellSkill, Vector3 point)
        {
            if (CombatEntity.SkillAbility != null)
                return;

            if (CombatEntity.SkillSpellAction.TryMakeAction(out var actionExecution))
            {
                actionExecution.SkillAbility = spellSkill;
                actionExecution.Point = point;
                spellSkill.OwnerEntity.Rotation = Quaternion.LookRotation(point - spellSkill.OwnerEntity.Position);
                actionExecution.InputDirection = spellSkill.OwnerEntity.Rotation.eulerAngles.y;
                actionExecution.SpellSkill();
            }
        }
    }
}