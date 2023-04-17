using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility
{
    public GameEntity owner;

    public void Init(GameEntity owner)
    {
        this.owner = owner;
        OnInit();
    }
    
    public abstract void OnInit();
    
    /// 创建能力执行体
    public abstract BaseAbilityExecution CreateExecution(BaseActionExecution baseActionExecution);
}
