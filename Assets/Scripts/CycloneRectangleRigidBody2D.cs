using System;
using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Collisions;
using UnityEngine;
using UnityEngine.UIElements;


public class CycloneRectangleRigidBody2D : CycloneComponent
{
    public float thickness = 5f;
    private CollisionBox collisionBox;
    public BoxCollider2D boxCollider2D;
    public double inverseMass = 0;
    public double inverseInertia = 0;
    public double damping = 0.9;
    public RigidBody body;

    protected override void Awake()
    {
        base.Awake();
        boxCollider2D.isTrigger = true;
        
        var pos = transform.position.ToVector3d();
        var scale = transform.localScale.ToVector3d() * 0.5;
        var rot = transform.rotation.ToQuaternion();

        pos.z = 0;
        scale.z = thickness * 0.5;
        scale.x *= boxCollider2D.size.x;
        scale.y *= boxCollider2D.size.y;

        body = new RigidBody();
        body.InverseInertiaTensor = new Matrix3(Vector3d.Zero, Vector3d.Zero, new Vector3d(0, 0, inverseInertia));
        body.Position = pos;
        body.Orientation = rot;
        body.LinearDamping = damping;
        body.AngularDamping = damping;
        body.InverseMass = inverseMass;
        body.SetAwake(true);
        body.SetCanSleep(true);

        collisionBox = new CollisionBox(scale);
        collisionBox.Body = body;
        
        engine.Bodies.Add(body);
        engine.Collisions.Primatives.Add(collisionBox);

    }

    private void Update()
    {
        transform.position = body.Position.ToVector3();
        transform.rotation = body.Orientation.ToQuaternion();
    }
}
