using System;
using cfg.Skill;
using ECGameplay;
using ECGamePlay;
using UnityEngine;

public class Test : MonoBehaviour
{
    private MasterEntity Master => MasterEntity.Instance;


    private void Start()
    {
        
    }

    private void Update()
    {
        Master.Update();
    }
}