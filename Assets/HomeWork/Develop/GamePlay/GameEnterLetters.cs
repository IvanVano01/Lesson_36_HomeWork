using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.HomeWork.Develop.GamePlay
{
    public class GameEnterLetters : IGameMode
    {
        private List<char> _randomChairList;
        private List<char> _сhairList;
        private List<char> _inputCharKeys;

        private int _maxAmountCharLength = 4;

        private bool _isRunning;

        public bool IsGameOver { get; private set; }
        public bool IsWin { get; private set; }

        public GameEnterLetters()
        {
            _inputCharKeys = new();
            _randomChairList = new List<char>();
            _сhairList = new List<char>();
            _сhairList.AddRange(new[]{
                'a','b', 'c', 'd','e','f','g','h','j','k','l'
            });


            _isRunning = true;
            IsWin = false;
            IsGameOver = false;

            ToGenerateRandomCharArray();
            ToDisplayOnScreen();
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            ToEnterChar();
        }

        private void ToProcessingGameResult()
        {
            if (_inputCharKeys.SequenceEqual(_randomChairList))
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

        private void ToEnterChar()
        {
            if (_inputCharKeys.Count >= _randomChairList.Count)
                return;

            if (Input.anyKeyDown)
            {
                Char char1;
                string inputKey = Input.inputString;

                if (char.TryParse(inputKey, out char result))
                {
                    char1 = result;
                    Debug.Log(" Вы ввели букву " + char1);
                    _inputCharKeys.Add(char1);

                    if (_inputCharKeys.Count == _randomChairList.Count)
                    {
                        ToProcessingGameResult();
                        return;
                    }
                    Debug.Log(" Введите следущую букву");

                    return;
                }
                Debug.Log(" Вы нажали не известную клавишу, попробуйте ещё раз " + Input.inputString);
            }
        }

        private void ToGenerateRandomCharArray()
        {
            for (int i = 0; i < _maxAmountCharLength; i++)
                _randomChairList.Add(GenerateRandomChar());
        }

        private void ToDisplayOnScreen() => Debug.Log(string.Join(" , ", _randomChairList));

        private char GenerateRandomChar()
        {           
            int randomElement = UnityEngine.Random.Range(0, _сhairList.Count);
           
            return _сhairList[randomElement];
        }
    }
}
