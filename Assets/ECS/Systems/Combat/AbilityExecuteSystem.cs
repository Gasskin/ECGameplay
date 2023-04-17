using Entitas;

public class AbilityExecuteSystem : IExecuteSystem,ICleanupSystem
{
    private IGroup<GameEntity> abilities;

    public AbilityExecuteSystem(GameContext context)
    {
        abilities = context.GetGroup(GameMatcher.AbilityExecution);
    }
    
    public void Execute()
    {
        foreach (var ability in abilities)
        {
            ability.abilityExecution.logic.Update();
        }
    }

    public void Cleanup()
    {
        var entities = abilities.GetEntities();
        for (int i = entities.Length - 1; i >= 0; i--)
        {
            var entity = entities[i];
            if (entity.abilityExecution.logic.IsEnd) 
            {
                entity.Destroy();
            }
        }
    }
}
