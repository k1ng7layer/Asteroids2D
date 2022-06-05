using Assets.Interfaces;
using Assets.Scripts.Runtime.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Components
{
    public struct EnemyComponent
    {
        public GameObject attachedGameObject;
        public EnemyView attachedView;
        public bool alive;
        public string enemyType;
        public bool isSplited;
    }
}
