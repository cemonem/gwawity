using System;
using Cyclone.Core;
using UnityEngine;

namespace DefaultNamespace
{
    public class CycloneRectangleAddForce : CycloneComponent
    {
        public CycloneRectangleRigidBody2D rectangleRigidBody;

        public override void CycloneUpdate()
        {
            base.CycloneUpdate();
            rectangleRigidBody.body.AddForce(-9.81*rectangleRigidBody.body.GetMass()*Vector3d.UnitY);
        }
    }
}