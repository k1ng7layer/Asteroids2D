using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyECS2
{
    public interface IGameSystem
    {
        void Initialize();
        void Update();
        void OnDestroy();
    }
}
