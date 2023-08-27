using System;
using Cyclone.Rigid;
using UnityEngine;

public class CycloneComponent : MonoBehaviour
{
    public RigidBodyEngine engine;
    public GameManager manager;
    protected virtual void Awake()
    {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.cycloneComponents.Add(this);
        engine = manager.engine;
    }

    public virtual void CycloneUpdate()
    {
    }
}