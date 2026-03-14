using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._02_ListenerComponent
{
    /// <summary>
    /// Reacts to quest completion by granting rewards.
    ///
    /// Methods are public so they can be wired to an EventListener's UnityEvent
    /// in the Inspector. This class holds no channel references and no subscription
    /// code — all wiring is done via the IntEventListener component in the scene.
    /// </summary>
    public class QuestReward : MonoBehaviour
    {
        public void OnQuestCompleted(QuestData data) => Debug.Log($"QuestReward: Granting {data.RewardXP} XP for quest '{data.QuestName}'.");
    }
}
