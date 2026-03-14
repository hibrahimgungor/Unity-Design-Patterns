using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._01_CSharpEvent
{
    /// <summary>
    /// Concrete publisher that raises events through the EventChannel.
    /// Holds no reference to any subscriber — complete decoupling from observers.
    /// Any system in the project can subscribe without this class knowing about it.
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        public void StartQuest()
        {
            Debug.Log("QuestSystem: Quest started.");
            EventChannel<QuestStartedEvent>.Raise(new QuestStartedEvent());
        }

        public void CompleteQuest(QuestCompletedEvent data)
        {
            Debug.Log("QuestSystem: Quest completed.");
            EventChannel<QuestCompletedEvent>.Raise(data);
        }

        public void FailQuest(int questId)
        {
            Debug.Log("QuestSystem: Quest failed.");
            EventChannel<QuestFailedEvent>.Raise(new QuestFailedEvent(questId));
        }
    }
}
