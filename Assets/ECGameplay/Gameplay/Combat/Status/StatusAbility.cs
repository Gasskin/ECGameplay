using cfg.Status;

namespace ECGameplay
{
    public class StatusAbility : Entity, IAbility
    {
        public CombatEntity OwnerEntity { get; set; }
        public bool Enable { get; set; }
        public StatusConfig StatusConfig { get; set; }


        public override void Awake(object initData)
        {
            StatusConfig = initData as StatusConfig;
        }

        public void TryActivateAbility()
        {
        }

        public void ActivateAbility()
        {
        }

        public void DeactivateAbility()
        {
        }

        public void EndAbility()
        {
        }

        public Entity CreateExecution()
        {
            return null;
        }
    }
}