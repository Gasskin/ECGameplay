using System.Collections.Generic;
using cfg.Effect;

namespace ECGameplay
{
    public class EffectComponent : Component
    {
        public CombatEntity OwnerEntity => Entity.As<CombatEntity>();
        public List<EffectAbility> Effects { get; set; } = new List<EffectAbility>();
        public Dictionary<int,List<EffectAbility>> Id2Effects { get; set; } = new Dictionary<int, List<EffectAbility>>();

        public EffectAbility AttachEffect(EffectConfig effectConfig)
        {
            var effect = OwnerEntity.AddChild<EffectAbility>(effectConfig);
            if (!Id2Effects.ContainsKey(effectConfig.Id))
            {
                Id2Effects.Add(effectConfig.Id,new List<EffectAbility>());
            }
            Id2Effects[effectConfig.Id].Add(effect);
            Effects.Add(effect);
            return effect;
        }
        
        
        public bool TryGetEffect(int id,out EffectAbility effect)
        {
            effect = null;
            if (Id2Effects.ContainsKey(id))
            {
                effect = Id2Effects[id][0];
                return true;
            }
            return false;
        }
    }
}