namespace ECGameplay
{
    /// <summary>
    /// 能力执行体
    /// </summary>
    public interface IAbilityExecution
    {
        public Entity AbilityEntity { get; set; }
        public Entity OwnerEntity { get; set; }

        /// 开始执行
        public void BeginExecute();

        /// 结束执行
        public void EndExecute();
    }
}