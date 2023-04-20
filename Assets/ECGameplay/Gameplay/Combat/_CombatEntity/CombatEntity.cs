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
        // 普攻能力
        public AttackAbility AttackAbility { get; private set; }

        public override void Awake()
        {
            AddComponent<AttributeComponent>();
            AddComponent<ActionPointComponent>();

            AttackAction = AttachAction<AttackAction>();
            BlockAction = AttachAction<BlockAction>();
            
            AttackAbility = AttachAbility<AttackAbility>();
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

        private T AttachAbility<T>(object config = null) where T : Entity, IAbility
        {
            var ability = config == null ? AddChild<T>() : AddChild<T>(config);
            return ability;
        }
        
        private T AttachAction<T>(object config = null) where T : Entity, IAction
        {
            var action = config == null ? AddChild<T>() : AddChild<T>(config);
            action.Enable = true;
            return action;
        }
    }
}