using System.Linq;
using Attributes;
using BattleSystem;
using Commons.Copy;
using Commons.Extensions;
using Data;
using DG.Tweening;
using GameplayElements.Characters;
using InGameIndicators.Texts;
using ProgressSystem.Data;

namespace ProgressSystem
{
    public class ProgressManager : IProgressionProvider
    {
        private readonly HeroAttributes[] initialHeroAttributes;
        private readonly ProgressIOHelper progressIOHelper;
        
        public ProgressData CurrentProgress => progressIOHelper.ReadProgress();
        
        public ProgressManager()
        {
            initialHeroAttributes = DataContainers.InitialHeroAttributes.attributes;
            
            progressIOHelper = new ProgressIOHelper(initialHeroAttributes);
            
            UnlockInitialHeroesIfAllHeroesAreLocked();

            BattleManager.OnBattleResulted += HandleBattleResulted;
        }

        public HeroAttributes[] GetUpToDateHeroAttributes()
        {
            var heroAttributes = initialHeroAttributes.GetDeepCopy();

            for (int i = 0; i < heroAttributes.Length; i++)
            {
                var heroId = heroAttributes[i].id;
                var heroProgression = CurrentProgress.heroProgressions.First(h => h.id == heroId);
                heroAttributes[i].Apply(heroProgression, DataContainers.GameDesignValues);
            }

            return heroAttributes;
        }

        private void HandleBattleResulted(BattleCharacter[] winners)
        {
            IncreaseBattleCount(CurrentProgress);

            var didHeroesWin = winners[0] is Hero;
            if (!didHeroesWin)
            {
                progressIOHelper.SaveProgress(CurrentProgress);
                return;
            }

            for (int i = 0; i < winners.Length; i++)
            {
                if (winners[i].Health.IsDead)
                {
                    continue;
                }

                var heroView = (Hero) winners[i];
                IncreaseHeroExperience(heroView, CurrentProgress);
            }

            progressIOHelper.SaveProgress(CurrentProgress);
        }

        private void IncreaseHeroExperience(Hero hero, ProgressData currentProgress)
        {
            var heroId = hero.HeroAttributes.id;
            var heroProgression = currentProgress.heroProgressions.First(h => h.id == heroId);
            var currentExperience = ++heroProgression.experience;

            var messagePosition = hero.ThisTransform.position + DataContainers.UxDesignValues.experienceTextStartOffset;
            InGameTextDirector.DisplayMessage("+1 Experience!", messagePosition);
            
            if (currentExperience == DataContainers.GameDesignValues.experienceNeededToLevelUp)
            {
                heroProgression.level++;
                heroProgression.experience = 0;
                DOVirtual.DelayedCall(DataContainers.UxDesignValues.levelUpTextDelay, () =>
                {
                    InGameTextDirector.DisplayMessage("Level up!", hero.ThisTransform.position);
                });
            }
        }

        private void IncreaseBattleCount(ProgressData currentProgress)
        {
            currentProgress.battleCount++;
            if (currentProgress.battleCount % 5 == 0)
            {
                UnlockRandomHero();
            }
        }
        
        private void UnlockInitialHeroesIfAllHeroesAreLocked()
        {
            var isAllHeroesLocked = CurrentProgress.heroProgressions.All(hp => !hp.isUnlocked);
            if (isAllHeroesLocked)
            {
                for (int i = 0; i < DataContainers.GameDesignValues.initialHeroCount; i++)
                {
                    UnlockHero(CurrentProgress.heroProgressions[i]);
                }
            }
        }
        
        private void UnlockRandomHero()
        {
            var lockedHeroes = CurrentProgress.heroProgressions.Where(hp => !hp.isUnlocked);
            if (!lockedHeroes.Any())
            {
                return;
            }
            
            var randomLockedHero = lockedHeroes.GetRandomElement();
            UnlockHero(randomLockedHero);
        }
        
        private void UnlockHero(HeroProgression hero)
        {
            hero.isUnlocked = true;
            progressIOHelper.SaveProgress(CurrentProgress);
        }
    }
}