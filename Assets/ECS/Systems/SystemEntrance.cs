using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class SystemEntrance : Feature
{
    public SystemEntrance():base("SystemEntrance")
    {
        var gameContext = Contexts.sharedInstance.game;
        
        Add(new ViewSystem(gameContext));
        Add(new PositionSystem(gameContext));
        
        Add(new PropertySystem(gameContext));

        Add(new AbilityInitSystem(gameContext));
        Add(new AbilityExecuteSystem(gameContext));
    }

    public sealed override Systems Add(ISystem system)
    {
        return base.Add(system);
    }
}
