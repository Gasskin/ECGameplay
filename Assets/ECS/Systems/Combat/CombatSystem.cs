using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CombatSystem : IExecuteSystem
{
    private GameContext gameContext;
    
    public CombatSystem(Contexts context)
    {
        gameContext = context.game;
    }
    
    public void Execute()
    {
    }
}
