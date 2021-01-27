namespace GameEventSystem
{
    public interface IEventListener<T>
    {
        T Event { get; set; }
        void OnEventRaised();
    }
}