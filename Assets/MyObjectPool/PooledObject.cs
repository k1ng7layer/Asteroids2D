using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MyObjectPool
{
    public abstract class PooledObject : MonoBehaviour
    {
        [HideInInspector] public int id;
        public abstract void OnCreated();
    }
}
