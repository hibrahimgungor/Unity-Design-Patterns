using UnityEngine;
using UnityEngine.Events;

namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_05_EventChannel._02_ScriptableObject._02_ListenerComponent
{
    /// <summary>
    /// Abstract base class for all ScriptableObject-based event listeners.
    ///
    /// Acts as the bridge between a BaseEventChannelSO and a UnityEvent.
    /// When the channel raises an event, this listener forwards it to the
    /// UnityEvent — which can be wired up entirely in the Inspector without
    /// writing any subscription code.
    ///
    /// This is what makes the approach designer-friendly: QuestUI, QuestAudio,
    /// and QuestReward expose their public methods, and designers connect them
    /// to the appropriate listener's UnityEvent via drag-and-drop.
    ///
    /// Subscription lifecycle follows OnEnable/OnDisable — a disabled GameObject
    /// does not receive notifications.
    /// </summary>
    public abstract class BaseEventListenerSO<T> : MonoBehaviour
    {
        [SerializeField] private BaseEventChannelSO<T> _channel;
        [SerializeField] private UnityEvent<T> _unityEvent;

        private void OnEnable() => _channel.Subscribe(Raise);
        private void OnDisable() => _channel.Unsubscribe(Raise);

        public void Raise(T value) => _unityEvent?.Invoke(value);
    }
}
