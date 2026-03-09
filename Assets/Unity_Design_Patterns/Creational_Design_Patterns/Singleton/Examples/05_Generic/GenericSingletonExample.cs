using UnityEngine;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// Demonstrates usage of <see cref="GameManager"/> and <see cref="AudioManager"/>,
    /// both of which inherit from <see cref="GenericSingleton{T}"/>.
    /// Attach to any GameObject in the scene to run the example.
    /// </summary>
    public class GenericSingletonExample : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.AddScore(100);
            AudioManager.Instance.SetVolume(0.75f);

            if (GameManager.HasInstance)
                Debug.Log($"[Example] GameManager score: {GameManager.Instance.Score}");

            AudioManager.TryGetInstance()?.SetVolume(0.5f);
        }
    }
}
