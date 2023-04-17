using System;
using System.Collections.Generic;
using Entitas;

public class PropertySystem : ReactiveSystem<GameEntity>
{
    private IGroup<GameEntity> propertyGroup;

    public PropertySystem(IContext<GameEntity> context) : base(context)
    {
        propertyGroup = context.GetGroup(GameMatcher.Property);
        propertyGroup.OnEntityRemoved += OnEntityRemoved;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Property);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var entity in entities)
        {
            var target = entity.property.target;
            if (target is { hasBaseProperty: true })
            {
                var property = entity.property;
                var baseProperty = target.baseProperty;

                var healthAdd = property.enable ? property.healthAdd : -property.healthAdd;
                var attackAdd = property.enable ? property.attackAdd : -property.attackAdd;
                var defenceAdd = property.enable ? property.defenceAdd : -property.defenceAdd;
                var healthPctAdd = property.enable ? property.healthPctAdd : -property.healthPctAdd;
                var attackPctAdd = property.enable ? property.attackPctAdd : -property.attackPctAdd;
                var defencePctAdd = property.enable ? property.defencePctAdd : -property.defencePctAdd;

                baseProperty.healthAdd += healthAdd;
                baseProperty.attackAdd += attackAdd;
                baseProperty.defenceAdd += defenceAdd;
                baseProperty.healthPctAdd += healthPctAdd;
                baseProperty.attackPctAdd += attackPctAdd;
                baseProperty.defencePctAdd += defencePctAdd;

                baseProperty.health =
                    (baseProperty.baseHealth + baseProperty.healthAdd) * (1 + baseProperty.healthPctAdd);
                baseProperty.attack =
                    (baseProperty.baseAttack + baseProperty.attackAdd) * (1 + baseProperty.attackPctAdd);
                baseProperty.defence =
                    (baseProperty.baseDefence + baseProperty.defenceAdd) * (1 + baseProperty.defencePctAdd);
            }
            else
            {
                entity.Destroy();
            }
        }
    }

    private void OnEntityRemoved(IGroup<GameEntity> group, GameEntity entity, int index, IComponent component)
    {
        if (component is PropertyComponent propertyComponent)
        {
            var target = propertyComponent.target;
            if (target is { hasBaseProperty: true })
            {
                var baseProperty = target.baseProperty;

                baseProperty.healthAdd -= propertyComponent.healthAdd;
                baseProperty.attackAdd -= propertyComponent.attackAdd;
                baseProperty.defenceAdd -= propertyComponent.defenceAdd;
                baseProperty.healthPctAdd -= propertyComponent.healthPctAdd;
                baseProperty.attackPctAdd -= propertyComponent.attackPctAdd;
                baseProperty.defencePctAdd -= propertyComponent.defencePctAdd;

                baseProperty.health =
                    (baseProperty.baseHealth + baseProperty.healthAdd) * (1 + baseProperty.healthPctAdd);
                baseProperty.attack =
                    (baseProperty.baseAttack + baseProperty.attackAdd) * (1 + baseProperty.attackPctAdd);
                baseProperty.defence =
                    (baseProperty.baseDefence + baseProperty.defenceAdd) * (1 + baseProperty.defencePctAdd);
            }
            entity.Destroy();
        }
    }
}