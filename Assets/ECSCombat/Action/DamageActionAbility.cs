public class DamageActionAbility: BaseActionAbility
{
    public override void OnInit()
    {
        
    }

    public override bool TryMakeAction(GameEntity creator, GameEntity target, out BaseActionExecution baseActionExecution)
    {
        baseActionExecution = new DamageAction(creator, target);
        return true;
    }
}


public class DamageAction: BaseActionExecution
{
    public DamageAction(GameEntity creator, GameEntity target)
    {
        this.creator = creator;
        this.target = target;
    }
}