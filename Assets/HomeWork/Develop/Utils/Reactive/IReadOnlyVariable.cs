using System;

namespace Assets.HomeWork.Develop.Utils.Reactive
{
    public interface IReadOnlyVariable<T> // интерфейс для подписки или считывания нашего реактивного свойства
    {
        event Action<T, T> Changed;

        T Value { get; }
    }
}
