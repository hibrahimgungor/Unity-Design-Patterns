using UnityEngine;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts
{
    /// <summary>
    /// A Singleton that extends MonoBehaviour, integrating with Unity's component system.
    /// On Awake, if another instance already exists the incoming duplicate is immediately
    /// destroyed, guaranteeing only one instance is active at a time.
    ///
    /// Limitations:
    /// - Does not survive scene transitions. The instance is destroyed when its scene unloads.
    /// - Must be placed on a GameObject in the scene manually.
    ///
    /// Use when: Single-scene projects or managers that are only needed within one scene.
    /// </summary>
    public class MonoBehaviourSingleton : MonoBehaviour
    {
        private static MonoBehaviourSingleton _instance;

        public static MonoBehaviourSingleton Instance => _instance;

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}
