namespace ECGameplay
{
    public interface IActionExecution
    {
        /// 行动能力
        public IAction Action { get; set; }
        /// 效果赋给行动
        // public EffectAssignActionExecution EffectActionExecution { get; set; }
        public AbilityEffect AbilityEffect { get; set; }
        /// 行动实体
        public CombatEntity Creator { get; set; }
        /// 目标对象
        public CombatEntity Target { get; set; }

        /// 行动结束
        public void FinishAction();
    }
}