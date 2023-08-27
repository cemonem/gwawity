using System;
using System.Collections.Generic;
using Cyclone.Core;
using UnityEngine;

public class CycloneAttractorUwU : CycloneComponent
{
    public List<CycloneUwU> attracted;
    public Collider2D attractionField;
    public Collider2D clickField;
    public bool attractionActive = true;
    public FaceHandler faceHandler;
    public double attraction_acc = 19.6;
    public AttractorClickDispatcher dispatcher;
    private void Start()
    {
        dispatcher = new AttractorClickDispatcher(this);
    }
    public override void CycloneUpdate()
    {
        base.CycloneUpdate();
        if (attractionActive)
        {
            foreach (var cycloneUwU in attracted)
            {
                if (attractionField.OverlapPoint(cycloneUwU.gameObject.transform.position))
                {
                    cycloneUwU.body.AddForceAtBodyPoint(attraction_acc * cycloneUwU.visibleMass * Vector3d.UnitY, cycloneUwU.centerOfMass);
                }
            }
        }
    }

    public void Update()
    {
        dispatcher.Dispatch(clickField);
    }

    public class AttractorClickDispatcher : CustomMouseOverDispatcher
    {
        private CycloneAttractorUwU attractorUwU;

        public AttractorClickDispatcher(CycloneAttractorUwU attractorUwU)
        {
            this.attractorUwU = attractorUwU;
        }
        protected override void OnMouseUpAsButton()
        {
            attractorUwU.attractionActive = !attractorUwU.attractionActive;
            if (attractorUwU.attractionActive)
            {
                attractorUwU.faceHandler.faceType = FaceType.CoolHappy;
            }
            else attractorUwU.faceHandler.faceType = FaceType.CoolSad;
        }
    }
}