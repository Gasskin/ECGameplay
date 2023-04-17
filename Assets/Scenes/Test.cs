using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject playerAsset;
    public GameObject enemyAsset;

    private GameEntity player;
    private GameEntity enemy;

    private void Start()
    {
        player = Contexts.sharedInstance.game.CreateEntity();
        var go = Instantiate(playerAsset);
        player.AddView(go);
        player.AddTransform(new Vector3(5, 0, 0), Vector3.one, Vector3.one);
        player.AddActionPoint(new ActionPoint());
        player.AddAttackActionAbility(new AttackActionAbility());
        player.AddAttackAbility(new AttackAbility());

        enemy = Contexts.sharedInstance.game.CreateEntity();
        go = Instantiate(enemyAsset);
        enemy.AddView(go);
        enemy.AddActionPoint(new ActionPoint());
        enemy.AddTransform(new Vector3(-5, 0, 0), Vector3.one, Vector3.one);
        enemy.AddBlockActionAbility(new BlockActionAbility());
    }

    public void Attack()
    {
        if (player.attackActionAbility.logic.TryMakeAction(player, enemy, out var actionExecution) 
            && actionExecution is AttackActionExecution attackActionExecution)
        {
            attackActionExecution.StartAttack();
        }
    }
}
