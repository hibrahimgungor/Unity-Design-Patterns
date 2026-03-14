using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_01_ClassicObserverPattern
{
    /// <summary>
    /// Concrete observer that represents the UI layer.
    /// Registers with all three subjects — reacts to start, completion, and failure.
    ///
    /// Each OnNotify call identifies the source subject by casting and pulls
    /// the relevant data. This is the pull model in practice.
    /// </summary>
    public class QuestUI : MonoBehaviour, IObserver
    {
        [SerializeField] private QuestStartedSubject _questStartedSubject;
        [SerializeField] private QuestCompletedSubject _questCompletedSubject;
        [SerializeField] private QuestFailedSubject _questFailedSubject;

        private void OnEnable()
        {
            _questStartedSubject.AddObserver(this);
            _questCompletedSubject.AddObserver(this);
            _questFailedSubject.AddObserver(this);
        }

        private void OnDisable()
        {
            _questStartedSubject.RemoveObserver(this);
            _questCompletedSubject.RemoveObserver(this);
            _questFailedSubject.RemoveObserver(this);
        }

        public void OnNotify(ISubject subject)
        {
            if (subject is QuestStartedSubject)
                Debug.Log("QuestUI: Showing quest started screen.");
            else if (subject is QuestCompletedSubject completed)
                Debug.Log($"QuestUI: Quest '{completed.LastQuestData.QuestName}' completed. Earned {completed.LastQuestData.RewardXP} XP.");
            else if (subject is QuestFailedSubject failed)
                Debug.Log($"QuestUI: Showing quest failed screen. Quest ID: {failed.LastFailedQuestId}");
        }
    }
}
