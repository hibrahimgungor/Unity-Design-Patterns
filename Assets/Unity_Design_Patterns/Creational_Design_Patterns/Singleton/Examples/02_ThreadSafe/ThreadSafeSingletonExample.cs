using System.Threading;
using UnityEngine;
using Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts;

namespace Unity_Design_Patterns.Creational_Design_Patterns.Singleton.Scripts.Examples
{
    /// <summary>
    /// Demonstrates that <see cref="ThreadSafeSingleton"/> produces the same instance
    /// even when accessed concurrently from multiple threads.
    /// Attach this component to any GameObject in the scene to run the example.
    /// </summary>
    public class ThreadSafeSingletonExample : MonoBehaviour
    {
        private void Start()
        {
            ThreadSafeSingleton capturedFromThreadA = null;
            ThreadSafeSingleton capturedFromThreadB = null;

            var threadA = new Thread(() => capturedFromThreadA = ThreadSafeSingleton.Instance);
            var threadB = new Thread(() => capturedFromThreadB = ThreadSafeSingleton.Instance);

            threadA.Start();
            threadB.Start();
            threadA.Join();
            threadB.Join();

            Debug.Log($"Same instance across threads: {ReferenceEquals(capturedFromThreadA, capturedFromThreadB)}");
        }
    }
}
