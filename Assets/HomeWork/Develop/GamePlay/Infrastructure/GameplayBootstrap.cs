using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using System.Collections;
using UnityEngine;
using System;

namespace Assets.HomeWork.Develop.GamePlay.Infrastructure
{
    public class GameplayBootstrap : MonoBehaviour
    {
        private DIContainer _container;

        public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
        {
            _container = container;

            ProcessRegistrations();

            Debug.Log($"��������� ������� ��� ������ {gameplayInputArgs.LevelNumber}");
            Debug.Log("������ ���������");


            yield return new WaitForSeconds(1f);// ���������� ��������

            Debug.Log("����� ������, ����� �������� ����");

        }

        private void ProcessRegistrations()
        {
            // ������ ����������� ��� ����� �������

            _container.Initialize();// �������������� ���������, �� ������� ���������� � �������� ��������,
                                    // ������� ����� �� �� �����, �� ������� ������� ��
        }

        private void Update()// ��� �����
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitcherSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
            }

            
        }        
    }
}