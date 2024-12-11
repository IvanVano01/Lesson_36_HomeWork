using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay
{
    public class GameEnterNumbers : IGameMode
    {
        private List<int> _randomNumbersList;
        private List<int> _inputNunberKeys;

        private int _maxAmountNumber = 10;
        private int _maxAmountNumberLength = 4;

        private bool _isRunning;

        public bool IsGameOver { get; private set; }
        public bool IsWin { get; private set; }

        public GameEnterNumbers()
        {
            _randomNumbersList = new();
            _inputNunberKeys = new();

            _isRunning = true;
            IsWin = false;
            IsGameOver = false;

            ToGenerateRandomNumbersArray();
            ToDisplayOnScreen();
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            ToEnterNumber();
        }

        private void ToProcessingGameResult()
        {
            if (_inputNunberKeys.SequenceEqual(_randomNumbersList))
            {
                Debug.Log(" Ура вы победили!");
                Debug.Log(" Нажмите пробел что бы перейти в Главное меню ");
                _isRunning = false;
                IsGameOver = true;
                IsWin = true;
                // переходим с геймплэй на сцену Главного меню
            }
            else
            {
                Debug.Log(" Вы попроиграли! + \n  Нажмите пробел и попробуйте ещё раз ");
                _isRunning = false;
                IsGameOver = true;
                IsWin = false;
                // переходим с геймплэй на гемплэй
            }

        }

        private void ToEnterNumber()
        {
            if (_inputNunberKeys.Count >= _randomNumbersList.Count)
                return;

            if (Input.anyKeyDown)
            {
                int number;
                string inputKey = Input.inputString;

                if (int.TryParse(inputKey, out int result))
                {
                    number = result;
                    Debug.Log(" Вы ввели цифру " + number);
                    _inputNunberKeys.Add(number);

                    if (_inputNunberKeys.Count == _randomNumbersList.Count)
                    {
                        ToProcessingGameResult();
                        return;
                    }
                    Debug.Log(" Введите следующую ");

                    return;
                }
                Debug.Log(" Вы нажали не известную клавишу, попробуйте ещё раз " + Input.inputString);
            }
        }

        private void ToGenerateRandomNumbersArray()
        {
            for (int i = 0; i < _maxAmountNumberLength; i++)
                _randomNumbersList.Add(GenerateRandomNumber());
        }

        private void ToDisplayOnScreen() => Debug.Log(string.Join(" , ", _randomNumbersList));
        private int GenerateRandomNumber() => Random.Range(0, _maxAmountNumber);
    }
}
