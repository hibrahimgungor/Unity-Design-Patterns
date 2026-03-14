using System.Collections.Generic;
using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_01_ClassicObserverPattern
{
    /// <summary>
    /// Concrete subject that notifies observers when a quest becomes active.
    /// Carries no data — the notification itself is the meaningful signal.
    /// </summary>
    public class QuestStartedSubject : MonoBehaviour, ISubject
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        public void AddObserver(IObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer) => _observers.Remove(observer);

        public void NotifyObservers()
        {
            for (int i = _observers.Count - 1; i >= 0; i--)
                _observers[i].OnNotify(this);
        }
    }
}
