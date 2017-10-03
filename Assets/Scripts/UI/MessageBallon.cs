using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DDP.UI
{
    public class MessageBallon : MonoBehaviour
    {
        [SerializeField]
        private Text messageText;
        private void Awake()
        {
        }

        public static void Show(Transform parent, Vector3 localPos, string message, float time)
        {
            MessageBallon ballonInstance = Instantiate(Resources.Load<GameObject>("Prefabs/UI/MessageBallon")).GetComponent<MessageBallon>();
            ballonInstance.messageText.text = message;
            ballonInstance.StartCoroutine(ballonInstance.ShowProcess(time));
            ballonInstance.transform.SetParent(parent);
            ballonInstance.transform.localPosition = localPos;
        }

        private IEnumerator ShowProcess(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(this.gameObject);
        }
    }
}