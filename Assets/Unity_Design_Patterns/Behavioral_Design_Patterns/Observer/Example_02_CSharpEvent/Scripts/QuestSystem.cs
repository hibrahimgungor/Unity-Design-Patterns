using System;
using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_02_CSharpEvent
{
    /// <summary>
    /// Concrete subject rebuilt with C# events instead of ISubject/IObserver interfaces.
    ///
    /// Each event type becomes its own channel. Observers subscribe to exactly the events
    /// they care about — a listener interested only in quest completion does not receive
    /// start or failure notifications.
    ///
    /// Compared to Classic Observer:
    /// - No ISubject or IObserver interface required.
    /// - No manual observer list or NotifyObservers loop.
    /// - Multiple event types are handled cleanly without a single OnNotify entry point.
    /// - Invocation is restricted to this class — external classes cannot trigger events directly.
    ///
    /// Scene setup:
    /// Attach this component to a GameObject. All observer components must hold a reference
    /// to this component via the Inspector to subscribe in OnEnable.
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        public event Action OnQuestStarted;
        public event Action<QuestData> OnQuestCompleted;
        public event Action<int> OnQuestFailed;

        public void StartQuest()
        {
            Debug.Log("QuestSystem: Quest started.");
            OnQuestStarted?.Invoke();
        }

        public void CompleteQuest(QuestData data)
        {
            Debug.Log("QuestSystem: Quest completed.");
            OnQuestCompleted?.Invoke(data);
        }

        public void FailQuest(int questId)
        {
            Debug.Log("QuestSystem: Quest failed.");
            OnQuestFailed?.Invoke(questId);
        }
    }
}
