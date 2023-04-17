//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TransformComponent transform { get { return (TransformComponent)GetComponent(GameComponentsLookup.Transform); } }
    public bool hasTransform { get { return HasComponent(GameComponentsLookup.Transform); } }

    public void AddTransform(UnityEngine.Vector3 newPosition, UnityEngine.Vector3 newRotation, UnityEngine.Vector3 newScale) {
        var index = GameComponentsLookup.Transform;
        var component = (TransformComponent)CreateComponent(index, typeof(TransformComponent));
        component.position = newPosition;
        component.rotation = newRotation;
        component.scale = newScale;
        AddComponent(index, component);
    }

    public void ReplaceTransform(UnityEngine.Vector3 newPosition, UnityEngine.Vector3 newRotation, UnityEngine.Vector3 newScale) {
        var index = GameComponentsLookup.Transform;
        var component = (TransformComponent)CreateComponent(index, typeof(TransformComponent));
        component.position = newPosition;
        component.rotation = newRotation;
        component.scale = newScale;
        ReplaceComponent(index, component);
    }

    public void RemoveTransform() {
        RemoveComponent(GameComponentsLookup.Transform);
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

    static Entitas.IMatcher<GameEntity> _matcherTransform;

    public static Entitas.IMatcher<GameEntity> Transform {
        get {
            if (_matcherTransform == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Transform);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTransform = matcher;
            }

            return _matcherTransform;
        }
    }
}
