using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActionAbility
{
    public abstract bool TryMakeAction(GameEntity owner,out BaseActionExecution baseActionExecution);
}
