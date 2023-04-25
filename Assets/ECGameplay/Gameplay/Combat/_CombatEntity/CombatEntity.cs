using System;
using UnityEngine;

namespace ECGameplay
{
    public class CombatEntity : Entity
    {
        // 普攻行为
        public AttackAction AttackAction { get; private set; }
        // 格挡行为
        public BlockAction BlockAction { get; private set; }
        // 伤害行为
        public DamageAction DamageAction { get; private set; }

        public AddEffectAction AddEffectAction { get; private set; }
        
        
        // 普攻能力
        public AttackAbility AttackAbility { get; private set; }
        
        

        public override void Awake()
        {
            AddComponent<AttributeComponent>();
            AddComponent<ActionPointComponent>();
            AddComponent<EffectComponent>();
            
            AttackAction = AttachAction<AttackAction>();
            BlockAction = AttachAction<BlockAction>();
            DamageAction = AttachAction<DamageAction>();
            AddEffectAction = AttachAction<AddEffectAction>();
            // EffectAssignAction = AttachAction<EffectAssignAction>();

            AttackAbility = AttachAbility<AttackAbility>(1);
        }

    #region 行动点

        public void ListenActionPoint(ActionPointType actionPointType, Action<Entity> action)
        {
            GetComponent<ActionPointComponent>().AddListener(actionPointType, action);
        }

        public void UnListenActionPoint(ActionPointType actionPointType, Action<Entity> action)
        {
            GetComponent<ActionPointComponent>().RemoveListener(actionPointType, action);
        }

        public void TriggerActionPoint(ActionPointType actionPointType, Entity action)
        {
            GetComponent<ActionPointComponent>().TriggerActionPoint(actionPointType, action);
        }

    #endregion

    #region 受伤/治疗

        public void ReceiveDamage(IActionExecution actionExecution)
        {
            var damageAction = actionExecution as DamageActionExecution;
            if (damageAction == null)
                return;
            GetComponent<AttributeComponent>().HealthPoint.MinusBaseValue(damageAction.Damage);
        }

    #endregion

    #region 添加能力/状态
        public T AttachAbility<T>(int id) where T : Entity, IAbility
        {
            return AddChild<T>(TableUtil.Tables.SkillTable[id]);
        }

        public EffectAbility AttachEffect(int id)
        {
            return GetComponent<EffectComponent>().AttachEffect(TableUtil.Tables.EffectTable[id]);
        }

        public void RemoveEffect(EffectAbility effect)
        {
            GetComponent<EffectComponent>().RemoveEffect(effect);
        }
        
        public T AttachAction<T>(object config = null) where T : Entity, IAction
        {
            var action = config == null ? AddChild<T>() : AddChild<T>(config);
            action.Enable = true;
            return action;
        }
    #endregion
    }
}