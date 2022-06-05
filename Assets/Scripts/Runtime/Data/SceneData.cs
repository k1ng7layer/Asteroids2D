using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Data
{
    public class SceneData
    {
        public SceneData(Camera mainCamera)
        {
            MainCamera = mainCamera;
        }
        public Camera MainCamera { get; private set; }
    }
}
