using System;
using UnityEngine;

namespace ECGameplay
{
    public class CombatEntity : Entity
    {
        public GameObject target;

        // 普攻行为
        public AttackAction AttackAction { get; private set; }
        // 格挡行为
        public BlockAction BlockAction { get; private set; }
        // 效果分配行为
        public EffectAssignAction EffectAssignAction { get; private set; }
        // 伤害行为
        public DamageAction DamageAction { get; private set; }
        
        // 普攻能力
        public AttackAbility AttackAbility { get; private set; }
        
        

        public override void Awake()
        {
            AddComponent<AttributeComponent>();
            AddComponent<ActionPointComponent>();

            AttackAction = AttachAction<AttackAction>();
            BlockAction = AttachAction<BlockAction>();
            DamageAction = AttachAction<DamageAction>();
            EffectAssignAction = AttachAction<EffectAssignAction>();

            AttackAbility = AttachAbility<AttackAbility>(1);
        }

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

        public void ReceiveDamage(IActionExecution actionExecution)
        {
            var damageAction = actionExecution as DamageActionExecution;
            if (damageAction == null)
                return;
            Debug.LogError("ReceiveDamage : " + damageAction.Damage);
        }
        
        private T AttachAbility<T>(int id) where T : Entity, IAbility
        {
            if (TableUtil.Tables.SkillTable.DataMap.TryGetValue(id, out var config))
            {
                return AddChild<T>(config);
            }

            Debug.LogError("AttachAbility Error : " + id);
            return null;
        }

        private T AttachAction<T>(object config = null) where T : Entity, IAction
        {
            var action = config == null ? AddChild<T>() : AddChild<T>(config);
            action.Enable = true;
            return action;
        }
    }
}