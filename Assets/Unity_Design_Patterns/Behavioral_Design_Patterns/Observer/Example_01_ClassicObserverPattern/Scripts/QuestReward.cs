using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_01_ClassicObserverPattern
{
    /// <summary>
    /// Concrete observer that represents the reward layer.
    /// Registers only with QuestCompletedSubject — rewards are granted on success only.
    /// No cast to a specific event type is needed — the subject itself carries the data.
    /// </summary>
    public class QuestReward : MonoBehaviour, IObserver
    {
        [SerializeField] private QuestCompletedSubject _questCompletedSubject;

        private void OnEnable() => _questCompletedSubject.AddObserver(this);
        private void OnDisable() => _questCompletedSubject.RemoveObserver(this);

        public void OnNotify(ISubject subject)
        {
            if (subject is QuestCompletedSubject completed)
                Debug.Log($"QuestReward: Granting {completed.LastQuestData.RewardXP} XP for quest '{completed.LastQuestData.QuestName}'.");
        }
    }
}
