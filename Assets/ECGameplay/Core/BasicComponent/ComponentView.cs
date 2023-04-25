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
        private GUIStyle textField;
        
        [OnInspectorGUI]
        private void OnInspectGUI()
        {
            PrepareColor();
            
            textField = new GUIStyle("TextField");
            textField.fontStyle = FontStyle.Bold;
            
            GUI.enabled = false;
            for (int i = 0; i < componts.Count; i++)
            {
                var component = componts[i];
                var type = component.GetType();
                textField.normal.textColor = colors[i];
                EditorGUILayout.TextField(type.Name, textField);

                // Go组件展示其Entity的属性，其他组件展示自身的属性
                if (type == typeof(GameObjectComponent))
                {
                    var entityType = component.Entity.GetType();
                    if (entityType.GetCustomAttribute<DrawPropertyAttribute>() != null) 
                    {
                        EditorGUILayout.TextArea(component.Entity.ToString());
                    }
                }
                else
                {
                    if (type.GetCustomAttribute<DrawPropertyAttribute>() != null)
                    {
                        EditorGUILayout.TextArea(component.ToString());
                    }
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