using System;

namespace ProgressSystem.Data
{
    [Serializable]
    public class HeroProgression
    {
        public int id;
        public bool isUnlocked;
        public int experience;
        public int level;
    }
}