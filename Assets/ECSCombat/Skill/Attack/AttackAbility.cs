using UnityEngine;

public class AttackAbility : BaseAbility
{
    public override void OnInit()
    {
        
    }

    public override BaseAbilityExecution CreateExecution(BaseActionExecution baseActionExecution)
    {
        var execution = new AttackAbilityExecution();
        execution.actionExecution = baseActionExecution;
        var entity = Contexts.sharedInstance.game.CreateEntity();
        entity.AddAbilityExecution(execution);
        return execution;
    }
}

public class AttackAbilityExecution : BaseAbilityExecution
{
    public bool isBlocked = false;

    public override void OnBeginExecute()
    {
        Debug.LogError("开始攻击");
    }

    public override void OnExecute()
    {
        if (actionExecution.creator.hasActionPoint)
        {
            actionExecution.creator.actionPoint.logic.TriggerActionPoint(ActionPointType.PreGiveAttackEffect,
                actionExecution);
        }

        if (actionExecution.target.hasActionPoint)
        {
            actionExecution.target.actionPoint.logic.TriggerActionPoint(ActionPointType.PreReceiveAttackEffect,
                actionExecution);
        }
        
        if (isBlocked)
        {
            Debug.LogError("被格挡了");
            return;
        }

        Debug.LogError("进行一次普工！");
    }

    public override void OnEndExecute()
    {
        Debug.LogError("结束攻击");
    }
}