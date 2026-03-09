using UnityEngine;
using UnityEngine.SceneManagement;
using Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// Demonstrates that <see cref="PersistentSingleton"/> survives scene transitions
    /// and that duplicate instances created in subsequent scenes are destroyed.
    /// Attach to a GameObject in the first scene and verify after loading a second scene.
    /// </summary>
    public class PersistentSingletonExample : MonoBehaviour
    {
        private void Start()
        {
            if (PersistentSingleton.HasInstance)
                Debug.Log($"Instance alive: {PersistentSingleton.Instance.gameObject.name}");

            var safeRef = PersistentSingleton.TryGetInstance();
            if (safeRef != null)
                Debug.Log("TryGetInstance returned a valid reference.");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
