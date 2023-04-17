using System;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;
using EventType = Entitas.CodeGeneration.Attributes.EventType;

#region ID
[Game]
public class IdentityComponent : IComponent
{
    public int id;
}
#endregion

#region Property
[Game]
public class BasePropertyComponent : IComponent
{
    // 基础属性
    public float baseHealth;
    public float baseAttack;

    public float baseDefence;

    // 增加的固定属性
    public float healthAdd;
    public float attackAdd;

    public float defenceAdd;

    // 增加的百分比属性
    public float healthPctAdd;
    public float attackPctAdd;

    public float defencePctAdd;

    // 最终属性
    public float health;
    public float attack;
    public float defence;
}

[Game]
public class PropertyComponent : IComponent
{
    // 目标
    public GameEntity target;

    // 是否启用
    public bool enable;

    // 固定生命数值
    public float healthAdd;

    // 百分比生命数值
    public float healthPctAdd;

    // 固定攻击数值
    public float attackAdd;

    // 百分比攻击数值
    public float attackPctAdd;

    // 固定防御数值
    public float defenceAdd;

    // 百分比防御数值
    public float defencePctAdd;
}
#endregion

#region View
[Game]
public class ViewComponent : IComponent
{
    public GameObject gameObject;
}

[Game]
public class TransformComponent : IComponent
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;
}
#endregion

#region Combat
[Game, Event(EventTarget.Self, EventType.Removed)]
public class CombatLogicComponent : IComponent
{
    public CombatLogic logic;
}

[Game]
public class ActionPointComponent : IComponent
{
    public ActionPoint logic;
}

[Game]
public class AttackActionAbilityComponent : IComponent
{
    public AttackBaseActionAbility logic;
}

[Game]
public class AttackAbilityComponent : IComponent
{
    public AttackAbility logic;
}

[Game]
public class AbilityExecutionComponent : IComponent
{
    public GameEntity creator;
    public GameEntity target;
    public BaseAbilityExecution logic;
}
#endregion























