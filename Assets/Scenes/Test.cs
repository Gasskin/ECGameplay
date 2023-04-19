using System;
using ECGameplay;
using ECGameplay.BasicComponent;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    public class TestEntity : Entity
    {
        
    }
    
    private void Start()
    {
        var master = MasterEntity.Instance;
        var entity = master.AddChild<TestEntity>();
    }
}
