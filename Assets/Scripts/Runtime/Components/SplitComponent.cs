using Assets.Scripts.Runtime.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Components
{
    public struct SplitComponent
    {
        public int splitCount;
        public int spawnedCount;
        public EnemyView splitedView;
        public Vector3 splitPoint;
        public bool isOriginalDestroyed;
    }
}
