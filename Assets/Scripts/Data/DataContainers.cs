using Data.Containers;
using UnityEngine;

namespace Data
{
    public static class DataContainers
    {       
        public static readonly AssetReferences AssetReferences;
        public static readonly InitialHeroAttributes InitialHeroAttributes;
        public static readonly InitialEnemyAttributes InitialEnemyAttributes;
        public static readonly CharacterAnimationSettings CharacterAnimationSettings;
        public static readonly GameDesignValues GameDesignValues;
        public static readonly UxDesignValues UxDesignValues;
        
        static DataContainers()
        {
            AssetReferences = (AssetReferences) Resources.Load("Data/AssetReferences", typeof(ScriptableObject));
            InitialEnemyAttributes = (InitialEnemyAttributes) Resources.Load("Data/InitialEnemyAttributes", typeof(ScriptableObject));
            InitialHeroAttributes = (InitialHeroAttributes) Resources.Load("Data/InitialHeroAttributes", typeof(ScriptableObject));
            CharacterAnimationSettings = (CharacterAnimationSettings) Resources.Load("Data/CharacterAnimationSettings", typeof(ScriptableObject));
            GameDesignValues = (GameDesignValues) Resources.Load("Data/GameDesignValues", typeof(ScriptableObject));
            UxDesignValues = (UxDesignValues) Resources.Load("Data/UxDesignValues", typeof(ScriptableObject));
        }
    }
}