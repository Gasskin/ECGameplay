using System;
using ECGameplay;
using UnityEngine;

public class Test : MonoBehaviour
{

    public GameObject hero;
    public GameObject monster;

    private MasterEntity Master => MasterEntity.Instance;
    private CombatEntity HeroEntity;
    private CombatEntity MonsterEntity;
    
    
    private void Start()
    {
        HeroEntity = Master.AddChild<CombatEntity>();
        HeroEntity.target = hero;
        
        MonsterEntity = Master.AddChild<CombatEntity>();
        MonsterEntity.target = monster;
    }

    private void Update()
    {
        Master.Update();
    }

    public void Attack()
    {
        if (HeroEntity.AttackAction.TryMakeAction(out var actionExecution))
        {
            actionExecution.Target = MonsterEntity;
            actionExecution.ApplyAttack();
        }
    }
}