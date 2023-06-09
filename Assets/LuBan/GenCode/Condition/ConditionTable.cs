//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;
using SimpleJSON;



namespace cfg.Condition
{ 

public sealed partial class ConditionTable
{
    private readonly Dictionary<int, Condition.ConditionConfig> _dataMap;
    private readonly List<Condition.ConditionConfig> _dataList;
    
    public ConditionTable(JSONNode _json)
    {
        _dataMap = new Dictionary<int, Condition.ConditionConfig>();
        _dataList = new List<Condition.ConditionConfig>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = Condition.ConditionConfig.DeserializeConditionConfig(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, Condition.ConditionConfig> DataMap => _dataMap;
    public List<Condition.ConditionConfig> DataList => _dataList;

    public Condition.ConditionConfig GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Condition.ConditionConfig Get(int key) => _dataMap[key];
    public Condition.ConditionConfig this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    
    partial void PostInit();
    partial void PostResolve();
}

}