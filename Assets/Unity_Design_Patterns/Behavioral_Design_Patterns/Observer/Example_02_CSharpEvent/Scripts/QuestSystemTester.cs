using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_02_CSharpEvent
{
    /// <summary>
    /// Triggers QuestSystem methods via keyboard input for testing purposes.
    /// Attach to any GameObject in the scene and assign the QuestSystem reference.
    ///
    /// Space — StartQuest
    /// C     — CompleteQuest
    /// F     — FailQuest
    /// </summary>
    public class QuestSystemTester : MonoBehaviour
    {
        [SerializeField] private QuestSystem _questSystem;

        private void Update()
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame) _questSystem.StartQuest();
            if (Keyboard.current.cKey.wasPressedThisFrame) _questSystem.CompleteQuest(new QuestData { QuestId = 1, RewardXP = 100, QuestName = "Slay the Dragon" });
            if (Keyboard.current.fKey.wasPressedThisFrame) _questSystem.FailQuest(1);
        }
    }
}
