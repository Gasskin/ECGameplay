using Entitas;

public class AbilityExecuteSystem : IExecuteSystem
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
}
