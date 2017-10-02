using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DDP.UI
{
    [RequireComponent(typeof(Text))]
    public class MessageBallon : MonoBehaviour
    {
        private Text messageText;
        private void Awake()
        {
            messageText = GetComponent<Text>();
        }

        public void Show(string message, float time)
        {

        }

        private IEnumerator ShowProcess(float time)
        {
            this.gameObject.SetActive(true);
            yield return new WaitForSeconds(time);
            this.gameObject.SetActive(false);
        }
    }
}