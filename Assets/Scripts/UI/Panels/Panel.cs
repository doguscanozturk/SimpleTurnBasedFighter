using UI.Elements;

namespace UI.Panels
{
    public class Panel : BasicUIElement
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            Hide();
        }
    }
}