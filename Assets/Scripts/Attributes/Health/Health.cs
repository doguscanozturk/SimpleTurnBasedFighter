using System;

namespace Attributes.Health
{
    public class Health
    {
        public int MaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public bool IsDead { get; private set; }

        private readonly IHealthOwner healthOwner;
        
        private Settings settings;

        public Health(IHealthOwner healthOwner, Settings settings)
        {
            this.healthOwner = healthOwner;
            this.settings = settings;
            MaxHealth = CurrentHealth = settings.maxHealth;
        }

        public void TakeDamage(int amount)
        {
            if (IsDead) return;

            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
            {
                IsDead = true;
                CurrentHealth = 0;
                healthOwner.OnDied();
            }

            healthOwner.OnDamageTaken();
        }

        [Serializable]
        public struct Settings
        {
            public int maxHealth;
        }
    }
}