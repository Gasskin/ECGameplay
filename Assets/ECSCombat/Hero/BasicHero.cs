using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHero
{
    private const string MODEL = "BasicHero";

    public GameEntity entity;

    public BasicHero()
    {
        entity = Contexts.sharedInstance.game.CreateEntity();

        var viewAsset = Resources.Load<GameObject>(MODEL);
        var view = Object.Instantiate(viewAsset);
        entity.AddView(view);
        entity.AddTransform(new Vector3(0, 0, 0), Vector3.one, Vector3.one);
        entity.AddActionPoint(new ActionPoint());
        entity.AddAttackActionAbility(new AttackActionAbility());
        entity.AddAttackAbility(new AttackAbility());
    }

    public void Attack(GameEntity target)
    {
        if (target == null) 
            return;
        if (entity.attackActionAbility.logic.TryMakeAction(entity, target, out var actionExecution) 
            && actionExecution is AttackActionExecution attackActionExecution)
        {
            attackActionExecution.StartAttack();
        }
    }
    
    public void Move(Vector3 position)
    {
        var trans = entity.transform;
        entity.ReplaceTransform(position, trans.rotation, trans.scale);
    }
}