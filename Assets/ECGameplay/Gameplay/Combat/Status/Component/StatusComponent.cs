using System.Collections.Generic;

namespace ECGameplay
{
    public class StatusComponent : Component
    {
        public CombatEntity OwnerEntity => Entity.As<CombatEntity>();

        public List<StatusAbility> Statuses { get; set; } = new List<StatusAbility>();

        public Dictionary<int, List<StatusAbility>> TypeIdStatuses { get; set; } =
            new Dictionary<int, List<StatusAbility>>();

        public StatusAbility AttachStatus(object statusConfig)
        {
            var status = OwnerEntity.AttachAbility<StatusAbility>(statusConfig);
            if (!TypeIdStatuses.ContainsKey(status.StatusConfig.Id))
            {
                TypeIdStatuses.Add(status.StatusConfig.Id, new List<StatusAbility>());
            }
            TypeIdStatuses[status.StatusConfig.Id].Add(status);
            Statuses.Add(status);
            return status;
        }
    }
}