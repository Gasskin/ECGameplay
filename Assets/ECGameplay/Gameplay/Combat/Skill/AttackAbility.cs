using cfg.Skill;
using UnityEngine;

namespace ECGameplay
{
    public class AttackAbility : Entity, IAbility
    {
        public CombatEntity OwnerEntity
        {
            get => GetParent<CombatEntity>();
            set { }
        }

        public bool Enable { get; set; }
        
        private SkillConfig SkillConfig { get; set; }

        public override void Awake(object initObject)
        {
            if (initObject is SkillConfig skillConfig)
            {
                AddComponent<AbilityEffectComponent>(skillConfig);
            }
        }

        public void TryActivateAbility()
        {
        }

        public void ActivateAbility()
        {
            Enable = true;
        }

        public void DeactivateAbility()
        {
        }

        public void EndAbility()
        {
        }

        public Entity CreateExecution()
        {
            var execution = OwnerEntity.AddChild<AttackAbilityExecution>(this);
            execution.Ability = this;
            return execution;
        }
    }

    public class AttackAbilityExecution : Entity, IAbilityExecution
    {
        public IAbility Ability { get; set; }
        public CombatEntity OwnerEntity { get; set; }
        public AttackActionExecution AttackActionExecution { get; set; }

        // 被格挡
        private bool blocked;

        // 已经触发伤害
        private bool damaged;

        public void BeginExecute()
        {
            AddComponent<UpdateComponent>();
        }

        public void EndExecute()
        {
            AttackActionExecution.FinishAction();
            Destroy(this);
        }
        
        public void SetBlocked()
        {
            blocked = true;
        }

        public override void Update()
        {
            if (!IsDispose)
            {
                if (!damaged)
                {
                    TryTriggerAttackEffect();
                }
                else
                {
                    EndExecute();
                }
            }
        }
        
        private void TryTriggerAttackEffect()
        {
            damaged = true;

            AttackActionExecution.Creator?.TriggerActionPoint(ActionPointType.BeforeGiveAttackEffect, AttackActionExecution);
            AttackActionExecution.Target?.TriggerActionPoint(ActionPointType.BeforeReceiveAttackEffect, AttackActionExecution);

            if (blocked)
            {
                Debug.LogError("被格挡了");
                return;
            }
            
            var actionExecutions = 
                (Ability as AttackAbility)?.GetComponent<AbilityEffectComponent>().CreateAssignActions(AttackActionExecution.Target);
            if (actionExecutions == null) return;
            foreach (var actionExecution in actionExecutions)
                actionExecution.AssignEffect();
            
            Debug.LogError("进行一次普工");
            
            AttackActionExecution.Creator?.TriggerActionPoint(ActionPointType.AfterGiveAttack, AttackActionExecution);
            AttackActionExecution.Target?.TriggerActionPoint(ActionPointType.AfterReceiveAttack, AttackActionExecution);
        }
    }
}