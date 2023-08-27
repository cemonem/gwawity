using System;
using System.Collections.Generic;
using Cyclone.Core;
using Cyclone.Rigid;
using Cyclone.Rigid.Constraints;
using UnityEngine;


public class CycloneSpring : CycloneComponent
{
    public CycloneUwU cycloneUwU;
    public double stiffness;

    private void Start()
    {
        engine.Constraints.Add(new XFixedConstraint(cycloneUwU.body));
    }

    public override void CycloneUpdate()
    {
        base.CycloneUpdate();
        cycloneUwU.body.AddForce(stiffness*(gameObject.transform.position.ToVector3d()-cycloneUwU.body.Position));
    }

    public class XFixedConstraint : RigidConstraint
    {
        public Cyclone.Rigid.RigidBody body;
        public double x;
        public double epsilon = 0.0001;
        public XFixedConstraint(Cyclone.Rigid.RigidBody body)
        {
            this.body = body;
            x = body.Position.x;
        }
        
        public override int AddContact(IList<RigidBody> bodies, IList<RigidContact> contacts, int next)
        {
            if (Math.Abs(x-body.Position.x) > epsilon)
            {
                var contact = contacts[next];
                contact.Body[0] = body;
                contact.Body[1] = null;
                contact.ContactNormal = Math.Sign(x-body.Position.x)*Vector3d.UnitX;
                contact.ContactPoint = body.Position;
                contact.Penetration = Math.Abs(x - body.Position.x);
                contact.Friction = 1.0;
                contact.Restitution = 0;
                return 1;
            }

            return 0;
        }
    }
}