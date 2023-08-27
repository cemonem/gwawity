using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColliderMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver() {
        Debug.Log(System.String.Format("Mouse Entered to {0}",gameObject.name));
    }
}
