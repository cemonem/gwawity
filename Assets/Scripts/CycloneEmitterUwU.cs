using System;
using Cyclone.Core;
using UnityEngine;


public class CycloneEmitterUwU : MonoBehaviour
{
    public Transform spawnPoint;
    private GameObject spawnedGameObject = null;
    public GameObject sgameObject;
    public double velocity;
    public double mass;
    public Collider2D clickField;
    private ClickHandler clickHandler;

    void Start()
    {
        clickHandler = new ClickHandler(this);
    }

    private void Update()
    {
        clickHandler.Dispatch(clickField);
    }

    public void Emit()
    {
        if (spawnedGameObject)
        {
            Destroy(spawnedGameObject);
        }


        spawnedGameObject = Instantiate(sgameObject, spawnPoint.position, spawnPoint.rotation);
        CycloneCircleRigidBody2D rigidBody2D = spawnedGameObject.GetComponent<CycloneCircleRigidBody2D>();
        rigidBody2D.body.Velocity = Vector3d.UnitX*velocity;
        rigidBody2D.body.SetMass(mass);
    }

    public class ClickHandler : CustomMouseOverDispatcher
    {
        private CycloneEmitterUwU ceu;
        public ClickHandler(CycloneEmitterUwU ceu)
        {
            this.ceu = ceu;
        }

        protected override void OnMouseUpAsButton()
        {
            base.OnMouseUpAsButton();
            ceu.Emit();
        }
    }
}