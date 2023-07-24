using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class ScrollViewContentObject : MonoBehaviour
    {
        [SerializeField] private Toggle _toggle;
        [HideInInspector] public MyObject Object;
        [HideInInspector] public ScrollViewContentController ScrollViewContentController;
        
        public void ChangeColor(Color newColor)
        {
            Object.ChangeColor(newColor);
        }
        
        public void ChangeTransparency(float alpha)
        {
            Object.ChangeTransparency(alpha);
        }

        public void ChangeVisibility()
        {
            Object.SwitchTransparency();
        }
        
        public void WasSelected(bool newState)
        {
            _toggle.SetIsOnWithoutNotify(newState);
        }

        public void Select()
        {
            ScrollViewContentController.Select(this);
        }
        
    }
}