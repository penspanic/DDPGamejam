using System;
namespace DDP.Main_Scene
{
	public class GradeChangeListener : Singleton<GradeChangeListener>
	{

		public bool isUpgrading = false;
		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(this.gameObject);
		}

		public void OnHotelGradeIncreased()
		{
			UnityEngine.Debug.Log("UPGRade!!");
			isUpgrading = true;
			//SceneManager.Instance.ChangeScene("Main");
		}

		public void CheckChangeUpgradeScene()
		{
			if (isUpgrading == false)
				return;

			UnityEngine.Debug.Log("CHAGNE!!!");

			SceneManager.Instance.ChangeScene("Main");
		}
	}
}
