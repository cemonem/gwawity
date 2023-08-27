using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetWhenOutofBounds : MonoBehaviour
{
    public Collider2D Collider2D;
    public GameObject player;
    private void Update()
    {
        if (!Collider2D.OverlapPoint(player.transform.position))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}