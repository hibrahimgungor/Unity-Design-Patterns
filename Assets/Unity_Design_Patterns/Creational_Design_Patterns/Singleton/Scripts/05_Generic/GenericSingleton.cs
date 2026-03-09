using UnityEngine;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts
{
    /// <summary>
    /// A reusable generic MonoBehaviour Singleton base class. Any manager class can
    /// become a Singleton simply by inheriting from this type, eliminating boilerplate.
    /// The instance does not persist across scene transitions; it is tied to the scene
    /// it lives in and destroyed when that scene unloads.
    ///
    /// <para><b>HasInstance / TryGetInstance:</b> Allow callers to check existence or
    /// retrieve the instance without triggering lazy creation or throwing exceptions.</para>
    ///
    /// <para><b>OnDestroy:</b> Nullifies the static reference so destroyed instances
    /// are not returned on subsequent access.</para>
    ///
    /// Use when: Managers that are only needed within a single scene.
    /// For managers that must survive scene transitions, use <see cref="GenericPersistentSingleton{T}"/> instead.
    /// Always call base.Awake() when overriding Awake in a subclass.
    /// </summary>
    public abstract class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static bool HasInstance => _instance != null;
        public static T TryGetInstance() => HasInstance ? _instance : null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError($"[{typeof(T).Name}] No instance found in the scene.");

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance != null && _instance != this as T)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this as T)
                _instance = null;
        }
    }
}
