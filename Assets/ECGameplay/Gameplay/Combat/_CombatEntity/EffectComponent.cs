using System.Collections.Generic;
using cfg.Effect;

namespace ECGameplay
{
    public class EffectComponent : Component
    {
        public CombatEntity OwnerEntity => Entity.As<CombatEntity>();
        public Dictionary<int,List<EffectAbility>> Id2Effects { get; set; } = new Dictionary<int, List<EffectAbility>>();

        public EffectAbility AttachEffect(EffectConfig effectConfig)
        {
            var effect = OwnerEntity.AddChild<EffectAbility>(effectConfig);
            if (!Id2Effects.ContainsKey(effectConfig.Id))
            {
                Id2Effects.Add(effectConfig.Id,new List<EffectAbility>());
            }
            Id2Effects[effectConfig.Id].Add(effect);
            return effect;
        }

        public void RemoveEffect(EffectAbility effect)
        {
            var id = effect.EffectConfig.Id;
            if (Id2Effects.TryGetValue(id,out var list))
            {
                list.Remove(effect);
                Entity.Destroy(effect);
            }
        }
        
        public bool TryGetEffect(int id,out EffectAbility effect)
        {
            effect = null;
            if (Id2Effects.TryGetValue(id,out var list) && list.Count > 0)
            {
                effect = list[0];
                return true;
            }
            return false;
        }
    }
}