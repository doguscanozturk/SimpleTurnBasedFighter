using System;
using System.Linq;
using Attributes;
using Controllers;
using Data;
using EventMediators;
using GameplayElements;
using GameplayElements.Characters;
using Utility;
using Object = UnityEngine.Object;

namespace BattleSystem
{
    public class BattleManager
    {
        public static event Action<BattleState> OnBattleStateUpdated;
        public static event Action<BattleCharacter[]> OnBattleResulted;

        private readonly Battleground battleground;
        private readonly BattleCharacterController battleCharacterController;
        
        private BattleParticipants battleParticipants;
        private BattleState currentBattleState;
        private bool isHeroAttackTriggeredOnce;

        public BattleManager()
        {
            battleCharacterController = new BattleCharacterController();

            battleground = PrefabInstantiator.Instantiate<Battleground>();

            BattleCharacterController.OnButtonUpOnBattleCharacter += HandleButtonUpOnBattleCharacter;
            BattleCharacter.OnBattleCharacterAttacked += HandleBattleCharacterAttacked;
            BattleCharacter.OnBattleCharacterDied += HandleBattleCharacterDied;
            UIEvents.OnBattleClicked += HandleBattleClicked;
            UIEvents.OnReturnCharacterSelectionClicked += HandleReturnCharacterSelectionClicked;
        }

        public void UpdateFrame(float deltaTime)
        {
            battleCharacterController.UpdateFrame(deltaTime);
        }

        private void InitializeParticipants(HeroAttributes[] heroSettings, BasicAttributes enemyAttributes)
        {
            battleParticipants = InstantiateBattleParticipants(heroSettings, enemyAttributes);
        }

        private void DestroyParticipants()
        {
            for (int i = battleParticipants.heroes.Length - 1; i >= 0; i--)
            {
                battleParticipants.heroes[i].Uninitialize();
                Object.Destroy(battleParticipants.heroes[i].ThisGameObject);
            }

            battleParticipants.enemy.Uninitialize();
            Object.Destroy(battleParticipants.enemy.ThisGameObject);
        }

        private void StartBattle()
        {
            UpdateState(BattleState.PlayerAttacking);
        }

        private void EndBattle(bool didHeroesWin)
        {
            UpdateState(BattleState.Ended);

            var winners = didHeroesWin ? battleParticipants.heroes : new BattleCharacter[] {battleParticipants.enemy};
            OnBattleResulted?.Invoke(winners);
        }

        private void HandleBattleClicked(HeroAttributes[] heroes)
        {
            var randomEnemy =
                DataContainers.InitialEnemyAttributes.attributes[UnityEngine.Random.Range(0, DataContainers.InitialEnemyAttributes.attributes.Length)];
            InitializeParticipants(heroes, randomEnemy);
            battleCharacterController.TriggerActivate(true);
            StartBattle();
        }

        private void HandleReturnCharacterSelectionClicked()
        {
            DestroyParticipants();
        }

        private void HandleButtonUpOnBattleCharacter(BattleCharacter battleCharacter)
        {
            switch (currentBattleState)
            {
                case BattleState.PlayerAttacking:
                    if (isHeroAttackTriggeredOnce) return;

                    if (battleCharacter is Hero hero)
                    {
                        if (hero.Health.IsDead)
                        {
                            return;
                        }

                        hero.Attack(battleParticipants.enemy);
                        isHeroAttackTriggeredOnce = true;
                    }

                    break;
                case BattleState.EnemyAttacking:

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void HandleBattleCharacterAttacked(BattleCharacter battleCharacter)
        {
            switch (battleCharacter)
            {
                case Hero hero:
                    UpdateState(BattleState.EnemyAttacking);
                    isHeroAttackTriggeredOnce = false;
                    break;
                case Enemy enemy:
                    UpdateState(BattleState.PlayerAttacking);
                    break;
            }
        }

        private void HandleBattleCharacterDied(BattleCharacter deadCharacter)
        {
            switch (deadCharacter)
            {
                case Hero hero:
                    var isAllHeroesDead = battleParticipants.heroes.All(h => h.Health.IsDead);
                    if (isAllHeroesDead)
                    {
                        EndBattle(false);
                    }
                    break;
                case Enemy enemy:
                    EndBattle(true);
                    break;
            }
        }

        private void UpdateState(BattleState newState)
        {
            currentBattleState = newState;
            OnBattleStateUpdated?.Invoke(currentBattleState);

            if (currentBattleState == BattleState.EnemyAttacking)
            {
                TriggerEnemyAttack();
            }
        }

        private void TriggerEnemyAttack()
        {
            if (battleParticipants.enemy.Health.IsDead) return;

            var aliveHeroes = battleParticipants.heroes.Where(h => !h.Health.IsDead).ToArray();
            var randomHero = aliveHeroes[UnityEngine.Random.Range(0, aliveHeroes.Length)];

            battleParticipants.enemy.Attack(randomHero);
        }

        private BattleParticipants InstantiateBattleParticipants(HeroAttributes[] heroSettings, BasicAttributes enemyAttributes)
        {
            var result = new BattleParticipants();

            var enemy = PrefabInstantiator.Instantiate<Enemy>();
            enemy.ThisTransform.position = battleground.enemySpot.position;
            enemy.ThisGameObject.name = $"Enemy {enemyAttributes.name}";
            enemy.Initialize(enemyAttributes);
            result.enemy = enemy;

            var heroes = new Hero[heroSettings.Length];
            for (int i = 0; i < heroes.Length; i++)
            {
                var heroView = PrefabInstantiator.Instantiate<Hero>();
                heroView.ThisTransform.position = battleground.heroSpots[i].position;
                heroView.ThisGameObject.name = $"Hero {heroSettings[i].name}";
                heroView.Initialize(heroSettings[i]);
                heroes[i] = heroView;
            }

            result.heroes = heroes;

            return result;
        }
    }
}