using System;
using DG.Tweening;
using UnityEngine;

namespace Core.UI
{
    public class MenuSlider : MonoBehaviour
    {
        [SerializeField] private RectTransform _menu;

        private void Start()
        {
            _startX = _menu.anchoredPosition.x;
        }

        public void Slide()
        {
            if (_isOn)
            {
                _menu.DOLocalMoveX(_menu.localPosition.x - _menu.rect.width - _startX, 0.2f);
            }
            else
            {
                _menu.DOLocalMoveX(_menu.localPosition.x + _menu.rect.width + _startX, 0.2f);
            }

            _isOn = !_isOn;
        }

        private float _startX = 0;
        private bool _isOn = true;
    }
}