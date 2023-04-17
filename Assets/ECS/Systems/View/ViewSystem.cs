using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class ViewSystem : ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> viewGroup;

    public ViewSystem(IContext<GameEntity> context) : base(context)
    {
        viewGroup = context.GetGroup(GameMatcher.View);
        viewGroup.OnEntityRemoved += OnEntityRemoved;
    }
    
    public ViewSystem(ICollector<GameEntity> collector) : base(collector)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.View);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.view.gameObject.Link(entity);
        }
    }
    
    private void OnEntityRemoved(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
    {
        if (component is ViewComponent viewComponent)
        {
            viewComponent.gameObject.Unlink();
            Object.DestroyImmediate(viewComponent.gameObject);
            entity.Destroy();
        }
    }
}
