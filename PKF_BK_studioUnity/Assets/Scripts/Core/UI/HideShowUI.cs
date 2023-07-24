using UnityEngine;

namespace Core.UI
{
    public class HideShowUI : MonoBehaviour
    {
        public void HideShowSwitch()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}