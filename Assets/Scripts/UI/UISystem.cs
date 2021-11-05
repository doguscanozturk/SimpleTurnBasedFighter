using Attributes;
using BattleSystem;
using Data;
using DG.Tweening;
using EventMediators;
using GameplayElements.Characters;
using ProgressSystem;
using UI.Handlers;
using UI.Panels;
using UnityEngine;
using Utility;

namespace UI
{
    public class UISystem
    {
        private readonly Canvas canvas;
        private readonly Transform canvasTransform;
        private readonly CharacterSelectionPanel characterSelectionPanel;
        private readonly BattlePanel battlePanel;
        private readonly GameOverPanel gameOverPanel;
        private readonly HeroInfoPopUpHandler heroInfoPopupHandler;
        
        public UISystem(IProgressionProvider progressionProvider)
        {
            canvas = PrefabInstantiator.Instantiate<Canvas>();
            canvasTransform = canvas.transform;

            heroInfoPopupHandler = new HeroInfoPopUpHandler((RectTransform) canvasTransform);

            characterSelectionPanel = PrefabInstantiator.Instantiate<CharacterSelectionPanel>(canvasTransform);
            characterSelectionPanel.Initialize(heroInfoPopupHandler, progressionProvider);
            characterSelectionPanel.Show();

            battlePanel = PrefabInstantiator.Instantiate<BattlePanel>(canvasTransform);

            gameOverPanel = PrefabInstantiator.Instantiate<GameOverPanel>(canvasTransform);

            heroInfoPopupHandler.SetAsLastSibling();

            UIEvents.OnBattleClicked += HandleBattleClicked;
            UIEvents.OnReturnCharacterSelectionClicked += HandleReturnCharacterSelectionClicked;
            BattleManager.OnBattleResulted += HandleBattleResulted;
        }

        public void UpdateFrame(float deltaTime)
        {
            heroInfoPopupHandler.UpdateFrame(deltaTime);
        }

        private void HandleBattleClicked(HeroAttributes[] chosenHeroAttributes)
        {
            characterSelectionPanel.Hide();
            battlePanel.Show();
        }

        private void HandleBattleResulted(BattleCharacter[] winners)
        {
            var didHeroesWin = winners[0] is Hero;
            battlePanel.Hide();
            gameOverPanel.PreparePanel(didHeroesWin);

            DOVirtual.DelayedCall(DataContainers.UxDesignValues.gameOverPanelOpeningDelay, () =>
            {
                gameOverPanel.Show();
            });
        }

        private void HandleReturnCharacterSelectionClicked()
        {
            gameOverPanel.Hide();
            characterSelectionPanel.Show();
        }
    }
}