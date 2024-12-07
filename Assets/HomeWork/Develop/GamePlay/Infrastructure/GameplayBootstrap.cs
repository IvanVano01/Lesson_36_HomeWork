using Assets.HomeWork.Develop.CommonServices.DI;
using Assets.HomeWork.Develop.CommonServices.SceneManagment;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("����� ������, ����� �������� ����");

            yield return new WaitForSeconds(1f);// ���������� ��������
        }

        private void ProcessRegistrations()
        {
            // ������ ����������� ��� ����� �������
        }

        private void Update()// ��� �����
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _container.Resolve<SceneSwitcher>().ProcessSwitcherSceneFor(new OutputGamplayArgs(new MainMenuInputArgs()));
            }
        }
    }
}