using cfg.Effect;
using cfg.Effect.Enum;

namespace ECGameplay
{
    public class EffectAbility : Entity
    {
        public CombatEntity OwnerEntity { get; set; }
        public bool Enable { get; set; }
        public EffectConfig EffectConfig { get; set; }

        public override void Awake(object initData)
        {
            EffectConfig = initData as EffectConfig;

            if (EffectConfig == null)
                return;

            switch (EffectConfig.EffectType)
            {
                case EffectType.Damage:
                    AddComponent<EffectDamageComponent>();
                    break;
                case EffectType.Cure:
                    AddComponent<EffectCureComponent>();
                    break;
            }
        }

        public void ActivateAbility()
        {
            foreach (var comp in Components.Values)
            {
                comp.Enable = true;
            }
        }

        public void DeactivateAbility()
        {
            foreach (var comp in Components.Values)
            {
                comp.Enable = false;
            }
        }
    }
}