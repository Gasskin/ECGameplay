namespace ECGameplay
{
    /// <summary>
    /// 能力
    /// </summary>
    public interface IAbility
    {
        public Entity OwnerEntity { get; set; }
        public Entity ParentEntity { get; }
        public bool Enable { get; set; }


        /// 尝试激活能力
        public void TryActivateAbility();

        /// 激活能力
        public void ActivateAbility();

        /// 禁用能力
        public void DeactivateAbility();

        /// 结束能力
        public void EndAbility();

        /// 创建能力执行体
        public Entity CreateExecution();
    }
}