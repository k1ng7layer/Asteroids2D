using System;

namespace MyUISystem
{
    public interface INavigator
    {
        public event Action<string> OnNavigate;
    }
}
