using System;
using Shapes2D;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndPoint : MonoBehaviour
{
    public Collider2D collider2d;
    public GameObject player;
    private void Update()
    {
        if (collider2d.OverlapPoint(player.transform.position))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}