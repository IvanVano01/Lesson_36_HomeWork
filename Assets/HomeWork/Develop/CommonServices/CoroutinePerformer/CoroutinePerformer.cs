using System.Collections;
using UnityEngine;

namespace Assets.HomeWork.Develop.CommonServices.CoroutinePerformer
{
    public class CoroutinePerformer : MonoBehaviour, ICoroutinePerformer // для запуска корутин
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        public Coroutine StartPerform(IEnumerator coroutineFunction) => StartCoroutine(coroutineFunction);// принимаем "IEnumerator" и стартуем его     

        public void StopPerform(Coroutine coroutine) => StopCoroutine(coroutine);// принимаем "IEnumerator" и останавливаем корутину         
    }
}
