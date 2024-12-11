using Assets.HomeWork.Develop.CommonServices.DataManagment.DataProviders;
using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using Assets.HomeWork.Develop.CommonServices.Wallet;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.HomeWork.Develop.MainMenu.Infrastructure
{
    public class MainMenuBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
        {
            _container = container;

            ProcessRegistrations();

            Debug.Log($"��������� ������� ��� ����� {mainMenuInputArgs}");            

            yield return new WaitForSeconds(1f);// ���������� ��������
            
        }

        private void ProcessRegistrations()
        {
            // ������ ����������� ��� ����� �������

            _container.Initialize();// �������������� ���������, �� ������� ���������� � �������� ��������,
                                          // ������� ����� �� �� �����, �� ������� ������� ��
        }

        private void Update()
        {            
            if (Input.GetKeyDown(KeyCode.Space))// ��� �����
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitcherSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));
            }

            if(Input.GetKeyDown(KeyCode.F))
            {
                WalletService wallet = _container.Resolve<WalletService>();
                wallet.Add(CurrencyTypes.Gold, 100);
                Debug.Log($"� ������ Gold = {wallet.GetCurrency(CurrencyTypes.Gold).Value}");
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _container.Resolve<PlayerDataProvider>().Save();// � ����������, ����������� "PlayerDataProvider" � � ���� �������� ����� ����������
                Debug.Log(" ��������� ������!");
            }
        }
    }
}