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
			OnExpRateChanged(Logic.HotelManager.Instance.GetExpRate());
			Debug.Log("-----HOTEL EXPBAR --- " + fillImage.fillAmount);
        }

		private void OnDestroy()
		{
			Debug.Log("-----HOTEL EXP BAR DESTROY----");
			Logic.HotelManager.Instance.OnExpRateChanged -= OnExpRateChanged;
		}

        private void OnExpRateChanged(float rate)
        {
			Debug.Log("Rate: " + rate);
            fillImage.fillAmount = rate;
        }
    }
}