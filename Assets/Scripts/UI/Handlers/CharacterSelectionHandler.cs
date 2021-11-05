using System.Collections.Generic;
using System.Linq;
using Attributes;
using Data;
using UI.Elements;

namespace UI.Handlers
{
    public class CharacterSelectionHandler
    {
        public List<HeroUIElement> SelectedHeroes { get; private set; }
        
        public CharacterSelectionHandler()
        {
            SelectedHeroes = new List<HeroUIElement>();
        }

        public HeroAttributes[] GetSelectedHeroes()
        {
            return SelectedHeroes.Select(h => h.HeroAttributes).ToArray();
        }

        public void Clear()
        {
            for (int i = 0; i < SelectedHeroes.Count; i++)
            {
                SelectedHeroes[i].ToggleOutline();
            }
            
            SelectedHeroes.Clear();
        }

        public void HandleHeroUIElementPointerUp(HeroUIElement heroUIElement)
        {
            if (SelectedHeroes.Contains(heroUIElement))
            {
                heroUIElement.ToggleOutline();
                SelectedHeroes.Remove(heroUIElement);
                return;
            }
            
            if (SelectedHeroes.Count >= DataContainers.GameDesignValues.maxSelectableHeroAmount)
            {
                SelectedHeroes[0].ToggleOutline();
                SelectedHeroes.RemoveAt(0);
            }
            
            SelectedHeroes.Add(heroUIElement);
            heroUIElement.ToggleOutline();
        }
    }
}