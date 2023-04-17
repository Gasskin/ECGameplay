using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : IAbility
{
    public BaseAbilityExecution CreateExecution(BaseActionExecution baseActionExecution)
    {
        var execution = new AttackAbilityExecution();
        execution.actionExecution = baseActionExecution;
        var entity = Contexts.sharedInstance.game.CreateEntity();
        entity.AddAbilityExecution(baseActionExecution.creator, baseActionExecution.target, execution);
        return execution;
    }
}
