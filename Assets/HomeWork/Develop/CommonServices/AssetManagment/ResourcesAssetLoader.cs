using UnityEngine;

namespace Assets.HomeWork.Develop.CommonServices.AssetManagment
{
    public class ResourcesAssetLoader 
    {
        public T LoadResource<T>(string resourcePath) where T : Object => Resources.Load<T>(resourcePath);// ������ ��� ��������� �������� �� ����� "Resource"

    }
}