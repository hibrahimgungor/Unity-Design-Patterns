using UnityEngine;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts
{
    /// <summary>
    /// A reusable generic MonoBehaviour Singleton base class that persists across
    /// scene transitions via <see cref="Object.DontDestroyOnLoad"/>.
    /// Extends <see cref="GenericSingleton{T}"/> with persistence and the
    /// AutoUnparentOnAwake safeguard.
    ///
    /// <para><b>AutoUnparentOnAwake:</b> Detaches the GameObject from its parent before
    /// calling DontDestroyOnLoad. Unity requires the object to be at the root of the
    /// hierarchy for DontDestroyOnLoad to work correctly on nested prefabs.</para>
    ///
    /// Use when: Managers that must be available in every scene throughout the entire
    /// application lifetime (e.g. GameManager, AudioManager, SaveManager).
    /// For scene-scoped managers, use <see cref="GenericSingleton{T}"/> instead.
    /// Always call base.Awake() when overriding Awake in a subclass.
    /// </summary>
    public abstract class GenericPersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public bool AutoUnparentOnAwake = true;

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
            if (AutoUnparentOnAwake)
                transform.SetParent(null);

            if (_instance != null && _instance != this as T)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this as T)
                _instance = null;
        }
    }
}
