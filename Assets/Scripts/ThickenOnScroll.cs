using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThickenOnScroll : MonoBehaviour
{
    public float minSize = 0.04f;
    public float maxSize = 1.6f;
    public float sizeStep = 0.01f;
    public float initialSize = 0.08f;
    private Rigidbody2D rb;
    private Shapes2D.Shape shape;
    private float currentSize = 0.08f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shape = GetComponent<Shapes2D.Shape>();
    }

    void Update()
    {
    }

    void OnMouseOver( ){
        Debug.Log("hello");
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            // Modify the size and mass based on the scroll input
            currentSize = Mathf.Clamp(currentSize + scroll * sizeStep, minSize, maxSize);
            Debug.Log(currentSize);
            // Update the object's size and mass
            shape.settings.outlineSize = currentSize;
            rb.mass = currentSize;
            rb.WakeUp();
        }
    }
}
