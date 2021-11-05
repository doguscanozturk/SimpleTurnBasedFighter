using System;

namespace ProgressSystem.Data
{
    [Serializable]
    public class ProgressData
    {
        public uint battleCount;
        public HeroProgression[] heroProgressions;
    }
}