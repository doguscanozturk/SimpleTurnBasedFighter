using Attributes;
using UnityEngine;

namespace Data.Containers
{
    [CreateAssetMenu(fileName = "InitialEnemyAttributes", menuName = "DataContainer/InitialEnemyAttributes")]
    public class InitialEnemyAttributes : ScriptableObject
    {
        public BasicAttributes[] attributes;
    }
}