using Assets.Interfaces;
using Assets.Scripts.Runtime.Data;
using MyECS2;
using MyExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Runtime.Views
{
    public class LaserGunView : BulletView, IWeaponView
    {
        [SerializeField] private Transform _laserEndPosition;
        [SerializeField] private Vector3 _laserStartPosition;
        [SerializeField] private LaserGunTrigger _trigger;
        private LineRenderer _lineRenderer;
        private IDestroyableEntity _destroyableEntity;
        public void DisplayMaxAmmoValue(float value)
        {

        }

        public void InitializeView()
        {
            _lineRenderer = this.GetOrCreateComponent<LineRenderer>();
            _trigger = this.transform.parent.GetComponentInChildren<LaserGunTrigger>();
            _destroyableEntity = new EmptyEntity();
            _trigger.SetEntity(ref _destroyableEntity);
           
        }

        public void UpdateAmmoInfo(float value)
        {

        }

        public void Vizualize()
        {
            _laserStartPosition = this.transform.position;
            _lineRenderer.SetPosition(0, _laserStartPosition);
            _lineRenderer.SetPosition(1, _laserEndPosition.position);
            _trigger.Enable();
        }

        public void DisableVizualization()
        {
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, Vector3.zero);
            _trigger.Disable();
        }
       
    }
}

