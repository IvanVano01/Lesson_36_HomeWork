using UnityEngine.SceneManagement;
using System.Collections;

namespace Assets.HomeWork.Develop.CommonServices.SceneManagment
{
    public interface ISeneLoader
    {
        IEnumerator LoadAsync(SceneID sceneID, LoadSceneMode loadSceneMode = LoadSceneMode.Single);// метод загрузки сцен
                                                                                                   // ("LoadSceneMode.Single это значит сцена выгрузится/загрузится полностью
                                                                                                   // есть ещё режимы подгрузки нескольких сцен в одну и т.д")
    }
}
