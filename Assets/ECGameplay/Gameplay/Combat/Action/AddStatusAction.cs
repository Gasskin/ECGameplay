using cfg.Status;

namespace ECGameplay
{
    public class AddStatusAction : Entity,IAction
    {
        public CombatEntity OwnerEntity { get; set; }
        public bool Enable { get; set; }

        public bool TryMakeAction(out AddStatusActionExecution actionExecution)
        {
            if (!Enable)
            {
                actionExecution = null;
            }
            else
            {
                actionExecution = new AddStatusActionExecution();
                actionExecution.Action = this;
                actionExecution.Creator = OwnerEntity;
            }

            return Enable;
        }
    }
    
    public class  AddStatusActionExecution : Entity,IActionExecution
    {
        public IAction Action { get; set; }
        public CombatEntity Creator { get; set; }
        public CombatEntity Target { get; set; }
        
        // 状态配表
        public StatusConfig StatusConfig { get; set; }
        
        public StatusAbility Status { get; set; }

        public void ApplyAddStatus()
        {
            // 不能叠加
            if (!StatusConfig.CanStack)
            {
                if (Target.TryGetStatus(StatusConfig.Id,out var statusAbility))
                {
                    var lifeTimer = statusAbility.GetComponent<StatusLifetimeComponent>().LifeTimer;
                    lifeTimer.MaxTime = StatusConfig.Duration / 1000f;
                    lifeTimer.Reset();
                    return;
                }
            }

            Status = Target.AttachStatus(StatusConfig);
            Status.OwnerEntity = Creator;
            Status.AddComponent<StatusLifetimeComponent>();
            Status.TryActivateAbility();
            
            Creator?.TriggerActionPoint(ActionPointType.AfterGiveStatus, this);
            Target?.TriggerActionPoint(ActionPointType.AfterReceiveStatus, this);
            
            FinishAction();
        }
        
        public void FinishAction()
        {
            Destroy(this);
        }
    }
}