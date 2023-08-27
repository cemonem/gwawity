using Cyclone.Core;
using UnityEngine;


public abstract class CycloneUwU : CycloneComponent
{
    public Vector3d centerOfMass;
    public Cyclone.Rigid.RigidBody body;
    public double visibleMass;
    public override void CycloneUpdate()
    {
        base.CycloneUpdate();
        body.AddForceAtBodyPoint(Vector3d.UnitY*-9.81*visibleMass, centerOfMass);
    }
    
    public virtual void Update()
    {
        transform.position = body.Position.ToVector3();
        transform.rotation = body.Orientation.ToQuaternion();
    }

}