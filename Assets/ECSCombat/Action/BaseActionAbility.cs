using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActionAbility
{
    public GameEntity owner;

    public void Init(GameEntity owner)
    {
        this.owner = owner;
        OnInit();
    }

    public abstract void OnInit();
    
    public abstract bool TryMakeAction(GameEntity creator,GameEntity target,out BaseActionExecution baseActionExecution);
}
