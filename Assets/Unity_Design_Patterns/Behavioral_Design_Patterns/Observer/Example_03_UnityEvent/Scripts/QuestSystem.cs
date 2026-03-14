using UnityEngine;
using UnityEngine.Events;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_03_UnityEvent
{
    /// <summary>
    /// Concrete subject rebuilt with UnityEvents instead of C# events.
    ///
    /// UnityEvent is a serializable event type provided by Unity. Unlike C# events,
    /// UnityEvents can be configured directly in the Inspector — listeners can be
    /// assigned without writing any subscription code. This makes them particularly
    /// useful for designer-driven workflows and rapid prototyping.
    ///
    /// Under the hood, UnityEvent is built on C# delegates. It is not a separate
    /// mechanism — it is a layer on top of the same delegate system used in Example 02.
    ///
    /// Two subscription modes:
    /// - Persistent listeners: assigned in the Inspector, serialized with the scene.
    ///   Survive play mode changes. Visible and editable without code.
    /// - Runtime listeners: added via AddListener() in code, just like C# event += syntax.
    ///   Not serialized. Must be removed manually via RemoveListener().
    ///
    /// In this example:
    /// - QuestUI subscribes as a persistent listener via the Inspector.
    /// - QuestAudio subscribes as a runtime listener via AddListener() in code.
    /// This demonstrates both modes side by side.
    ///
    /// Trade-offs compared to C# events:
    /// - Inspector visibility is a major advantage for designers and rapid iteration.
    /// - UnityEvent has higher overhead than a plain C# event — avoid in hot paths.
    /// - Persistent listeners create implicit dependencies that are hard to track in code.
    ///   A method rename silently breaks the Inspector binding.
    /// - Runtime listeners must be removed manually — forgetting RemoveListener causes
    ///   the same memory leak risk as forgetting to unsubscribe from a C# event.
    ///
    /// UnityEvent<T> and serialization:
    /// UnityEvent<T> can be used directly without subclassing in Unity 2020 and later.
    /// UnityEvent<QuestData> and UnityEvent<int> serialize and appear in the Inspector
    /// without requiring a [System.Serializable] wrapper class. Note that complex types
    /// like QuestData cannot be entered as static values in the Inspector — persistent
    /// listeners work best with primitives. Runtime listeners via AddListener() handle
    /// all data types without restriction.
    ///
    /// Scene setup:
    /// Attach this component to a GameObject. Assign QuestUI's handler method to
    /// OnQuestCompleted in the Inspector. QuestAudio subscribes in code automatically.
    /// </summary>
    public class QuestSystem : MonoBehaviour
    {
        public UnityEvent OnQuestStarted;
        public UnityEvent<QuestData> OnQuestCompleted;
        public UnityEvent<int> OnQuestFailed;

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
