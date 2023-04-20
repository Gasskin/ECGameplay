namespace ECGameplay
{
    /// <summary>
    /// 行动
    /// </summary>
    public interface IAction
    {
        public Entity OwnerEntity { get; set; }
        public bool Enable { get; set; }
    }
}