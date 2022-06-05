using MyECS2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Interfaces
{
    public interface ITransformableView
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }
        public Vector3 Forward => Rotation * Vector3.up;
        public Vector3 Scale { get; set; }
        public void SetPosition(ref Vector3 position);
        public void SetRotation(Quaternion rotation);
        public void SetScale(Vector3 scale);
        public void SetEntity(ref EntityObject entity);
        public void OnViewDestroy();
    }
}
