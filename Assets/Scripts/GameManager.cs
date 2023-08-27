using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyclone;
using Cyclone.Rigid;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public int iterations = 0;
    public int maxContacts = 100;
    public double epsilon = 0.01;

    public RigidBodyEngine engine;
    public List<CycloneComponent> cycloneComponents;
    public bool paused = false;
    public GameObject pauseSign;

    private void Awake()
    {
        engine = new RigidBodyEngine(maxContacts);
        engine.Resolver.PositionIterations = iterations;
        engine.Resolver.VelocityIterations = iterations;
        engine.Resolver.PositionEpsilon = epsilon;
        engine.Resolver.VelocityEpsilon = epsilon;
        engine.Collisions.Restitution = 0;
        engine.Collisions.Friction = 0.5;
        cycloneComponents = new List<CycloneComponent>();
        
        pauseSign.SetActive(paused);
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            double dt = Time.fixedDeltaTime;

            engine.StartFrame();
            foreach (CycloneComponent com in cycloneComponents)
            {
                com.CycloneUpdate();
            }

            engine.RunPhysics(dt);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            paused = !paused;
            pauseSign.SetActive(paused);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
