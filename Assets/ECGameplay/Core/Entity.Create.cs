using System;
using System.Collections.Generic;

namespace ECGameplay
{
    public abstract partial class Entity
    {
        public static MasterEntity Master => MasterEntity.Instance;

        public static Entity NewEntity(Type entityType, long id = 0)
        {
            var entity = Activator.CreateInstance(entityType) as Entity;
            entity.InstanceId = IdFactory.NewInstanceId();
            if (id == 0) entity.Id = entity.InstanceId;
            else entity.Id = id;
            if (!Master.Entities.ContainsKey(entityType))
            {
                Master.Entities.Add(entityType, new List<Entity>());
            }
            Master.Entities[entityType].Add(entity);
            return entity;
        }

        public static T Create<T>() where T : Entity
        {
            return Create(typeof(T)) as T;
        }

        public static T Create<T>(object initData) where T : Entity
        {
            return Create(typeof(T), initData) as T;
        }

        private static void SetupEntity(Entity entity, Entity parent)
        {
            parent.SetChild(entity);
            {
                entity.Awake();
            }
            entity.Start();
        }

        private static void SetupEntity(Entity entity, Entity parent, object initData)
        {
            parent.SetChild(entity);
            {
                entity.Awake(initData);
            }
            entity.Start(initData);
        }

        public static Entity Create(Type entityType)
        {
            var entity = NewEntity(entityType);
            SetupEntity(entity, Master);
            return entity;
        }

        public static Entity Create(Type entityType, object initData)
        {
            var entity = NewEntity(entityType);
            SetupEntity(entity, Master, initData);
            return entity;
        }

        public static void Destroy(Entity entity)
        {
            entity.OnDestroy();
            entity.Dispose();
        }
    }
}