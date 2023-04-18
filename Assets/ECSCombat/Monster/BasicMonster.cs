using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMonster
{
    private const string MODEL = "BasicMonster";

    public GameEntity entity;

    public BasicMonster()
    {
        entity = Contexts.sharedInstance.game.CreateEntity();

        var viewAsset = Resources.Load<GameObject>(MODEL);
        var view = Object.Instantiate(viewAsset);
        entity.AddView(view);
        entity.AddTransform(new Vector3(0, 0, 0), Vector3.one, Vector3.one);
        entity.AddActionPoint(new ActionPoint());
        entity.AddAttackActionAbility(new AttackActionAbility());
        entity.AddAttackAbility(new AttackAbility());
        entity.AddBlockActionAbility(new BlockActionAbility());
    }
    
    public void Move(Vector3 position)
    {
        var trans = entity.transform;
        entity.ReplaceTransform(position, trans.rotation, trans.scale);
    }
}
