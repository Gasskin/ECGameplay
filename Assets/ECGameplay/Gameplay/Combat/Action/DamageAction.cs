using ECGamePlay;

namespace ECGameplay
{
    public class DamageAction: Entity,IAction
    {
        public CombatEntity OwnerEntity
        {
            get=> GetParent<CombatEntity>();
            set{}
        }
        
        public bool Enable { get; set; }

        public bool TryMakeAction(out DamageActionExecution actionExecution)
        {
            if (!Enable)
            {
                actionExecution = null;
            }
            else
            {
                actionExecution = AddChild<DamageActionExecution>();
                actionExecution.Creator = OwnerEntity;
                actionExecution.Action = this;
            }
            return Enable;
        }
    }

    public class DamageActionExecution : Entity, IActionExecution
    {
        public IAction Action { get; set; }
        public EffectAssignActionExecution EffectActionExecution { get; set; }
        public CombatEntity Creator { get; set; }
        public CombatEntity Target { get; set; }

        public float Damage { get; set; }

        public void ApplyDamage()
        {
            Creator?.TriggerActionPoint(ActionPointType.BeforeCauseDamage, this);
            Target?.TriggerActionPoint(ActionPointType.BeforeReceiveDamage, this);
            
            var skillEffectConfig = EffectActionExecution.AbilityEffect.SkillEffectConfig;
            var attr = Creator?.GetComponent<AttributeComponent>();
            if (attr == null || skillEffectConfig == null) 
                return;

            // 没触发
            if (!MathUtil.PrizeDraw(skillEffectConfig.Probability))
                return;
            
            Damage = (float)ExpressionUtil.TryEvaluate(skillEffectConfig.ValueFormula, attr);
            var isCritical = MathUtil.PrizeDraw(attr.CriticalProbability.Value);
            if (isCritical && skillEffectConfig.CanCritical)
            {
                Damage *= attr.CriticalDamage.Value;
            }
            
            Target?.ReceiveDamage(this);
            
            Creator?.TriggerActionPoint(ActionPointType.AfterCauseDamage, this);
            Target?.TriggerActionPoint(ActionPointType.AfterReceiveDamage, this);
            
            FinishAction();
        }
        
        public void FinishAction()
        {
            Destroy(this);
        }
    }
}