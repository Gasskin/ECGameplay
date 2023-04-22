namespace ECGameplay
{
    /// <summary>
    /// 格挡行为
    /// </summary>
    public class BlockAction : Entity,IAction
    {
        public CombatEntity OwnerEntity
        {
            get=> GetParent<CombatEntity>();
            set{}
        }
        
        public bool Enable { get; set; }

        public override void Awake()
        {
            OwnerEntity?.ListenActionPoint(ActionPointType.BeforeReceiveAttackEffect, TryBlock);
        }

        
        private void TryBlock(Entity actionExecution)
        {
            var flag = MathUtil.PrizeDraw(0.3f);
            if (flag)
            {
                var attackExecution = actionExecution.As<AttackActionExecution>();
                attackExecution.AttackAbilityExecution.SetBlocked();
            }
        }
    }
}