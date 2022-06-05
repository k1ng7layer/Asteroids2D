namespace MyUISystem
{
    public interface IIndicator<T>
    {
        void SetInintialValue(T Value);
        void SetValue(T value);
    }
}
