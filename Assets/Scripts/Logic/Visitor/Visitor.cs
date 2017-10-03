using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace DDP.Logic
{
    public class Visitor : MonoBehaviour
    {
		[SerializeField]
		private SpriteRenderer[] sprRenders;
        [SerializeField]
        private SortingGroup sortingGroup;

        public int Serial { get; set; }
        public Sdb.VisitorInfo Info { get; set; }
		public Dictionary<string, Sprite> sprs { get; private set; }
        public Constants.AttributeType SelectedAttribute { get; private set; }
        public Constants.FacilityType SelectedFacility { get; private set; }
        public Constants.FoodType SelectedFood { get; private set; }

        private void Awake()
        {
			sprs = new Dictionary<string, Sprite>();
        }

		public void ApplySpriteSources(Constants.RaceType raceType)
		{
			string filePath = "Sprites/Character/" + raceType.ToString().Split('_')[0];
			string gender = raceType.ToString().Split('_')[1];

			var sprites = Resources.LoadAll<Sprite>(filePath);
			if (sprites == null || sprites.Length == 0)
				throw new UnityException("Don't have Resources!!, path: " + filePath);

			sprs.Clear();
			for (int i = 0; i < sprRenders.Length; ++i)
			{
				sprRenders[i].sprite = null;

				for (int j = 0; j < sprites.Length; ++j)
				{
					var sprName = sprites[j].name;
					var split = sprName.Split('_');

					if (split.Length >= 2)
					{
						if (sprRenders[i].name == split[0])
						{
							if (split[1] == "M" || split[1] == "W")
							{
								if (gender == "M" && split[1] == "M")
								{
									sprRenders[i].sprite = sprites[j];
									sprs.Add(split[0], sprites[j]);
								}
								else if (gender == "W" && split[1] == "W")
								{
									sprRenders[i].sprite = sprites[j];
									sprs.Add(split[0], sprites[j]);
								}
								
							}
							else if (split[0] == "Eyes")
							{
								if (split[1] == "Default")
									sprRenders[i].sprite = sprites[j];

								sprs.Add(split[1], sprites[j]);
							}
						}
					}
					else
					{
						if (sprRenders[i].name == sprName)
						{
							sprRenders[i].sprite = sprites[j];
							sprs.Add(sprName, sprites[j]);
						}
					}
				}
			}

			sortingGroup.sortingOrder = -Serial;
		}

        public void MoveToCounter(Vector3 endPosition)
        {
            StartCoroutine(MoveToCounterProcess(endPosition));
        }

        private IEnumerator MoveToCounterProcess(Vector3 endPos)
        {
            float elapsedTime = 0f;
            float movetime = 2f;
            Vector3 startPos = transform.position;
            while(elapsedTime < movetime)
            {
                elapsedTime += Time.deltaTime;
                transform.position = EasingUtil.EaseVector3(EasingUtil.linear, startPos, endPos, elapsedTime / movetime);
                yield return null;
            }
            transform.position = endPos;

            if(Info.EnterMessages.Length > 0)
            {
                UI.MessageBallon.Show(this.transform, new Vector3(0f, 3.5f, 0f), Info.EnterMessages[Random.Range(0, Info.EnterMessages.Length)], 999999f);
            }
        }

        public void SetRoom(Constants.AttributeType attribute)
        {
            this.SelectedAttribute = attribute;
            VisitorManager.Instance.OnRoomSelected();
        }

        public void SetFacility(Constants.FacilityType facility)
        {
            this.SelectedFacility = facility;
            VisitorManager.Instance.OnFacilitySelected();
        }

        public void SetFood(Constants.FoodType food)
        {
            this.SelectedFood = food;
            VisitorManager.Instance.OnFoodSelected();
        }
    }
}