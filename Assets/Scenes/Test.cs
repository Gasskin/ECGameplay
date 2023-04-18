using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject playerAsset;
    public GameObject enemyAsset;

    private BasicHero hero;
    
    private BasicMonster monster;

    private void Start()
    {
        hero = new BasicHero();
        hero.Move(new Vector3(5,0,0));
        
        monster = new BasicMonster();
        monster.Move(new Vector3(-5,0,0));
    }

    public void Attack()
    {
        hero.Attack(monster.entity);
    }
}
