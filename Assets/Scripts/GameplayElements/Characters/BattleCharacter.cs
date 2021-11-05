using System;
using Attributes.Health;
using Commons;
using Data;
using GameplayElements.Animation;
using InGameIndicators.Bars;
using UnityEngine;

namespace GameplayElements.Characters
{
    public abstract class BattleCharacter : BasicMono, IHealthOwner, IAttackable, IAnimatableCharacter
    {
        public static event Action<BattleCharacter> OnBattleCharacterAttacked;
        public static event Action<BattleCharacter> OnBattleCharacterDied;
        
        [SerializeField] protected Transform visuals;
        [SerializeField] protected new TextMesh name;
        [SerializeField] protected HealthBar healthBar;

        protected CharacterAnimationHandler characterAnimationHandler;
        protected IAttackable latestTarget;
        
        public abstract void Attack(IAttackable target);

        protected abstract void OnDamageDoneToTarget();
        
        protected void UpdateHealthBar()
        {
            var percentage = (float) Health.CurrentHealth / Health.MaxHealth;
            healthBar.UpdateFillAmount(percentage);
        }

        protected void TriggerOnAttacked(BattleCharacter battleCharacter)
        {
            OnBattleCharacterAttacked?.Invoke(battleCharacter);
        }

        #region IHealthOwner Implementation

        void IHealthOwner.OnDied()
        {
            characterAnimationHandler.TriggerDeathAnimation(DataContainers.CharacterAnimationSettings.death);
            OnBattleCharacterDied?.Invoke(this);
        }

        void IHealthOwner.OnDamageTaken()
        {
            UpdateHealthBar();
        }

        #endregion

        #region IAttackTarget Implementation

        public Health Health { get; protected set; }

        #endregion

        #region ICharacterAnimationUser Implementation

        Transform IAnimatableCharacter.AnimationTransform => visuals;

        public abstract AnimationSettings.Attack AnimationSettings { get; protected set; }

        #endregion
    }
}