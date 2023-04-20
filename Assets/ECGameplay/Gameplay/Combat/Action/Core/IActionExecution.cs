namespace ECGameplay
{
    public interface IActionExecution
    {
        /// 行动能力
        public Entity Action { get; set; }
        /// 行动实体
        public CombatEntity Creator { get; set; }
        /// 目标对象
        public CombatEntity Target { get; set; }

        /// 行动结束
        public void FinishAction();
    }
}