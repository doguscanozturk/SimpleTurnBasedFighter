using System;
using Commons.Copy;
using Data.Containers;
using ProgressSystem.Data;
using UnityEngine;

namespace Attributes
{
    [Serializable]
    public class HeroAttributes : BasicAttributes, IDeepCopyable<HeroAttributes>
    {
        [Header("Hero Attributes")] 
        public int experience;
        public int level;

        public void Apply(HeroProgression heroProgression, GameDesignValues gameDesignValues)
        {
            level = heroProgression.level;
            experience = heroProgression.experience;
            health.maxHealth = CalculateAttribute(health.maxHealth, gameDesignValues.heroStatIncreasePerLevel, heroProgression.level);
            attackPower = CalculateAttribute(attackPower, gameDesignValues.heroStatIncreasePerLevel, heroProgression.level);
        }

        public HeroAttributes DeepCopy()
        {
            return new HeroAttributes()
            {
                id = id,
                name = name,
                health = health,
                attackPower = attackPower,
                experience = experience,
                level = level
            };
        }

        private int CalculateAttribute(int initialValue, float increasePerLevel, int level)
        {
            var attributeValue = initialValue;
            for (int i = 1; i < level; i++)
            {
                attributeValue = (int) Math.Floor(attributeValue * increasePerLevel);
            }

            return attributeValue;
        }
    }
}