using UnityEngine;

namespace Core.UI
{
    public class ButtonHideShowAll : MonoBehaviour
    {
        [SerializeField] private ScrollViewContentController _scrollViewContentController;
        
        public void Change()
        {
            if (_isShown)
            {
                _scrollViewContentController.HideAll();
            }
            else
            {
                _scrollViewContentController.ShowAll();
            }

            _isShown = !_isShown;
        }
        
        private bool _isShown = true;
    }
}