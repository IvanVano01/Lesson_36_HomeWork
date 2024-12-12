using Assets.HomeWork.Develop.CommonServices.AssetManagment;
using Assets.HomeWork.Develop.Configs.Comnon.Wallet;

namespace Assets.HomeWork.Develop.CommonServices.ConfigsManagment
{
    public class ConfigsProviderService// класс для подгрузки конфигов из скриптблОбджектс
    {
        private ResourcesAssetLoader _resourcesAssetLoader;

        public ConfigsProviderService(ResourcesAssetLoader resourcesAssetLoader)
        {
            _resourcesAssetLoader = resourcesAssetLoader;
        }

        public StartWalletConfig StartWalletConfig { get; private set; }// делаем свойство для поля "StartWalletConfig"( ScriptableObject)

        public void LoadAll()
        {
            // подгружаем конфиги из папки рессурсов(Resources)
            LoadStartWalletConfig();
        }

        private void LoadStartWalletConfig()
            => StartWalletConfig = _resourcesAssetLoader.LoadResource<StartWalletConfig>("Configs/Common/Wallet/StartWalletConfig");

    }
}
