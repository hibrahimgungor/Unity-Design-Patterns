using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_06_EventBus
{
    /// <summary>
    /// Concrete publisher that raises events through the EventBus.
    ///
    /// Holds no reference to any subscriber — complete decoupling from observers.
    /// Any system in the project can subscribe to any event type without this
    /// class knowing about it.
    ///
    /// Compared to EventChannel from Example 05:
    /// EventChannel required a reference to a specific static class per event type.
    /// EventBus provides a single entry point — all events are published through
    /// the same class regardless of type.
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        public void StartQuest()
        {
            Debug.Log("QuestSystem: Quest started.");
            EventBus.Publish(new QuestStartedEvent());
        }

        public void CompleteQuest(QuestCompletedEvent data)
        {
            Debug.Log("QuestSystem: Quest completed.");
            EventBus.Publish(data);
        }

        public void FailQuest(int questId)
        {
            Debug.Log("QuestSystem: Quest failed.");
            EventBus.Publish(new QuestFailedEvent(questId));
        }
    }
}
