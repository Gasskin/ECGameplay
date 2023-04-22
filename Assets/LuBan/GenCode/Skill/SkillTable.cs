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



namespace cfg.Skill
{ 

public sealed partial class SkillTable
{
    private readonly Dictionary<int, Skill.SkillConfig> _dataMap;
    private readonly List<Skill.SkillConfig> _dataList;
    
    public SkillTable(JSONNode _json)
    {
        _dataMap = new Dictionary<int, Skill.SkillConfig>();
        _dataList = new List<Skill.SkillConfig>();
        
        foreach(JSONNode _row in _json.Children)
        {
            var _v = Skill.SkillConfig.DeserializeSkillConfig(_row);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, Skill.SkillConfig> DataMap => _dataMap;
    public List<Skill.SkillConfig> DataList => _dataList;

    public Skill.SkillConfig GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Skill.SkillConfig Get(int key) => _dataMap[key];
    public Skill.SkillConfig this[int key] => _dataMap[key];

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