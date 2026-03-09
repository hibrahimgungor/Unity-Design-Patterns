using UnityEngine;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// Demonstrates that <see cref="PersistentGameManager"/> survives scene transitions
    /// and remains the same instance across scenes.
    /// Attach to any GameObject in the scene to run the example.
    /// </summary>
    public class GenericPersistentSingletonExample : MonoBehaviour
    {
        private void Start()
        {
            PersistentGameManager.Instance.AddScore(100);

            if (PersistentGameManager.HasInstance)
                Debug.Log($"[Example] Score across scenes: {PersistentGameManager.Instance.Score}");

            PersistentGameManager.TryGetInstance()?.AddScore(50);
        }
    }
}
