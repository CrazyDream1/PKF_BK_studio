using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class ScrollViewContentController : MonoBehaviour
    {
        [SerializeField] private GameObject _contentObjectExample;
        [SerializeField] private GameObject _content;
        [SerializeField] private FlexibleColorPicker _flexibleColorPicker;
        
        private void Start()
        {
            var objects = GameObject.FindGameObjectsWithTag("Object");
            foreach (var placedObject in objects)
            {
                var newContentObject = Instantiate(_contentObjectExample, _content.transform);
                var contentObject = newContentObject.GetComponent<ScrollViewContentObject>();
                contentObject.Object = placedObject.GetComponent<MyObject>();
                contentObject.ScrollViewContentController = this;
                _objectList.AddLast(new ContentObjectWithBool {Object = contentObject, Selected = false});
            }
        }

        public void Select(ScrollViewContentObject scrollViewContentObject)
        {
            foreach (var current in _objectList)
            {
                if (scrollViewContentObject == current.Object)
                {
                    current.Selected = !current.Selected;
                    current.Object.WasSelected(current.Selected);
                    break;
                }
            }
        }

        public void ShowAll()
        {
            foreach (var current in _objectList)
            {
                current.Object.ChangeTransparency(1);
            }
        }

        public void HideAll()
        {
            foreach (var current in _objectList)
            {
                current.Object.ChangeTransparency(0);
            }
        }
        
        public void SelectAll()
        {
            foreach (var current in _objectList)
            {
                current.Selected = true;
                current.Object.WasSelected(true);
            }
        }
        
        public void UnselectAll()
        {
            foreach (var current in _objectList)
            {
                current.Selected = false;
                current.Object.WasSelected(false);
            }
        }
        
        public void ApplyColor()
        {
            Color newColor = _flexibleColorPicker.color;
            foreach (var contentObject in _objectList)
            {
                if (contentObject.Selected)
                {
                    contentObject.Object.ChangeColor(newColor);
                }
            }
        }
        
        public void ApplyTransparency(float alpha)
        {
            foreach (var contentObject in _objectList)
            {
                if (contentObject.Selected)
                {
                    contentObject.Object.ChangeTransparency(alpha);
                }
            }
        }
        
        private LinkedList<ContentObjectWithBool> _objectList = new();

        class ContentObjectWithBool
        {
            public ScrollViewContentObject Object;
            public bool Selected;
        }
    }
}