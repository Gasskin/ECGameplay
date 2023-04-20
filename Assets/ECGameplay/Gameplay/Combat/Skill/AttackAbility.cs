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


        public override void Awake()
        {
            // todo 暂时屏蔽
            // var effects = new List<Effect>();
            // var damageEffect = new DamageEffect();
            // damageEffect.Enabled = true;
            // damageEffect.AddSkillEffectTargetType = AddSkillEffetTargetType.SkillTarget;
            // damageEffect.EffectTriggerType = EffectTriggerType.Condition;
            // damageEffect.CanCrit = true;
            // damageEffect.DamageType = DamageType.Physic;
            // damageEffect.DamageValueFormula = $"自身攻击力";
            // effects.Add(damageEffect);
            // AddComponent<AbilityEffectComponent>(effects);
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
            execution.AbilityEntity = this;
            return execution;
        }
    }

    public class AttackAbilityExecution : Entity, IAbilityExecution
    {
        public Entity AbilityEntity { get; set; }
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

            AttackActionExecution.Creator?.TriggerActionPoint(ActionPointType.PreGiveAttackEffect, AttackActionExecution);
            AttackActionExecution.Target?.TriggerActionPoint(ActionPointType.PreReceiveAttackEffect, AttackActionExecution);

            if (blocked)
            {
                Debug.LogError("被格挡了");
            }
            else
            {
                // var effectAssigns = AbilityEntity.GetComponent<AbilityEffectComponent>().CreateAssignActions(AttackAction.Target);
                // foreach (var item in effectAssigns)
                // {
                //     item.AssignEffect();
                // }
                Debug.LogError("进行一次普工");
            }
        }
    }
}