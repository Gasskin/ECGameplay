using System.IO;
using cfg;
using cfg.Skill;
using SimpleJSON;
using UnityEngine;

namespace ECGameplay
{
    public static class TableUtil
    {
        private static Tables _tables;

        public static Tables Tables
        {
            get
            {
                if (_tables == null)
                {
                    _tables = new Tables((file => JSON.Parse(File.ReadAllText($"{Application.dataPath}/LuBan/Json/{file}.json"))));
                }
                return _tables;
            }
        }

        public static bool TryGet(this SkillTable table,int id, out SkillConfig config)
        {
            return table.DataMap.TryGetValue(id, out config);
        }
    }
}