using cfg.Skill;

namespace ECGameplay
{
    public class EffectDamageComponent : Component, IEffectTrigger
    {
        public SkillEffectConfig SkillEffectConfig { get; set; }

        public override void Awake()
        {
            SkillEffectConfig = GetEntity<AbilityEffect>().SkillEffectConfig;
        }

        public void OnTriggerApplyEffect(IActionExecution execution)
        {
            var effectActionExecution = (EffectAssignActionExecution)execution;
            if (effectActionExecution == null) 
                return;
            if (GetEntity<AbilityEffect>().OwnerAbility.OwnerEntity.DamageAction.TryMakeAction(out var actionExecution))
            {
                actionExecution.Target = effectActionExecution.Target;
                actionExecution.EffectActionExecution = effectActionExecution;
                actionExecution.ApplyDamage();
            }
        }
    }
}