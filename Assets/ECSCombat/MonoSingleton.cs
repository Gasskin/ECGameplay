using Entitas;
using UnityEditor;
using UnityEngine;

public class MonoSingleton : Singleton<MonoSingleton>
{
    private Systems systems;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Init()
    {
        var temp = Instance;

        Instance.systems = new Feature("System");
        Instance.systems.Add(new SystemEntrance());
        
        Instance.systems.Initialize();
    }

    void Update()
    {
        systems.Execute();
        systems.Cleanup();
    }
}
