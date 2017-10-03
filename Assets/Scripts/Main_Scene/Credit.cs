using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace DDP.Main_Scene
{
    public class Credit : Singleton<Credit>
    {
        protected override void Awake()
        {
            base.Awake();
			transform.localPosition = new Vector2(0, -1400);
        }

        public void Show()
        {
			transform.DOLocalMoveY(0f, 0.4f);
        }

        public void Hide()
        {
			transform.DOLocalMoveY(-1400f, 0.4f);
        }
    }
}