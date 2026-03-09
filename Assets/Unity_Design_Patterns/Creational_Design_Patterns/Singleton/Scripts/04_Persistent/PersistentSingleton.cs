using UnityEngine;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts
{
    /// <summary>
    /// A MonoBehaviour Singleton that persists across scene transitions via
    /// <see cref="Object.DontDestroyOnLoad"/>.
    ///
    /// <para><b>AutoUnparentOnAwake:</b> When true, the GameObject is detached from its
    /// parent before DontDestroyOnLoad is called. Unity requires the object to be at
    /// the root of the hierarchy for DontDestroyOnLoad to work correctly on nested prefabs.</para>
    ///
    /// <para><b>HasInstance:</b> Returns true without triggering lazy creation. Safe to call
    /// during shutdown or before the instance is initialized.</para>
    ///
    /// <para><b>TryGetInstance:</b> Returns the existing instance or null. Never creates
    /// a new instance. Use this when the caller can tolerate a missing manager.</para>
    ///
    /// <para><b>OnDestroy:</b> Clears the static reference so a future scene load
    /// does not hold a stale destroyed reference.</para>
    ///
    /// Use when: Global managers (GameManager, AudioManager, etc.) that must be
    /// available in every scene throughout the entire application lifetime.
    /// </summary>
    public class PersistentSingleton : MonoBehaviour
    {
        public bool AutoUnparentOnAwake = true;

        private static PersistentSingleton _instance;

        public static bool HasInstance => _instance != null;
        public static PersistentSingleton TryGetInstance() => HasInstance ? _instance : null;
        public static PersistentSingleton Instance => _instance;

        protected virtual void Awake()
        {
            if (AutoUnparentOnAwake)
                transform.SetParent(null);

            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}
