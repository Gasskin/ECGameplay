using System.Collections.Generic;
using Entitas;

public static class PropertyUtil
{
    public static void AddBaseProperty(GameEntity target, float health, float attack, float defence)
    {
        if (!target.hasBaseProperty)
        {
            target.AddBaseProperty(health, attack, defence, 0, 0, 0, 0, 0, 0, health, attack, defence);
        }
    }
    
    public static void AddProperty_Health(GameEntity target, int id, float healthAdd, float healthPctAdd)
    {
        AddProperty(target, id, healthAdd, 0, 0, healthPctAdd, 0, 0);
    }

    public static void AddProperty_Attack(GameEntity target, int id, float attackAdd, float attackPctAdd)
    {
        AddProperty(target, id, 0, attackAdd, 0, 0, attackPctAdd, 0);
    }

    public static void AddProperty_Defence(GameEntity target, int id, float defenceAdd, float defencePctAdd)
    {
        AddProperty(target, id, 0, 0, defenceAdd, 0, 0, defencePctAdd);
    }

    public static void AddProperty_Health_Attack(GameEntity target, int id, float healthAdd, float attackAdd,
        float healthPctAdd, float attackPctAdd)
    {
        AddProperty(target, id, healthAdd, attackAdd, 0, healthPctAdd, attackPctAdd, 0);
    }

    public static void AddProperty_Health_Defence(GameEntity target, int id, float healthAdd, float defenceAdd,
        float healthPctAdd, float defencePctAdd)
    {
        AddProperty(target, id, healthAdd, 0, defenceAdd, healthPctAdd, 0, defencePctAdd);
    }

    public static void AddProperty_Attack_Defence(GameEntity target, int id, float attackAdd, float defenceAdd,
        float attackPctAdd, float defencePctAdd)
    {
        AddProperty(target, id, 0, attackAdd, defenceAdd, 0, attackPctAdd, defencePctAdd);
    }

    public static void AddProperty(GameEntity target, int id, float healthAdd, float attackAdd, float defenceAdd,
        float healthPctAdd, float attackPctAdd, float defencePctAdd)
    {
        // 同一个ID的属性只能存在一个
        var group = Contexts.sharedInstance.game.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Property, GameMatcher.Identity));
        foreach (var entity in group)
        {
            if (entity.identity.id == id) 
                return;
        }
        
        if (target.hasBaseProperty)
        {
            var entity = Contexts.sharedInstance.game.CreateEntity();
            entity.AddProperty(target, true, healthAdd, healthPctAdd, attackAdd, attackPctAdd, defenceAdd, defencePctAdd);
            entity.AddIdentity(id);
        }
    }

    public static List<GameEntity> GetAllPropertyEntity(GameEntity target)
    {
        if (!target.hasBaseProperty)
        {
            return null;
        }
        var group = Contexts.sharedInstance.game.GetGroup(GameMatcher.Property);
        if (group.count > 0) ;
        {
            var list = new List<GameEntity>();
            foreach (var entity in group.GetEntities())
            {
                if (entity.property.target  == target)
                {
                    list.Add(entity);
                }
            }

            return list;
        }
    }
}