using System;

namespace Assets.HomeWork.Develop.Utils.Reactive
{
    public class ReactiveVariable<T> : IReadOnlyVariable<T> where T : IEquatable<T> // реактивное свойство,
                                                                                    //IReadOnlyVariable<T> интерфейс для подписки или считывания нашего реактивного свойства
                                                                                    // IEquatable<T> - это интерфейс для сравнения int,float и других переменных
    {
        public event Action<T, T> Changed;// событие с двумя параметрами на всякий случай, для сравнения старого значения и нового.

        private T _value;

        public ReactiveVariable() => _value = default(T);// коструктор с дефолтным значением переменной "T"

        public ReactiveVariable(T value) => _value = value; // конструктор, где сами будем задавать значение переменной "T"

        public T Value // свойство для проверки и считывания переменной "T"
        {
            get => _value;
            set
            {
                T oldValue = _value;
                _value = value;

                if (_value.Equals(oldValue) == false)// проверяем, отлечается ли новое значение переменной "T" от старого
                    Changed?.Invoke(oldValue, oldValue);// вызываем событие для обновления значение, кому это значение требуется
            }
        }
    }
}
