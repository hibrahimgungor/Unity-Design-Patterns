using UnityEngine;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// Demonstrates that <see cref="EventManager"/> is created automatically on first access
    /// without any manual scene setup, and persists across scene transitions.
    /// Attach to any GameObject in the scene to run the example.
    /// </summary>
    public class LazyPersistentSingletonExample : MonoBehaviour
    {
        private void Start()
        {
            EventManager.Instance.Trigger("GameStarted");

            EventManager.TryGetInstance()?.Trigger("SafeCall");

            if (EventManager.HasInstance)
                Debug.Log("[Example] EventManager is alive.");
        }
    }
}
