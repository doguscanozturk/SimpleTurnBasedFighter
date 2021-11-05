using System;
using UnityEngine;

namespace Attributes
{
    [Serializable]
    public class BasicAttributes
    {
        [Header("Basic Attributes")]
        public int id;
        public string name;
        public Health.Health.Settings health;
        public int attackPower;
    }
}