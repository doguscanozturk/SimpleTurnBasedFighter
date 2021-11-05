using Attributes;
using UI.Elements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.PopUps
{
    public class HeroInfoPopup : BasicUIElement, IPointerExitHandler, IPointerEnterHandler
    {
        [SerializeField] private new Text name;
        [SerializeField] private Text level;
        [SerializeField] private Text attackPower;
        [SerializeField] private Text experience;
        private bool isPointerOnTopOfMe;

        public void Fill(HeroAttributes heroHeroAttributes)
        {
            name.text = $"Name: {heroHeroAttributes.name}";
            level.text = $"Level: {heroHeroAttributes.level}";
            attackPower.text = $"Attack Power: {heroHeroAttributes.attackPower}";
            experience.text = $"Experience: {heroHeroAttributes.experience}";
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            Hide();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !isPointerOnTopOfMe)
            {
                Hide();
            }
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            isPointerOnTopOfMe = false;
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            isPointerOnTopOfMe = true;
        }
    }
}