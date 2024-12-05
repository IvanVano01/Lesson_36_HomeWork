using Assets.HomeWork.Develop.CommonServices.DI;
using System.Collections;
using UnityEngine;

namespace Assets.HomeWork.Develop.EntryPoint
{
    // EntryPoint - для глобальных регистраций которые стартуют проект
    // BootStrap - для инициализации начала работы
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DIContainer container)
        {
            Debug.Log("Начинается инициализация сервисов в Bootstrap !");
            //Включаем загрузочную штору после всех регистраций

            // Инициализация всех сервисов(конфиги, инит сервисы рекламы/ аналитики)

            yield return new WaitForSeconds(1.5f);// заглушка, имитирует инициализацию сервисов которые выше

            // скрываем штору

            Debug.Log("Завершается инициализация сервисов в Bootstrap, наченаем переход на другую сцену!");
            // переход на следующую сцену с помощью сервиса смены сцены
        }
    }
}
