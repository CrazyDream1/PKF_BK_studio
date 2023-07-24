using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MyObject : MonoBehaviour
    {
        void Awake()
        {
            _material = this.gameObject.GetComponent<Renderer>().material;
        }

        public void ChangeColor(Color newColor)
        {
            Color color = _material.color;
            _material.color = new Color(newColor.r, newColor.g, newColor.b, color.a);
        }

        public void ChangeTransparency(float alpha)
        {
            Color color = _material.color;
            _material.color = new Color(color.r, color.g, color.b, alpha);
        }
        
        public void SwitchTransparency()
        {
            Color color = _material.color;
            if (_material.color.a > 0)
            {
                _material.color = new Color(color.r, color.g, color.b, 0);
            }
            else
            {
                _material.color = new Color(color.r, color.g, color.b, 1);
            }
        }
        
        private Material _material;
    }
}