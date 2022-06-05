using Assets.Interfaces;
using Assets.Scripts.Runtime.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Runtime.Factories
{
    public abstract class ViewFactory
    {
        public abstract PlayerView SpawnedPlayerObject { get; protected set; }
        public abstract ITransformableView GetTransformableView();
    }
}
