using UnityEngine;

namespace Assets.HomeWork.Develop.CommonServices.LoadingScreen
{
    public class StandardLoadingCurtain : MonoBehaviour, ILoadingCurtain
    {
        public bool IsShow => gameObject.activeSelf;

        private void Awake()
        {
            Hide();
            DontDestroyOnLoad(this);
        }

        public void Hide() => gameObject.gameObject.SetActive(false);        

        public void Show() => gameObject.gameObject.SetActive(true);
    }
}
