using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DDP.UI
{
    public class SelectMessage : Singleton<SelectMessage>
    {
        [SerializeField]
        private Sprite[] sprites;
        [SerializeField]
        private Image messageImage;

        public void Show(int index)
        {
            messageImage.enabled = true;
            messageImage.sprite = sprites[index];
        }
        
        public void Hide()
        {
            messageImage.enabled = false;
        }
    }
}