﻿namespace ECGameplay
{
    public class AttackAction : Entity, IAction
    {
        public CombatEntity OwnerEntity {get => GetParent<CombatEntity>();set{}}
        public bool Enable { get; set; }

        public bool TryMakeAction(out AttackActionExecution action)
        {
            if (Enable == false)
            {
                action = null;
            }
            else
            {
                action = OwnerEntity.AddChild<AttackActionExecution>();
                action.Action = this;
                action.Creator = OwnerEntity;
            }

            return Enable;
        }
    }

    public class AttackActionExecution : Entity, IActionExecution
    {
        // 行为
        public IAction Action { get; set; }
        // 释放者
        public CombatEntity Creator { get; set; }
        // 目标
        public CombatEntity Target { get; set; }
        // 能力执行体
        public AttackAbilityExecution AttackAbilityExecution { get; set; }
        public void ApplyAttack()
        {
            var attackAbility = Creator.GetChild<AttackAbility>();
            AttackAbilityExecution = attackAbility.CreateExecution().As<AttackAbilityExecution>();
            AttackAbilityExecution.ActionExecution = this;
            AttackAbilityExecution.BeginExecute();
        }
        
        public void FinishAction()
        {
            Destroy(this);
        }
    }
}