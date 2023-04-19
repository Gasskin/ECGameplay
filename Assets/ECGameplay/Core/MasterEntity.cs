﻿using System;
using System.Collections.Generic;
using ECGameplay.BasicComponent;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ECGameplay
{
    public class MasterEntity : Entity
    {
        // 记录所有的实体，和所有的组件
        public Dictionary<Type, List<Entity>> Entities { get; private set; } = new Dictionary<Type, List<Entity>>();
        public List<Component> AllComponents { get; private set; } = new List<Component>();

        // 单例
        private static MasterEntity instance;

        public static MasterEntity Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new MasterEntity();
                    var go = instance.AddComponent<GameObjectComponent>().GameObject;
                    Object.DontDestroyOnLoad(go);
                }

                return instance;
            }
            private set => instance = value;
        }

        // 禁止实例化
        private MasterEntity()
        {
            
        }
        
        public static void Destroy()
        {
            Destroy(Instance);
            Instance = null;
        }

        public override void Update()
        {
            if (AllComponents.Count == 0)
            {
                return;
            }
            for (int i = AllComponents.Count - 1; i >= 0; i--)
            {
                var item = AllComponents[i];
                if (item.IsDisposed)
                {
                    AllComponents.RemoveAt(i);
                    continue;
                }
                if (item.Disable)
                {
                    continue;
                }
                item.Update();
            }
        }
    }
}