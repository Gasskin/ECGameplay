using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class AbilityInitSystem : ReactiveSystem<GameEntity>
{
    public AbilityInitSystem(IContext<GameEntity> context) : base(context)
    {
    }

    public AbilityInitSystem(ICollector<GameEntity> collector) : base(collector)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(Matcher<GameEntity>.AnyOf(
            GameMatcher.AttackActionAbility,
            GameMatcher.BlockActionAbility,
            GameMatcher.AttackAbility));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasView;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            if (entity.hasAttackActionAbility) 
            {
                entity.attackActionAbility.logic?.Init(entity);
            }

            if (entity.hasBlockActionAbility)
            {
                entity.blockActionAbility.logic?.Init(entity);
            }

            if (entity.hasAttackAbility)
            {
                entity.attackAbility.logic?.Init(entity);
            } 
        }
    }
}