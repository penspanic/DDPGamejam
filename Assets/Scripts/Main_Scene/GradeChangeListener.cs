using System;
namespace DDP.Main_Scene
{
	public class GradeChangeListener : Singleton<GradeChangeListener>
	{
		protected override void Awake()
		{
			base.Awake();
			DontDestroyOnLoad(this.gameObject);
		}

		public void OnHotelGradeIncreased()
		{
			
		}
	}
}
