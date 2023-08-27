using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class COMHandler : MonoBehaviour
{
    public GameObject face;
    public CycloneUwU cycloneUwU;
    private FaceHandler faceHandler;
    private Collider2D faceCollider;
    public Collider2D boundsCollider;
    private Collider2D col2D;
    private COMHandlerMouseOverDispatcher mouseOverDispatcher;
    // Start is called before the first frame update
    void Start()
    {
        faceHandler = face.GetComponent<FaceHandler>();
        faceCollider = face.GetComponent<BoxCollider2D>();

        col2D = GetComponent<Collider2D>();
        mouseOverDispatcher = new COMHandlerMouseOverDispatcher(this);
        
        cycloneUwU.centerOfMass = cycloneUwU.gameObject.transform.InverseTransformPoint(transform.position).ToVector3d();
        cycloneUwU.body.SetAwake();
    }

    // Update is called once per frame
    private bool overlappingFace;
    void Update()
    {
        mouseOverDispatcher.Dispatch(col2D);
        if(faceCollider.OverlapPoint(transform.position)){
            if(!overlappingFace){
                overlappingFace = true;
                faceHandler.onMouseHoverEnabled = false;
                faceHandler.StartFadeOut();
            }
        }
        else{
            if(overlappingFace){
                overlappingFace = false;
                faceHandler.onMouseHoverEnabled = true;
                faceHandler.StartFadeIn();
            }
        }
    }

    private Vector3 offset;
    private class COMHandlerMouseOverDispatcher : CustomMouseOverDispatcher {
        private  COMHandler comHandler;
        public COMHandlerMouseOverDispatcher(COMHandler comHandler) {
            this.comHandler = comHandler;
        }

        protected override void OnMouseDown()
        {
            comHandler.faceHandler.faceType = FaceType.Touched;
            comHandler.offset = comHandler.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }

        protected override void OnMouseDrag()
        {
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Vector3 clampedPosition = comHandler.boundsCollider.ClosestPoint(cursorPosition+comHandler.offset);
            comHandler.transform.position = clampedPosition;
            comHandler.cycloneUwU.centerOfMass = comHandler.cycloneUwU.gameObject.transform.InverseTransformPoint(clampedPosition).ToVector3d();
            comHandler.cycloneUwU.body.SetAwake();
        }

        protected override void OnMouseUp()
        {
            comHandler.faceHandler.faceType = FaceType.Regular;
        }
    }
}
