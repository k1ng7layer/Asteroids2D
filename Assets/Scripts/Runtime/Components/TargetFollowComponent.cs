﻿using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Components
{
    public struct TargetFollowComponent
    {
        public TransformComponent targetTransform;
        public EntityObject targetEntity;
    }
}
