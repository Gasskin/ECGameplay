using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActionExecution : BaseActionExecution
{
    public AttackAbilityExecution attackAbilityExecution;
    
    public void StartAttack()
    {
        if (creator.hasAttackAbility)
        {
            attackAbilityExecution = creator.attackAbility.logic.CreateExecution(this) as AttackAbilityExecution;
            attackAbilityExecution.BeginExecute();
        }
    }
}
