using System;
using ECGameplay;
using UnityEngine;

public class Test : MonoBehaviour
{
    private MasterEntity MasterEntity => MasterEntity.Instance;
    
    public class TestEntity : Entity
    {
        public override void Update()
        {
            Debug.Log(GetComponent<AttributeComponent>()?.HealthPoint.Value);
        }
    }
    
    private void Start()
    {
        var entity = MasterEntity.AddChild<TestEntity>();
        entity.AddComponent<UpdateComponent>();
        entity.AddComponent<AttributeComponent>();
        entity.AddChild<AttackAction>().AddChild<AttackAbility>();
    }

    private void Update()
    {
        MasterEntity.Update();
    }
}
