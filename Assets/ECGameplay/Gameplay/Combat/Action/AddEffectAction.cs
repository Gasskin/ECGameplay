using cfg.Effect;
using cfg.Effect.Enum;

namespace ECGameplay
{
    public class AddEffectAction: Entity, IAction
    {
        public CombatEntity OwnerEntity
        {
            get=>GetParent<CombatEntity>() ;
            set{}
        }
        
        public bool Enable { get; set; }

        public bool TryMakeAction(out AddEffectActionExecution actionExecution)
        {
            if (!Enable)
            {
                actionExecution= null;
            }
            else
            {
                actionExecution = OwnerEntity.AddChild<AddEffectActionExecution>();
                actionExecution.Action = this;
                actionExecution.Creator = OwnerEntity;
            }
            return Enable;
        }
    }


    public class AddEffectActionExecution : Entity, IActionExecution
    {
        public IAction Action { get; set; }
        public CombatEntity Creator { get; set; }
        public CombatEntity Target { get; set; }

        public EffectConfig EffectConfig { get; set; }

        public EffectAbility Effect { get; set; }

        public void AddEffect()
        {
            if (!EffectConfig.CanStack)
            {
                if (Target.GetComponent<EffectComponent>().TryGetEffect(EffectConfig.Id,out var effect))
                {
                    var lifeTimer = effect.GetComponent<EffectLifeTimerComponent>();
                    lifeTimer.SetEffectConfig(EffectConfig);
                    lifeTimer.Reset();
                    return;
                }
            }

            switch (EffectConfig.EffectTarget)
            {
                case EffectTarget.Target:
                    Effect = Target.AttachEffect(EffectConfig.Id);
                    break;
                case EffectTarget.Self:
                    Effect = Creator.AttachEffect(EffectConfig.Id);
                    break;
            }

            Effect.AddComponent<EffectLifeTimerComponent>();
            Effect.ActivateAbility();
            
            Creator?.TriggerActionPoint(ActionPointType.AfterGiveEffect, this);
            Target?.TriggerActionPoint(ActionPointType.AfterReceiveEffect, this);   
            
            FinishAction();
        }
        
        public void FinishAction()
        {
            Destroy(this);
        }
    }
}


























