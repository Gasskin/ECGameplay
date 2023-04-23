namespace ECGameplay
{
    public class StatusAbility : Entity, IAbility
    {
        public CombatEntity OwnerEntity { get; set; }
        public bool Enable { get; set; }

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