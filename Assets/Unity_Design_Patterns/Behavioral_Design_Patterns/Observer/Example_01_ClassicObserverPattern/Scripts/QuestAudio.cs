using UnityEngine;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_01_ClassicObserverPattern
{
    /// <summary>
    /// Concrete observer that represents the audio layer.
    /// Registers only with QuestStartedSubject and QuestCompletedSubject —
    /// no sound is needed when a quest fails.
    /// </summary>
    public class QuestAudio : MonoBehaviour, IObserver
    {
        [SerializeField] private QuestStartedSubject _questStartedSubject;
        [SerializeField] private QuestCompletedSubject _questCompletedSubject;

        private void OnEnable()
        {
            _questStartedSubject.AddObserver(this);
            _questCompletedSubject.AddObserver(this);
        }

        private void OnDisable()
        {
            _questStartedSubject.RemoveObserver(this);
            _questCompletedSubject.RemoveObserver(this);
        }

        public void OnNotify(ISubject subject)
        {
            if (subject is QuestStartedSubject)
                Debug.Log("QuestAudio: Playing quest start sound.");
            else if (subject is QuestCompletedSubject)
                Debug.Log("QuestAudio: Playing quest complete sound.");
        }
    }
}
