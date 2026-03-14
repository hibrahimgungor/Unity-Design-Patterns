using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_00_NoPattern._01_DirectCall
{
    /// <summary>
    /// Reacts to quest completion via a direct method call from QuestSystem.
    /// QuestSystem holds a reference to this class and calls its methods explicitly.
    /// </summary>
    public class QuestReward : MonoBehaviour
    {
        public void OnQuestCompleted(QuestData data) => Debug.Log($"QuestReward: Granting {data.RewardXP} XP for quest '{data.QuestName}'.");
    }
}
