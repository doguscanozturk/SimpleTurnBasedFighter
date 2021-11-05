using System;
using System.IO;
using Attributes;
using ProgressSystem.Data;
using UnityEngine;

namespace ProgressSystem
{
    public class ProgressIOHelper
    {
        private const string ProgressJsonName = "Progress";

        private readonly HeroAttributes[] initialHeroAttributes;

        private ProgressData progressDataCache;

        public ProgressIOHelper(HeroAttributes[] initialHeroAttributes)
        {
            this.initialHeroAttributes = initialHeroAttributes;
        }
        
        public ProgressData ReadProgress()
        {
            if (progressDataCache != null)
            {
                return progressDataCache;
            }
            
            string progressJson;
            try
            {
                progressJson = File.ReadAllText(GetProgressDataPath());
            }
            catch (Exception e)
            {
                if (e is NullReferenceException || e is FileNotFoundException)
                {
                    progressJson = JsonUtility.ToJson(GenerateFreshProgressData(), true);
                    SaveProgress(progressJson);
                }
                else
                {
                    throw;
                }
            }

            progressDataCache = JsonUtility.FromJson<ProgressData>(progressJson);
            return progressDataCache;
        }

        private ProgressData GenerateFreshProgressData()
        {
            var result = new ProgressData();
            result.heroProgressions = new HeroProgression[initialHeroAttributes.Length];

            for (int i = 0; i < initialHeroAttributes.Length; i++)
            {
                var newHeroProgression = new HeroProgression();
                newHeroProgression.id = initialHeroAttributes[i].id;
                newHeroProgression.experience = initialHeroAttributes[i].experience;
                newHeroProgression.level = initialHeroAttributes[i].level;
                result.heroProgressions[i] = newHeroProgression;
            }

            return result;
        }

        public void SaveProgress(ProgressData progressData)
        {
            var json = JsonUtility.ToJson(progressData, true);
            SaveProgress(json);
        }

        private void SaveProgress(string jsonData)
        {
            if (string.IsNullOrEmpty(jsonData))
            {
                throw new Exception("Json data you want to save is null or empty!");
            }

            File.WriteAllText(GetProgressDataPath(), jsonData);
            progressDataCache = null;
        }

        private string GetProgressDataPath()
        {
            return $"{GetSaveDirectory()}{ProgressJsonName}.json";
        }
        
        private string GetSaveDirectory()
        {
            var directory = $"{Application.persistentDataPath}/";

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return directory;
        }
    }
}