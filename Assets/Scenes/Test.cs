using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject cube;
    private GameEntity player;

    private GameEntity target;
    
    private void Start()
    {
        target = Contexts.sharedInstance.game.CreateEntity();
        
        player = Contexts.sharedInstance.game.CreateEntity();
        var go = Instantiate(cube);
        player.AddView(go);
        player.AddActionPoint(new ActionPoint());
        player.AddAttackActionAbility(new AttackBaseActionAbility());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (player.attackActionAbility.logic.TryMakeAction(player,out var result) && result is AttackActionExecution attackActionExecution)
            {
                attackActionExecution.Target = target;
                attackActionExecution.StartAttack();
            }
        }
    }
}
