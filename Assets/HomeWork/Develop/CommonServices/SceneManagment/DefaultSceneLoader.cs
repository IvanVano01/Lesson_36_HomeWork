using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace Assets.HomeWork.Develop.CommonServices.SceneManagment
{
    public class DefaultSceneLoader : ISeneLoader
    {
        public IEnumerator LoadAsync(SceneID sceneID, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
           AsyncOperation waitLoading = SceneManager.LoadSceneAsync(sceneID.ToString(), loadSceneMode); 
            
            while(waitLoading.isDone == false)
            {
                yield return null;
            }
        }
    }
}
