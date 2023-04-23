using cfg.Status;

namespace ECGameplay
{
    public class AddStatusAction : Entity,IAction
    {
        public CombatEntity OwnerEntity { get; set; }
        public bool Enable { get; set; }
        
    }
    
    public class  AddStatusActionExecution : Entity,IActionExecution
    {
        public IAction Action { get; set; }
        public CombatEntity Creator { get; set; }
        public CombatEntity Target { get; set; }
        
        // 状态配表
        public StatusConfig StatusConfig { get; set; }

        public void ApplyAddStatus()
        {
            // 不能叠加
            if (!StatusConfig.CanStack)
            {
                
            }
        }
        
        public void FinishAction()
        {
            Destroy(this);
        }
    }
}