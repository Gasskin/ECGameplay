using System.Collections.Generic;
using cfg.Status;

namespace ECGameplay
{
    public class StatusComponent : Component
    {
        public CombatEntity OwnerEntity => Entity.As<CombatEntity>();

        public List<StatusAbility> Statuses { get; set; } = new List<StatusAbility>();
        
        public Dictionary<string,List<StatusAbility>> TypeIdStatuses { get; set; } = new Dictionary<string, List<StatusAbility>>();
        
        public StatusAbility AttachStatus(StatusConfig statusConfig)
        {
            var status = OwnerEntity.AttachStatus<StatusAbility>(statusConfig.Id);
            return null;
        }
    }
}