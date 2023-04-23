using cfg.Skill;

namespace ECGameplay
{
    public class DamageEffectComponent : Component, IEffectComponent
    {
        public SkillEffectConfig SkillEffectConfig { get; set; }

        public override void Awake()
        {
            SkillEffectConfig = GetEntity<AbilityEffect>().SkillEffectConfig;
        }

        // public void OnApplyEffect(IActionExecution execution)
        // {
        //     var effectActionExecution = (EffectAssignActionExecution)execution;
        //     if (effectActionExecution == null)
        //         return;
        //     if (GetEntity<AbilityEffect>().OwnerAbility.OwnerEntity.DamageAction.TryMakeAction(out var actionExecution))
        //     {
        //         actionExecution.Target = effectActionExecution.Target;
        //         actionExecution.EffectActionExecution = effectActionExecution;
        //         actionExecution.ApplyDamage();
        //     }
        // }

        public void OnApplyEffect(IAbilityExecution execution, AbilityEffect effect)
        {
            var abilityExecution = (AttackAbilityExecution)execution;
            if (GetEntity<AbilityEffect>().OwnerAbility.OwnerEntity.DamageAction.TryMakeAction(out var actionExecution))
            {
                actionExecution.Target = abilityExecution.AttackActionExecution.Target;
                actionExecution.AbilityEffect = effect;
                actionExecution.ApplyDamage();
            }
        }
    }
}