using System.Collections.Generic;

namespace ECGameplay
{
    public class FloatModifier
    {
        // Id，用于区分不同的修饰器
        public int Id;
        // 修饰值
        public float Value;
    }
    
    public class FloatModifierCollection
    {
        public float TotalValue { get; private set; }
        private List<FloatModifier> Modifiers { get; } = new List<FloatModifier>();

        public void AddModifier(FloatModifier modifier)
        {
            Modifiers.Add(modifier);
            Refresh();
        }

        public void RemoveModifier(FloatModifier modifier)
        {
            Modifiers.Remove(modifier);
            Refresh();
        }

        private void Refresh()
        {
            TotalValue = 0;
            foreach (var item in Modifiers)
            {
                TotalValue += item.Value;
            }
        }
    }
    
    public class FloatNumeric
    {
        public float Value { get; private set; }
        public float baseValue { get; private set; }

        private FloatModifierCollection AddCollection { get; } = new FloatModifierCollection();
        private FloatModifierCollection PctAddCollection { get; } = new FloatModifierCollection();
        private FloatModifierCollection FinalAddCollection { get; } = new FloatModifierCollection();
        private FloatModifierCollection FinalPctAddCollection { get; } = new FloatModifierCollection();

        public void SetBaseValue(float baseValue)
        {
            this.baseValue = baseValue;
            Refresh();
        }
        
        public void AddAddModifier(FloatModifier modifier)
        {
            AddCollection.AddModifier(modifier);
            Refresh();
        }

        public void AddPctAddModifier(FloatModifier modifier)
        {
            PctAddCollection.AddModifier(modifier);
            Refresh();
        }

        public void AddFinalAddModifier(FloatModifier modifier)
        {
            FinalAddCollection.AddModifier(modifier);
            Refresh();
        }

        public void AddFinalPctAddModifier(FloatModifier modifier)
        {
            FinalPctAddCollection.AddModifier(modifier);
            Refresh();
        }

        public void RemoveAddModifier(FloatModifier modifier)
        {
            AddCollection.RemoveModifier(modifier);
            Refresh();
        }

        public void RemovePctAddModifier(FloatModifier modifier)
        {
            PctAddCollection.RemoveModifier(modifier);
            Refresh();
        }

        public void RemoveFinalAddModifier(FloatModifier modifier)
        {
            FinalAddCollection.RemoveModifier(modifier);
            Refresh();
        }

        public void RemoveFinalPctAddModifier(FloatModifier modifier)
        {
            FinalPctAddCollection.RemoveModifier(modifier);
            Refresh();
        }

        private void Refresh()
        {
            var value1 = baseValue;
            var value2 = (value1 + AddCollection.TotalValue) * (100 + PctAddCollection.TotalValue) / 100f;
            var value3 = (value2 + FinalAddCollection.TotalValue) * (100 + FinalPctAddCollection.TotalValue) / 100f;
            Value = value3;
        }
    }
}