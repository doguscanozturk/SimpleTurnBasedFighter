using Attributes;
using UnityEngine;

namespace Data.Containers
{
    [CreateAssetMenu(fileName = "InitialHeroAttributes", menuName = "DataContainer/InitialHeroAttributes")]
    public class InitialHeroAttributes : ScriptableObject
    {
        public HeroAttributes[] attributes;

        [ContextMenu("SetPreValues")]
        public void SetPreValues()
        {
            for (var i = 0; i < attributes.Length; i++)
            {
                attributes[i].id = i;
                attributes[i].health.maxHealth = (i + 1) * 10;
                attributes[i].attackPower = (i + 1) * 10;
                attributes[i].level = 1;
                attributes[i].experience = 0;
            }
        }
    }
}