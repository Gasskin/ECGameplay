using UnityEngine;
using Random = System.Random;

public class BlockActionAbility : BaseActionAbility
{
    private Random random = new Random();

    public override void OnInit()
    {
        if (owner.hasActionPoint)
        {
            owner.actionPoint.logic.AddActionPoint(ActionPointType.PreReceiveAttackEffect, TryBlock);
        }
    }

    public override bool TryMakeAction(GameEntity creator, GameEntity target, out BaseActionExecution baseActionExecution)
    {
        baseActionExecution = null;
        // 模拟格挡率
        var probability = random.Next(0, 100);
        if (probability >= 70)
        {
            baseActionExecution = new BlockActionExecution();
            baseActionExecution.creator = creator;
            baseActionExecution.target = target;
            Debug.LogError("格挡成功");
            return true;
        }

        Debug.LogError("格挡失败");
        return false;
    }

    private void TryBlock(BaseActionExecution actionExecution)
    {
        var attackActionExecution = actionExecution as AttackActionExecution;
        if (attackActionExecution == null) 
            return;
        if (TryMakeAction(attackActionExecution.creator,attackActionExecution.target,out var baseActionExecution)
            && baseActionExecution is BlockActionExecution blockActionExecution)
        {
            blockActionExecution.ApplyBlock(attackActionExecution);
        }
    }
}

public class BlockActionExecution: BaseActionExecution
{
    public void ApplyBlock(AttackActionExecution attackActionExecution)
    {
        attackActionExecution.attackAbilityExecution.isBlocked = true;
    }
}