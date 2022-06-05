using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Interfaces
{
    public interface IWeaponView
    {
        void InitializeView();
        void Vizualize();
        void UpdateAmmoInfo(float value);
        void DisplayMaxAmmoValue(float value);
        void DisableVizualization();
    }
}
