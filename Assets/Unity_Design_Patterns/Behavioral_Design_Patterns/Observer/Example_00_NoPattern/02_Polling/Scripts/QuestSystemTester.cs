using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_00_NoPattern._02_Polling
{
    /// <summary>
    /// Triggers QuestSystem methods via keyboard input for testing purposes.
    /// Calls ResetFlags() in LateUpdate so all observers have a chance to react
    /// within the same frame before flags are cleared.
    ///
    /// ResetFlags() is intentionally placed in LateUpdate — not Update. If reset
    /// happened in the same Update pass as the flag was set, observers running
    /// later in the frame would miss the change entirely. LateUpdate guarantees
    /// all Update calls across all components complete before flags are cleared.
    ///
    /// Notice that reset responsibility still falls here — in a real project it is
    /// unclear who should own this. This is one of the core problems polling introduces.
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
            if (Keyboard.current.cKey.wasPressedThisFrame)     _questSystem.CompleteQuest(new QuestData { QuestId = 1, RewardXP = 100, QuestName = "Slay the Dragon" });
            if (Keyboard.current.fKey.wasPressedThisFrame)     _questSystem.FailQuest(1);
        }

        private void LateUpdate()
        {
            _questSystem.ResetFlags();
        }
    }
}
