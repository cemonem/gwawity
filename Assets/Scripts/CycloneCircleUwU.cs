using System;
using System.Collections.Generic;
using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Collisions;
using Cyclone.Rigid.Constraints;
using UnityEngine;
using UnityEngine.UIElements;


public class CycloneCircleUwU : CycloneUwU
{
    private CollisionSphere collisionSphere;
    public CircleCollider2D circleCollider2D;
    public double inverseMass = 1;
    public double inverseInertia = 1;
    public double damping = 0.9;

    protected override void Awake()
    {
        base.Awake();
        circleCollider2D.isTrigger = true;
        
        var pos = transform.position.ToVector3d();
        var rot = transform.rotation.ToQuaternion();

        pos.z = 0;
        var radius = circleCollider2D.gameObject.transform.localScale.y*circleCollider2D.radius;

        body = new RigidBody();
        body.InverseInertiaTensor = new Matrix3(Vector3d.Zero, Vector3d.Zero, new Vector3d(0, 0, inverseInertia));
        body.Position = pos;
        body.Orientation = rot;
        body.LinearDamping = damping;
        body.AngularDamping = damping;
        body.InverseMass = inverseMass;
        body.SetAwake(true);
        body.SetCanSleep(true);

        collisionSphere = new CollisionSphere(radius);
        collisionSphere.Body = body;
        
        engine.Bodies.Add(body);
        engine.Collisions.Primatives.Add(collisionSphere);

    }
}
