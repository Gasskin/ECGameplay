namespace ECGameplay
{
    public class AttackAction : Entity, IAction
    {
        public Entity OwnerEntity { get; set; }
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
        public Entity Action { get; set; }
        // 释放者
        public Entity Creator { get; set; }
        // 目标
        public Entity Target { get; set; }
        // 能力执行体
        public AttackAbilityExecution AttackAbilityExecution { get; set; }

        public void ApplyAttack()
        {
            var attackAbility = Creator.GetChild<AttackAbility>();
            AttackAbilityExecution = attackAbility.CreateExecution().As<AttackAbilityExecution>();
            AttackAbilityExecution.AttackActionExecution = this;
            AttackAbilityExecution.BeginExecute();
        }
        
        public void FinishAction()
        {
            Destroy(this);
        }
    }
}