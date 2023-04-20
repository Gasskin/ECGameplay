using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

namespace ECGameplay
{
    public class DrawPropertyAttribute : Attribute
    {
        public DrawPropertyAttribute()
        {
        }
    }
    
    public class ComponentView : MonoBehaviour
    {
        public List<Component> componts = new List<Component>();

        private Random random = new Random();
        private List<Color> colors = new List<Color>();

        [OnInspectorGUI]
        private void OnInspectGUI()
        {
            PrepareColor();
            GUI.enabled = false;
            for (int i = 0; i < componts.Count; i++)
            {
                GUI.color = colors[i];
                var component = componts[i];
                var type = component.GetType();
                EditorGUILayout.TextField(type.Name);
                GUI.color = Color.white;

                if (type.GetCustomAttribute<DrawPropertyAttribute>() != null)
                {
                    EditorGUILayout.TextArea(component.ToString());
                }
            }

            GUI.enabled = true;
        }

        private void PrepareColor()
        {
            if (colors.Count < componts.Count)
            {
                for (int i = colors.Count; i < componts.Count; i++)
                {
                    colors.Add(GetRandomColor());
                }
            }
        }

        private Color GetRandomColor()
        {
            var r = (float)random.NextDouble();
            var g = (float)random.NextDouble();
            var b = (float)random.NextDouble();
            return new Color(r, g, b, 1);
        }
    }
}