using Assets.Scripts.Runtime.Processing;
using MyUISystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Runtime.UI
{
    public class StartGameButton : UIButton
    {
        protected override void OnClick()
        {
            Game.LoadScene(1);
        }
    }
}
