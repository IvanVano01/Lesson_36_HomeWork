using System;
using System.Collections.Generic;

namespace Assets.HomeWork.Develop.CommonServices.DI
{
    public class DIContainer : IDisposable // контеёнер локальный в каждой сцене свой
    {
        private readonly Dictionary<Type, Registration> _container = new(); // список(контейнер) уже зареганных объектов

        private readonly DIContainer _parent;//поле для родительского контеёнера(глобальный для всех сцен)

        private readonly List<Type> _request = new List<Type>();// временный список запрашиваемых при регестрации типов объектов

        public DIContainer() : this(null) // конструктор пустой
        {

        }

        public DIContainer(DIContainer parent) => _parent = parent;// конструктор с передачей родительского контейнера        

        public Registration RegisterAsSingle<T>(Func<DIContainer, T> creator)// метод регистрации зависимости в единственном экземпляре
        {
            if (_container.ContainsKey(typeof(T))) // если конеёнер уже содержит регистрацию того типа который хотим зарегестрировать, выкидываем ошибку
                throw new InvalidOperationException($"{nameof(T)} Такой тип уже зарегистрован! ");

            Registration registration = new Registration(container => creator(container));// создаём регистрацию и указываем способ создания????????????????????

            // 1) создаём экземпляр класса "Registration" и передаём в его конструктор параметры 
            // 2) в качестве параметров, передаём DI контейнер и у него вызываем делегат " Funk Creator" который вернёт зареганный объект

            //_container.Add(typeof(T), registration);// добавляем регистрацию созданную выше в словарь контейнер
            _container[typeof(T)] = registration;// вариант записи

            return registration;
        }

        public T Resolve<T>()// метод для получения зарегестрированных объектов, смотрим зареганый объект в локальном контейнере, если нет то в родительском контейнере
        {
            if (_request.Contains(typeof(T)))
                throw new InvalidOperationException($" Cycle resolve {typeof(T)}");// для отслеживания циклических ошибок(когда одному объекту для регистрации требуется другой, а другому первый)

            _request.Add(typeof(T));// записали тип объекта в лист

            try
            {
                if (_container.TryGetValue(typeof(T), out Registration registration))// запрашиваем у контеёнера информацию о регистрации сервиса который хотим получить
                    return CreateFrom<T>(registration);

                if (_parent != null)// если у родительского контейнера уже есть регистрация такого объекта
                    return _parent.Resolve<T>();// забираем из родительского контейнера
            }
            finally
            {
                _request.Remove(typeof(T));// удаляем из временного листа запрашиваемый тип объекта, т.к уже получили его
            }

            throw new InvalidOperationException($" Registration for {typeof(T)} not exist");
        }

        public void Initialize()// инициализируем контейнер, на предмет маркировки и создания объектов, которые нужны до за ранее, до первого запроса их
        {
            foreach (Registration registration in _container.Values)// проходимся по каждой регистрации
            {
                if (registration.Instance == null && registration.IsNoLazy) // если поле "registration.Instance" пустое и маркер "IsNoLazy"
                {
                    registration.Instance = registration.Creator(this);// создаём регистрацию,т.е для тех объектов которые нужно создать заранее,
                                                                       // до первого обращения               
                }
                //для автоматической инициализации
                if (registration.Instance != null) // если поле "registration.Instance" не пустое, хранит всебе ссылку на объект
                    if(registration.Instance is IInitializable initializable)// если объект который храниться в поле "registration.Instance"
                                                                             // унаследован от интерфейса "IInitializable"
                    {
                        initializable.Initialise();// то вызываем у объекта метод инициализации
                    }
            }
        }

        public void Dispose()// для отписок и дициализации
        {
            foreach(Registration registration in _container.Values)
            {
                if(registration.Instance != null)
                    if( registration.Instance is IDisposable disposable)
                        disposable.Dispose();
            }
        }

        private T CreateFrom<T>(Registration registration)
        {
            if (registration.Instance == null && registration.Creator != null)// если мы ещё не регестрировали объект
                registration.Instance = registration.Creator(this);

            return (T)registration.Instance;
        }

        public class Registration// класс регистрации объектов
        {
            public Func<DIContainer, object> Creator { get; } //принимает контёйнер, возвращает зареганный объект

            public object Instance { get; set; }// св-во для уже созданного объекта, который просто нужно зарегать (уже созданного если не равна null)
            public bool IsNoLazy { get; private set; }// булка(маркер) для маркировки не ленивого создания объекта,
                                                      // т.е для тех объектов которые нужно создать заранее, до первого обращения

            // конструкторы
            public Registration(object instance) => Instance = instance;// если запрашиваемый объект, уже создан

            public Registration(Func<DIContainer, object> creator)// если запрашиваемый объект ещё не создан, регаем способ создания
            {
                Creator = creator;
            }

            public void NonLazy() => IsNoLazy = true;
        }
    }
}
