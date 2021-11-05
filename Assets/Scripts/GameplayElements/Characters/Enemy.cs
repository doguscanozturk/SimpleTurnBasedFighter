using Attributes;
using Attributes.Health;
using Data;
using GameplayElements.Animation;
using InGameIndicators.Texts;

namespace GameplayElements.Characters
{
    public class Enemy : BattleCharacter
    {
        public override AnimationSettings.Attack AnimationSettings { get; protected set; }
        
        private BasicAttributes attributes;

        public void Initialize(BasicAttributes attributes)
        {
            this.attributes = attributes;
            Health = new Health(this, attributes.health);
            name.text = this.attributes.name;
            AnimationSettings = DataContainers.CharacterAnimationSettings.enemyAttack;
            characterAnimationHandler = new CharacterAnimationHandler(this, OnDamageDoneToTarget);

            UpdateHealthBar();
        }
        
        public void Uninitialize()
        {
            Health = null;
            
            characterAnimationHandler.Clear();
            characterAnimationHandler = null;
        }

        public override void Attack(IAttackable target)
        {
            latestTarget = target;
            characterAnimationHandler.TriggerAttackAnimation();
        }
        
        protected override void OnDamageDoneToTarget()
        {
            InGameTextDirector.DisplayDamage(attributes.attackPower, latestTarget.ThisTransform.position);
            latestTarget.Health.TakeDamage(attributes.attackPower);
            TriggerOnAttacked(this);
        }
    }
}