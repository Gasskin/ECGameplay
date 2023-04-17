using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionAbility : BaseActionAbility
{
    private const float CD = 1f;
    private float preTime = -1;
    
    public override bool TryMakeAction(GameEntity owner,out BaseActionExecution baseActionExecution)
    {
        // 这里还可以判断各种条件
        var cur = Time.realtimeSinceStartup;
        if (cur - preTime < CD)
        {
            baseActionExecution = null;
            Debug.LogError("技能冷却中");
            return false;
        }
        preTime = cur;
        baseActionExecution = new AttackActionExecution();
        baseActionExecution.creator = owner;
        return true;
    }
}
