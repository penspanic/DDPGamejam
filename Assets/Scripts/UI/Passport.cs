using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace DDP.UI
{
    public class Passport : MonoBehaviour
    {
        [SerializeField]
        private Text nameText;
        [SerializeField]
        private Text sexText;
        [SerializeField]
        private Text raceText;
        [SerializeField]
        private Text jobText;
        [SerializeField]
        private Text attributeText;

        private void Awake()
        {
            Logic.VisitorManager.Instance.OnVisitorCreated += OnVisitorCreated;
        }

        private void OnVisitorCreated(Logic.Visitor visitor)
        {
            nameText.text = visitor.Info.Name;
            sexText.text = visitor.Info.Sex == Constants.SexType.Male ? "남자" : "여자";
            raceText.text = visitor.Info.RaceName;
            jobText.text = visitor.Info.JobName;
            attributeText.text = visitor.Info.AttributeDescription;
        }
    }
}