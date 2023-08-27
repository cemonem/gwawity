using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThicknessHandler : MonoBehaviour
{
    private Shapes2D.Shape shape;
    public GameObject face;
    private FaceHandler faceHandler;
    public CycloneUwU cycloneUwU;
    public float thicknessMin = 0.04f, massInitial = 1f, thicknessMax = 0.16f, massMin = 0.5f, massMax = 4f;
    public float scrollAmount = 4f, massStarved = 0.5f, massPlump = 3f;

    private ThicknessHandlerMouseOverDispatcher mouseOverDispatcher;
    public Collider2D col2D;

    private float currentMass;
    private float currentThickness {
        get { return thicknessMin + ((currentMass-massMin)/(massMax-massMin))*(thicknessMax-thicknessMin); }
    }

    void SetCheeksForCurrentMass(){
        if(currentMass < massStarved){
            faceHandler.cheeks = CheekType.Starved;
        }
        else if(currentMass < massPlump){
            faceHandler.cheeks = CheekType.None;
        }
        else faceHandler.cheeks = CheekType.Plump;
    }

    void Start()
    {
        shape = GetComponent<Shapes2D.Shape>();
        faceHandler = face.GetComponent<FaceHandler>();

        currentMass = massInitial;
        cycloneUwU.visibleMass = currentMass;
        SetCheeksForCurrentMass();
        shape.settings.outlineSize = currentThickness;
        mouseOverDispatcher = new ThicknessHandlerMouseOverDispatcher(this);
    }

    // Update is called once per frame
    void Update()
    {
        mouseOverDispatcher.Dispatch(col2D);
    }

    private class ThicknessHandlerMouseOverDispatcher : CustomMouseOverDispatcher {
        private ThicknessHandler th;
        public ThicknessHandlerMouseOverDispatcher(ThicknessHandler thicknessHandler) {
            this.th = thicknessHandler;
        }

        protected override void OnMouseOver()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                // Modify the size and mass based on the scroll input
                float currentPercent = (th.currentMass-th.massMin)/(th.massMax-th.massMin);
                float nextPercent = Mathf.Clamp(currentPercent + scroll * th.scrollAmount, 0f, 1f);
                th.currentMass = nextPercent*(th.massMax-th.massMin)+th.massMin;
                // Update the object's size and mass
                th.shape.settings.outlineSize = th.currentThickness;
                th.SetCheeksForCurrentMass();
                th.cycloneUwU.visibleMass = th.currentMass;
                th.cycloneUwU.body.SetAwake(true);
            }
        }
    }
}
