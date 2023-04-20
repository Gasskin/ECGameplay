namespace ECGameplay
{
    [DrawProperty]
    public class AttributeComponent : Component
    {
        // 移动速度
        public FloatNumeric MoveSpeed { get; set; } = new (); 
        // 当前生命值
        public FloatNumeric HealthPoint  { get; set; } = new (); 
        // 生命值上限
        public FloatNumeric HealthPointMax { get; set; } = new ();
        // 攻击力
        public FloatNumeric Attack { get; set; } = new (); 
        // 防御力（护甲）
        public FloatNumeric Defense { get; set; } = new (); 
        // 法术强度
        public FloatNumeric AbilityPower { get; set; } = new (); 
        // 魔法抗性
        public FloatNumeric SpellResistance { get; set; } = new (); 
        // 暴击概率
        public FloatNumeric CriticalProbability { get; set; } = new (); 
        // 暴击伤害
        public FloatNumeric CauseDamage { get; set; } = new ();


        public override void Awake()
        {
            MoveSpeed.SetBaseValue(1);
            HealthPoint.SetBaseValue(99_999);
            HealthPointMax.SetBaseValue(99_999);
            Attack.SetBaseValue(1000);
            Defense.SetBaseValue(300);
            AbilityPower.SetBaseValue(0);
            SpellResistance.SetBaseValue(300);
            CriticalProbability.SetBaseValue(0.5f);
            CauseDamage.SetBaseValue(1.5f);
        }

#if UNITY_EDITOR
        public override string ToString()
        {
            var str = "";
            str += "MoveSpeed: " + MoveSpeed.Value + "\n";
            str += "HealthPoint: " + HealthPoint.Value + "\n";
            str += "HealthPointMax: " + HealthPointMax.Value + "\n";
            str += "Attack: " + Attack.Value + "\n";
            str += "Defense: " + Defense.Value + "\n";
            str += "AbilityPower: " + AbilityPower.Value + "\n";
            str += "SpellResistance: " + SpellResistance.Value + "\n";
            str += "CriticalProbability: " + CriticalProbability.Value + "\n";
            str += "CauseDamage: " + CauseDamage.Value;
            return str;
        }
#endif
    }
}