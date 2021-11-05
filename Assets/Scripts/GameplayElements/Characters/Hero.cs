using Attributes;
using Attributes.Health;
using Data;
using GameplayElements.Animation;
using InGameIndicators.Texts;

namespace GameplayElements.Characters
{
    public class Hero : BattleCharacter
    {
        public HeroAttributes HeroAttributes { get; private set; }
        public override AnimationSettings.Attack AnimationSettings { get; protected set; }
        
        public void Initialize(HeroAttributes heroAttributes)
        {
            HeroAttributes = heroAttributes;
            Health = new Health(this, heroAttributes.health);
            name.text = HeroAttributes.name;
            AnimationSettings = DataContainers.CharacterAnimationSettings.heroAttack;
            characterAnimationHandler = new CharacterAnimationHandler(this, OnDamageDoneToTarget);

            UpdateHealthBar();
        }

        public void Uninitialize()
        {
            HeroAttributes = null;
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
            InGameTextDirector.DisplayDamage(HeroAttributes.attackPower, latestTarget.ThisTransform.position);
            latestTarget.Health.TakeDamage(HeroAttributes.attackPower);
            TriggerOnAttacked(this);
        }
    }
}