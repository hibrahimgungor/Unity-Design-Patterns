namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts
{
    /// <summary>
    /// A thread-safe Singleton using the double-checked locking pattern.
    /// The outer null check avoids unnecessary locking on every access.
    /// The inner null check inside the lock prevents a second thread from
    /// creating a new instance after the first thread has already done so.
    ///
    /// The readonly lock object ensures the lock target can never be reassigned.
    ///
    /// Use when: Pure C# systems, background threads, or any context where
    /// multiple threads may access the instance simultaneously.
    /// Not required for main-thread-only Unity MonoBehaviour code.
    /// </summary>
    public class ThreadSafeSingleton
    {
        private static ThreadSafeSingleton _instance;
        private static readonly object _lock = new object();

        private ThreadSafeSingleton() { }

        public static ThreadSafeSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                            _instance = new ThreadSafeSingleton();
                    }
                }
                return _instance;
            }
        }
    }
}
