namespace Unity_Design_Patterns.Behavioral_Design_Patterns.Observer.Example_01_ClassicObserverPattern
{
    /// <summary>
    /// Defines the contract for any object that wants to be notified
    /// when a change occurs in a subject it is observing.
    ///
    /// This is the core receiver interface in the GoF Observer pattern.
    /// Any class that implements this interface can be registered with
    /// an <see cref="ISubject"/> and will receive notifications via
    /// <see cref="OnNotify"/> when the subject decides to broadcast.
    ///
    /// Limitations:
    /// - <see cref="OnNotify"/> carries no data. The observer receives
    ///   no information about what changed or why.
    /// - A single <see cref="OnNotify"/> method cannot distinguish between
    ///   different event types from the same subject. If a subject publishes
    ///   multiple events, all arrive through the same entry point.
    ///
    /// Pull model:
    /// Rather than pushing data through the interface, the subject passes itself
    /// as an ISubject reference. The observer casts to the concrete type and pulls
    /// whatever data it needs. This keeps the interface generic — IObserver is not
    /// tied to any specific data type — but introduces a cast at the observer side.
    /// </summary>
    public interface IObserver
    {
        void OnNotify(ISubject subject);
    }
}
