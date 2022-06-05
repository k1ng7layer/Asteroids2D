using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Components
{
    public struct EnemySpawnerComponent
    {
        public string enemyPooledType;
        public Vector3 normalScale;
        public int countToSpawn;
        public bool hasTarget;
    }
}
