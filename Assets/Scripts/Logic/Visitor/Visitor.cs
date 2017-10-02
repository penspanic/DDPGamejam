using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace DDP.Logic
{
    public class Visitor : MonoBehaviour, ISynableObject
    {
		public Data.Visitor Data { get { return _data; } set { _data = value; } }
        private Data.Visitor _data;

		[SerializeField]
		private SpriteRenderer[] sprRenders;
        [SerializeField]
        private SortingGroup sortingGroup;

		public Dictionary<string, Sprite> sprs { get; private set; }
        private Logic.Room selectedRoom;
        private Constants.FacilityType selectedFacility;
        private Constants.FoodType selectedFood;

        private void Awake()
        {
			sprs = new Dictionary<string, Sprite>();
        }

        public void Sync(ISyncableData data)
        {
            _data = (Data.Visitor)data;
        }

        public void SetSerial(int serial)
        {
            _data.Serial = serial;
        }

		public void ApplySpriteSources(Constants.RaceType raceType)
		{
			string filePath = "Sprites/Character/" + raceType.ToString();

			var sprites = Resources.LoadAll<Sprite>(filePath);
			if (sprites == null || sprites.Length == 0)
				throw new UnityException("Don't have Resources!!");

			sprs.Clear();
			for (int i = 0; i < sprRenders.Length; ++i)
			{
				sprRenders[i].sprite = null;

				for (int j = 0; j < sprites.Length; ++j)
				{
					if (sprRenders[i].name == sprites[j].name)
					{
						sprRenders[i].sprite = sprites[j];
						sprs.Add(sprites[j].name, sprites[j]);
					}
				}
			}

			sortingGroup.sortingOrder = -_data.Serial;
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
        }

        public void SetRoom(Logic.Room room)
        {
            this.selectedRoom = room;
            VisitorManager.Instance.OnRoomSelected();
        }

        public void SetFacility(Constants.FacilityType facility)
        {
            this.selectedFacility = facility;
            VisitorManager.Instance.OnFacilitySelected();
        }

        public void SetFood(Constants.FoodType food)
        {
            this.selectedFood = food;
            VisitorManager.Instance.OnFoodSelected();
        }

		public SpriteRenderer[] GetRenderers()
		{
			return sprRenders;
		}
    }
}