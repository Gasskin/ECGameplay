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
        
        public SkillConfig SkillConfig { get; set; }

        public override void Awake(object initObject)
        {
            SkillConfig = initObject as SkillConfig;
            // if (initObject is SkillConfig skillConfig)
            // {
                // AddComponent<AbilityEffectComponent>(skillConfig);
            // }
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
            execution.OwnerEntity = OwnerEntity;
            return execution;
        }
    }

    public class AttackAbilityExecution : Entity, IAbilityExecution
    {
        public IAbility Ability { get; set; }
        public CombatEntity OwnerEntity { get; set; }
        public IActionExecution ActionExecution { get; set; }

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
            ActionExecution.FinishAction();
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

            ActionExecution.Creator?.TriggerActionPoint(ActionPointType.BeforeGiveAttackEffect, (AttackActionExecution)ActionExecution);
            ActionExecution.Target?.TriggerActionPoint(ActionPointType.BeforeReceiveAttackEffect, (AttackActionExecution)ActionExecution);

            if (blocked)
            {
                Debug.LogError("被格挡了");
                return;
            }
            
            // var actionExecutions = 
            //     (Ability as AttackAbility)?.GetComponent<AbilityEffectComponent>().CreateAssignActions(AttackActionExecution.Target);
            // if (actionExecutions == null) return;
            // foreach (var actionExecution in actionExecutions)
            //     actionExecution.AssignEffect();
            // (Ability as AttackAbility)?.GetComponent<AbilityEffectComponent>()?.AssignAllAbilityEffect(this);
            var attackAbility = Ability as AttackAbility;
            if (attackAbility != null) 
            {
                foreach (var effect in attackAbility.SkillConfig.AttachEffect_Ref)    
                {
                    if (OwnerEntity.AddEffectAction.TryMakeAction(out var actionExecution))
                    {
                        actionExecution.Target = ActionExecution.Target;
                        actionExecution.EffectConfig = effect;
                        actionExecution.AddEffect();
                    }
                }
            }
            else
            {
                return;
            }
            
            
            Debug.LogError("进行一次普工");
            
            ActionExecution.Creator?.TriggerActionPoint(ActionPointType.AfterGiveAttack, (AttackActionExecution)ActionExecution);
            ActionExecution.Target?.TriggerActionPoint(ActionPointType.AfterReceiveAttack, (AttackActionExecution)ActionExecution);
        }
    }
}