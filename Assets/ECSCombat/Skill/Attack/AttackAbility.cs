using UnityEngine;

public class AttackAbility : BaseAbility
{
    private const string ATTACK_EFFECT = "AttackLine";
    private GameObject asset;
    
    public override void OnInit()
    {
        asset = Resources.Load<GameObject>(ATTACK_EFFECT);
    }

    public override BaseAbilityExecution CreateExecution(BaseActionExecution baseActionExecution)
    {
        var execution = new AttackAbilityExecution();
        execution.actionExecution = baseActionExecution;
        var entity = Contexts.sharedInstance.game.CreateEntity();
        entity.AddAbilityExecution(execution);
        return execution;
    }
    
    public void ShowAttackEffect(GameEntity target)
    {
        var attackEffect = Object.Instantiate(asset);
        attackEffect.transform.position = Vector3.up;
        attackEffect.GetComponent<LineRenderer>().SetPosition(0, owner.view.gameObject.transform.position);
        attackEffect.GetComponent<LineRenderer>().SetPosition(1, target.view.gameObject.transform.position);
        Object.Destroy(attackEffect, 0.05f);
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
        actionExecution.creator.attackAbility.logic.ShowAttackEffect(actionExecution.target);
        Debug.LogError("进行一次普工！");
    }

    public override void OnEndExecute()
    {
        Debug.LogError("结束攻击");
    }
}