using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Runtime.Data
{
    public class PlayerInputData
    {
        public Controlls InputActions { get; set; }
        public PlayerInputData(Controlls inputActions)
        {
            InputActions = inputActions;
        }
    }
}
