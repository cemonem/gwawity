using System;
using Cyclone.Core;
using UnityEngine;

namespace DefaultNamespace
{
    public class CycloneCircleAddForce : CycloneComponent
    {
        public CycloneCircleRigidBody2D circleRigidBody;

        public override void CycloneUpdate()
        {
            base.CycloneUpdate();
            circleRigidBody.body.AddForce(-9.81*circleRigidBody.body.GetMass()*Vector3d.UnitY);
        }
    }
}