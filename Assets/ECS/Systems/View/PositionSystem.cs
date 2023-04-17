using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class PositionSystem : ReactiveSystem<GameEntity>
{
    public PositionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    public PositionSystem(ICollector<GameEntity> collector) : base(collector)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.View, GameMatcher.Transform));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var trans = entity.view.gameObject.transform;
            var transComp = entity.transform;
            trans.position = transComp.position;
            trans.rotation = Quaternion.Euler(transComp.rotation);
            trans.localScale = transComp.scale;
        }
    }
}
