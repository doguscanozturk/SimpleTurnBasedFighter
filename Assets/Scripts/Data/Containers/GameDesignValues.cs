using UnityEngine;

namespace Data.Containers
{
    [CreateAssetMenu(fileName = "GameDesignValues", menuName = "DataContainer/GameDesignValues")]
    public class GameDesignValues : ScriptableObject
    {
        public int initialHeroCount;
        public int maxSelectableHeroAmount;
        public int experienceNeededToLevelUp;
        public float heroStatIncreasePerLevel;
    }
}