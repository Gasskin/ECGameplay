using System.Collections.Generic;
using cfg.Skill;

namespace ECGameplay
{
    public class SkillAbility : Entity, IAbility
    {
        public CombatEntity OwnerEntity
        {
            get => GetParent<CombatEntity>();
            set { }
        }
        
        public bool Enable { get; set; }

        public SkillConfig SkillConfig { get; set; }

        public override void Awake(object initData)
        {
            SkillConfig = initData as SkillConfig;
        }

        public void TryActivateAbility()
        {
        }

        public void ActivateAbility()
        {
        }

        public void DeactivateAbility()
        {
        }

        public void EndAbility()
        {
        }

        public Entity CreateExecution()
        {
            var execution = OwnerEntity.AddChild<SkillAbilityExecution>(this);
            execution.Ability = this;
            execution.OwnerEntity = OwnerEntity;
            return execution;
        }
    }

    public class SkillAbilityExecution : Entity, IAbilityExecution
    {
        public IAbility Ability { get; set; }
        public CombatEntity OwnerEntity { get; set; }
        public IActionExecution ActionExecution { get; set; }

        public void BeginExecute()
        {
        }

        public void EndExecute()
        {
        }
    }
}