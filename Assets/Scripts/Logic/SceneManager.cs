using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace DDP
{
    public class SceneManager : Singleton<SceneManager>
    {
        public bool IsChanging { get; private set; }
        protected override void Awake()
        {
            base.Awake();
        }

        public void ChangeScene(string sceneId, Constants.SceneChangeEffect changeEffect = Constants.SceneChangeEffect.FadeOut)
        {
            if(IsChanging == true)
            {
                return;
            }

            StartCoroutine(SceneChangeProcess(sceneId, changeEffect, 1f));
        }
        
        private IEnumerator SceneChangeProcess(string sceneId, Constants.SceneChangeEffect changeEffect, float time)
        {
            IsChanging = true;

            yield return new WaitForSeconds(time);

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneId);
            IsChanging = false;
        }
    }
}