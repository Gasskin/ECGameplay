//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public BlockActionAbilityComponent blockActionAbility { get { return (BlockActionAbilityComponent)GetComponent(GameComponentsLookup.BlockActionAbility); } }
    public bool hasBlockActionAbility { get { return HasComponent(GameComponentsLookup.BlockActionAbility); } }

    public void AddBlockActionAbility(BlockActionAbility newLogic) {
        var index = GameComponentsLookup.BlockActionAbility;
        var component = (BlockActionAbilityComponent)CreateComponent(index, typeof(BlockActionAbilityComponent));
        component.logic = newLogic;
        AddComponent(index, component);
    }

    public void ReplaceBlockActionAbility(BlockActionAbility newLogic) {
        var index = GameComponentsLookup.BlockActionAbility;
        var component = (BlockActionAbilityComponent)CreateComponent(index, typeof(BlockActionAbilityComponent));
        component.logic = newLogic;
        ReplaceComponent(index, component);
    }

    public void RemoveBlockActionAbility() {
        RemoveComponent(GameComponentsLookup.BlockActionAbility);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherBlockActionAbility;

    public static Entitas.IMatcher<GameEntity> BlockActionAbility {
        get {
            if (_matcherBlockActionAbility == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BlockActionAbility);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBlockActionAbility = matcher;
            }

            return _matcherBlockActionAbility;
        }
    }
}