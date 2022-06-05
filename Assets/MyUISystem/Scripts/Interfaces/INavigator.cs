using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyUISystem
{
    public interface INavigator
    {
        public event Action<string> OnNavigate;
    }
}
