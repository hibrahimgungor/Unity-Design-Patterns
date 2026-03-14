using System.Collections.Generic;
using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_01_ClassicObserverPattern
{
    /// <summary>
    /// Concrete subject that notifies observers when a quest is failed.
    /// Exposes LastFailedQuestId so observers can identify which quest failed.
    /// </summary>
    public class QuestFailedSubject : MonoBehaviour, ISubject
    {
        private readonly List<IObserver> _observers = new List<IObserver>();

        public int LastFailedQuestId { get; private set; }

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

        public void Raise(int questId)
        {
            LastFailedQuestId = questId;
            NotifyObservers();
        }
    }
}
