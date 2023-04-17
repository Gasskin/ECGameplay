using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普攻行动能力
/// </summary>
public class AttackActionAbility : BaseActionAbility
{
    private const float CD = 1f;
    private float preTime = -1;

    public override void OnInit()
    {
        
    }

    public override bool TryMakeAction(GameEntity creator, GameEntity target, out BaseActionExecution baseActionExecution)
    {
        // 这里还可以判断各种条件
        var cur = Time.realtimeSinceStartup;
        if (cur - preTime < CD)
        {
            baseActionExecution = null;
            Debug.LogError("行为冷却中");
            return false;
        }

        preTime = cur;
        baseActionExecution = new AttackActionExecution();
        baseActionExecution.creator = creator;
        baseActionExecution.target = target;
        return true;
    }
}

/// <summary>
/// 普攻行动执行
/// </summary>
public class AttackActionExecution : BaseActionExecution
{
    public AttackAbilityExecution attackAbilityExecution;

    public void StartAttack()
    {
        if (creator.hasAttackAbility)
        {
            attackAbilityExecution = creator.attackAbility.logic.CreateExecution(this) as AttackAbilityExecution;
            attackAbilityExecution?.BeginExecute();
        }
    }
}