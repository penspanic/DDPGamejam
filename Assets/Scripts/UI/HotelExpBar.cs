using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DDP.UI
{
    public class HotelExpBar : MonoBehaviour
    {
        [SerializeField]
        private Image fillImage;
        private void Awake()
        {
            Logic.HotelManager.Instance.OnExpRateChanged += OnExpRateChanged;
        }

        private void OnExpRateChanged(float rate)
        {
            fillImage.fillAmount = rate;
        }
    }
}