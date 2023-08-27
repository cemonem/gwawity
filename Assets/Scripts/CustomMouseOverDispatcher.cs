using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class CustomMouseOverDispatcher {
    private bool isMouseOver = false;
    private bool isMouseDown = false;
    
    protected virtual void OnMouseEnter(){}
    protected virtual void OnMouseOver(){}
    protected virtual void OnMouseExit(){}
    protected virtual void OnMouseDown(){}
    protected virtual void OnMouseUp(){}
    protected virtual void OnMouseDrag(){}
    protected virtual void OnMouseUpAsButton(){}
    
    
    public void Dispatch(Collider2D collider)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);
        
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == collider)
            {
                if (!isMouseOver)
                {
                    isMouseOver = true;
                    OnMouseEnter();
                }
                else
                {
                    OnMouseOver();
                }
                
                if (Input.GetMouseButton(0))
                {
                    if(!isMouseDown)
                    {
                        isMouseDown = true;
                        OnMouseDown();
                    }
                    else {
                        OnMouseDrag();
                    }
                }
                else
                {
                    if(isMouseDown)
                    {
                        isMouseDown = false;
                        OnMouseUp();
                        OnMouseUpAsButton();
                    }
                    
                }
                return;
            }

    
        }

        if (isMouseDown)
        {
            if(Input.GetMouseButton(0))
            {
                OnMouseDrag();
            }
            else
            {
                isMouseDown = false;
                OnMouseUp();                
            }
        }

        if (isMouseOver)
        {
            isMouseOver = false;
            OnMouseExit();
        }
    }
}