using UnityEngine;

namespace Attributes.Health
{
    public interface IAttackable
    {
        Health Health { get; }
        
        Transform ThisTransform { get; }
    }
}