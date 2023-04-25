using cfg.Effect;
using cfg.Effect.Enum;

namespace ECGameplay
{
    public interface IEffectComponent
    {
        public void Reset();
    }

    public class EffectAbility : Entity
    {
        public CombatEntity OwnerEntity
        {
            get=> Parent.As<CombatEntity>(); 
            set{}
        }
        public EffectConfig EffectConfig { get; set; }
        public AddEffectActionExecution AddEffectActionExecution { get; set; }
        public Component EffectComponent { get; set; }

        public override void Awake(object initData)
        {
            EffectConfig = initData as EffectConfig;

            if (EffectConfig == null)
                return;

            switch (EffectConfig.EffectType)
            {
                case EffectType.Damage:
                    EffectComponent = AddComponent<EffectDamageComponent>();
                    break;
                case EffectType.Cure:
                    EffectComponent = AddComponent<EffectCureComponent>();
                    break;
            }
        }

        public void ActivateAbility()
        {
            EffectComponent.Enable = true;
        }

        public void DeactivateAbility()
        {
            EffectComponent.Enable = false;
        }
        
        public void EndAbility()
        {
            OwnerEntity.RemoveEffect(EffectConfig.Id);
        }

        public void Reset()
        {
            (EffectComponent as IEffectComponent)?.Reset();
        }
    }
}