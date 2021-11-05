using Attributes;
using Controllers;
using Data.Containers;
using EventMediators;
using GameplayElements.Characters;
using ProgressSystem;
using ProgressSystem.Data;
using UI.Elements;
using UI.Handlers;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI.Panels
{
    public class CharacterSelectionPanel : Panel
    {
        [SerializeField] private RectTransform charactersParent;
        [SerializeField] private Button battleButton;
        [SerializeField] private Text battleCount;
        
        private HeroUIElement[] heroUIElements;
        private CharacterSelectionHandler characterSelectionHandler;
        private HeroInfoPopUpHandler heroInfoPopUpHandler;
        private IProgressionProvider progressionProvider;
        private GameDesignValues gameDesignValues;
        private HeroAttributes[] upToDateHeroAttributes;
        private ProgressData upToDateProgressData;

        public void Initialize(HeroInfoPopUpHandler heroInfoPopUpHandler, IProgressionProvider progressionProvider, GameDesignValues gameDesignValues)
        {
            this.heroInfoPopUpHandler = heroInfoPopUpHandler;
            this.progressionProvider = progressionProvider;
            this.gameDesignValues = gameDesignValues;
            characterSelectionHandler = new CharacterSelectionHandler(gameDesignValues);

            GenerateHeroUIElements();
        }

        public void OnBattleClicked()
        {
            UIEvents.TriggerBattleClicked(characterSelectionHandler.GetSelectedHeroes());
            characterSelectionHandler.Clear();
            UpdateBattleButtonInteractableState();
        }

        private void OnEnable()
        {
            HeroUIElement.OnHeroUIElementPointerUp += HandleHeroUIElementPointerUp;
            HeroUIElement.OnHeroUIElementPointerDown += HandleHeroUIElementPointerDown;
            BattleCharacterController.OnButtonDownOnBattleCharacter -= HandleButtonDownOnBattleCharacter;
            BattleCharacterController.OnButtonUpOnBattleCharacter -= HandleButtonUpOnBattleCharacter;
            FillUpToDateProgressData();
            UpdateHeroUIElements();
            UpdateBattleCount();
        }

        private void OnDisable()
        {
            HeroUIElement.OnHeroUIElementPointerUp -= HandleHeroUIElementPointerUp;
            HeroUIElement.OnHeroUIElementPointerDown -= HandleHeroUIElementPointerDown;
            BattleCharacterController.OnButtonDownOnBattleCharacter += HandleButtonDownOnBattleCharacter;
            BattleCharacterController.OnButtonUpOnBattleCharacter += HandleButtonUpOnBattleCharacter;
            heroInfoPopUpHandler?.Cancel();
            upToDateHeroAttributes = null;
        }

        private void FillUpToDateProgressData()
        {
            upToDateHeroAttributes = progressionProvider.GetUpToDateHeroAttributes();
            upToDateProgressData = progressionProvider.CurrentProgress;
        }
        
        private void GenerateHeroUIElements()
        {
            FillUpToDateProgressData();
            
            heroUIElements = new HeroUIElement[upToDateHeroAttributes.Length];
            
            for (int i = 0; i < upToDateHeroAttributes.Length; i++)
            {
                var newElement = PrefabInstantiator.Instantiate<HeroUIElement>(charactersParent);
                heroUIElements[i] = newElement;
            }

            UpdateHeroUIElements();
        }

        private void UpdateHeroUIElements()
        {
            for (int i = 0; i < heroUIElements.Length; i++)
            {
                heroUIElements[i].UpdateSettings(upToDateHeroAttributes[i], upToDateProgressData.heroProgressions[i].isUnlocked);
            }
        }

        private void UpdateBattleCount()
        {
            battleCount.text = upToDateProgressData.battleCount.ToString();
        }
        
        private void HandleHeroUIElementPointerUp(HeroUIElement heroUIElement)
        {
            if (heroInfoPopUpHandler.IsShowingPopUp)
            {
                return;
            }
            
            heroInfoPopUpHandler.Cancel();
            characterSelectionHandler.HandleHeroUIElementPointerUp(heroUIElement);
            UpdateBattleButtonInteractableState();
        }

        private void HandleHeroUIElementPointerDown(HeroUIElement heroUIElement)
        {
            heroInfoPopUpHandler.StartTimer(heroUIElement.HeroAttributes, heroUIElement.ThisRectTransform.position);
        }

        private void HandleButtonUpOnBattleCharacter(BattleCharacter battleCharacter)
        {
            heroInfoPopUpHandler.Cancel();
        }

        private void HandleButtonDownOnBattleCharacter(BattleCharacter battleCharacter)
        {
            if (battleCharacter is Hero hero)
            {
                var screenPosition = CameraUtility.WorldToScreenPoint(hero.ThisTransform.position);
                heroInfoPopUpHandler.StartTimer(hero.HeroAttributes, screenPosition);
            }
        }

        private void UpdateBattleButtonInteractableState()
        {
            battleButton.interactable = characterSelectionHandler.SelectedHeroes.Count >= gameDesignValues.maxSelectableHeroAmount;
        }
    }
}