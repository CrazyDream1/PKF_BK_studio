using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class ToggleSelectAll : MonoBehaviour
    {
        [SerializeField] private ScrollViewContentController _scrollViewContentController;

        private void Start()
        {
            _toggle = GetComponent<Toggle>().isOn;
        }

        public void Change()
        {
            if (_toggle)
            {
                _scrollViewContentController.UnselectAll();
            }
            else
            {
                _scrollViewContentController.SelectAll();
            }

            _toggle = !_toggle;
        }
        
        private bool _toggle = false;
    }
}