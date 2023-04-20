using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace ECGameplay
{
    [Flags]
    public enum ActionPointType
    {
        [LabelText("（空）")]
        None = 0,

        [LabelText("造成伤害前")]
        PreCauseDamage = 1 << 1,
        [LabelText("承受伤害前")]
        PreReceiveDamage = 1 << 2,

        [LabelText("造成伤害后")]
        PostCauseDamage = 1 << 3,
        [LabelText("承受伤害后")]
        PostReceiveDamage = 1 << 4,

        [LabelText("给予治疗后")]
        PostGiveCure = 1 << 5,
        [LabelText("接受治疗后")]
        PostReceiveCure = 1 << 6,

        [LabelText("赋给技能效果")]
        AssignEffect = 1 << 7,
        [LabelText("接受技能效果")]
        ReceiveEffect = 1 << 8,

        [LabelText("赋加状态后")]
        PostGiveStatus = 1 << 9,
        [LabelText("承受状态后")]
        PostReceiveStatus = 1 << 10,

        [LabelText("给予普攻前")]
        PreGiveAttack = 1 << 11,
        [LabelText("给予普攻后")]
        PostGiveAttack = 1 << 12,

        [LabelText("遭受普攻前")]
        PreReceiveAttack = 1 << 13,
        [LabelText("遭受普攻后")]
        PostReceiveAttack = 1 << 14,

        [LabelText("起跳前")]
        PreJumpTo= 1 << 15,
        [LabelText("起跳后")]
        PostJumpTo = 1 << 16,

        [LabelText("施法前")]
        PreSpell = 1 << 17,
        [LabelText("施法后")]
        PostSpell = 1 << 18,

        [LabelText("赋给普攻效果前")]
        PreGiveAttackEffect = 1 << 19,
        [LabelText("赋给普攻效果后")]
        PostGiveAttackEffect = 1 << 20,
        [LabelText("承受普攻效果前")]
        PreReceiveAttackEffect = 1 << 21,
        [LabelText("承受普攻效果后")]
        PostReceiveAttackEffect = 1 << 22,

        Max,
    }
    
    [DrawProperty]
    public sealed class ActionPointComponent : Component
    {
        public Dictionary<ActionPointType, ActionPoint> ActionPoints { get; set; } = new Dictionary<ActionPointType, ActionPoint>();


        public void AddListener(ActionPointType actionPointType, Action<Entity> action)
        {
            if (!ActionPoints.ContainsKey(actionPointType))
            {
                ActionPoints.Add(actionPointType, new ActionPoint());
            }
            ActionPoints[actionPointType].AddListener(action);
        }

        public void RemoveListener(ActionPointType actionPointType, Action<Entity> action)
        {
            if (ActionPoints.ContainsKey(actionPointType))
            {
                ActionPoints[actionPointType].RemoveListener(action);
            }
        }

        public ActionPoint GetActionPoint(ActionPointType actionPointType)
        {
            if (ActionPoints.TryGetValue(actionPointType, out var actionPoint)) ;
            return actionPoint;
        }

        public void TriggerActionPoint(ActionPointType actionPointType, Entity actionExecution)
        {
            if (ActionPoints.TryGetValue(actionPointType, out var actionPoint))
            {
                actionPoint.TriggerAllActions(actionExecution);
            }
        }

#if UNITY_EDITOR
        public override string ToString()
        {
            var str = "";
            var firstLine = true;
            foreach (var actionPoint in ActionPoints)
            {
                if (actionPoint.Value.Listeners.Count > 0)
                {
                    str += firstLine? $"{actionPoint.Key}" : $"\n{actionPoint.Key}";
                    firstLine = false;
                }
            }
            return str;
        }
#endif
    }
}