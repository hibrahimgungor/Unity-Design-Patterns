using UnityEngine;
using Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// Demonstrates that <see cref="MonoBehaviourSingleton"/> destroys duplicate instances
    /// when multiple GameObjects carry the same component.
    /// Add this script and two GameObjects with MonoBehaviourSingleton to verify.
    /// </summary>
    public class MonoBehaviourSingletonExample : MonoBehaviour
    {
        private void Start()
        {
            if (MonoBehaviourSingleton.Instance != null)
                Debug.Log($"Active instance: {MonoBehaviourSingleton.Instance.gameObject.name}");
        }
    }
}
