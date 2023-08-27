using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FaceType { Regular, Touched, CoolSad, CoolHappy };



public class FaceHandler : MonoBehaviour
{
    public GameObject cheeksGameObject, eyesGameObject, mouthGameObject;

    private EyesHandler eyesHandler;
    private CheeksHandler cheeksHandler;
    private MouthHandler mouthHandler;

    private SpriteRenderer cheeksRenderer, eyesRenderer, mouthRenderer;

    private EyeType _eyes;
    public EyeType eyes { get { return _eyes;}
        set {
            switch(value){
                case EyeType.Open: eyesRenderer.sprite = eyesHandler.open; break;
                case EyeType.Closed: eyesRenderer.sprite = eyesHandler.closed; break;
                case EyeType.Happy: eyesRenderer.sprite = eyesHandler.happy; break;
                case EyeType.Left: eyesRenderer.sprite = eyesHandler.left; break;
                case EyeType.Right: eyesRenderer.sprite = eyesHandler.right; break;
                case EyeType.Satisfied: eyesRenderer.sprite = eyesHandler.satisfied; break;
                case EyeType.Shut: eyesRenderer.sprite = eyesHandler.shut; break;
                case EyeType.Smug: eyesRenderer.sprite = eyesHandler.smug; break;
                default: throw new UnityException("invalid type");
            }
            _eyes = value;
        }
    }
    private MouthType _mouth;
    public MouthType mouth { get { return _mouth;}
        set {
            switch(value){
                case MouthType.Bunny: mouthRenderer.sprite = mouthHandler.bunny; break;
                case MouthType.Flat: mouthRenderer.sprite = mouthHandler.flat; break;
                case MouthType.Frown: mouthRenderer.sprite = mouthHandler.frown; break;
                case MouthType.Pout: mouthRenderer.sprite = mouthHandler.pout; break;
                case MouthType.Smile: mouthRenderer.sprite = mouthHandler.smile; break;
                default: throw new UnityException("invalid type");
            }
            _mouth = value;
        }
    }
    public CheekType _cheeks;
    public CheekType cheeks { get { return _cheeks;}
        set {
            switch(value){
                case CheekType.None: cheeksRenderer.enabled = false; break;
                case CheekType.Plump: cheeksRenderer.enabled = true; cheeksRenderer.sprite = cheeksHandler.plump; break;
                case CheekType.Starved: cheeksRenderer.enabled = true; cheeksRenderer.sprite = cheeksHandler.starved; break;
                default: throw new UnityException("invalid type");
            }
            _cheeks = value;
        }
    }

    private FaceHandlerMouseOverDispatcher mouseOverDispatcher;
    private Collider2D col2D;

    public FaceType _faceType = FaceType.Regular;
    public FaceType faceType { get {return _faceType;}
        set {
            StopBlinking();
            switch(value) {
                case FaceType.Regular: eyes = EyeType.Open; mouth = MouthType.Bunny; StartBlinking(); break;
                case FaceType.Touched: eyes = EyeType.Shut;  mouth = MouthType.Bunny; break;
                case FaceType.CoolSad: eyes = EyeType.Smug; mouth = MouthType.Frown; break;
                case FaceType.CoolHappy: eyes = EyeType.Smug; mouth = MouthType.Smile; break;
                default: throw new UnityException("invalid type");
                
            }
            _faceType = value;
        } }

    void Awake() {
        eyesHandler = eyesGameObject.GetComponent<EyesHandler>();
        cheeksHandler = cheeksGameObject.GetComponent<CheeksHandler>();
        mouthHandler = mouthGameObject.GetComponent<MouthHandler>();
        eyesRenderer = eyesGameObject.GetComponent<SpriteRenderer>();
        cheeksRenderer = cheeksGameObject.GetComponent<SpriteRenderer>();
        mouthRenderer = mouthGameObject.GetComponent<SpriteRenderer>();
        
        faceType = _faceType;
        cheeks = _cheeks;

        col2D = GetComponent<BoxCollider2D>();

        mouseOverDispatcher = new FaceHandlerMouseOverDispatcher(this);
    }

    private IEnumerator blinkCoroutine;

    public void StartBlinking() {
        blinkOpenEyeType = eyes;
        eyes = EyeType.Closed;
        blinkCoroutine = _Blink();
        StartCoroutine(blinkCoroutine);
    }

    public float blinkOpenMin = 0.7f, blinkOpenMax = 1.5f, blinkClosed = 0.05f;
    private EyeType blinkOpenEyeType;

    IEnumerator _Blink() {
        while(true){
            if(eyes == blinkOpenEyeType) {
                eyes = EyeType.Closed;
                yield return new WaitForSeconds(blinkClosed);
                
            }
            else{
                eyes = blinkOpenEyeType;
                yield return new WaitForSeconds(Random.Range(blinkOpenMin, blinkOpenMax));
            }
        }
    } 

    public void StopBlinking() {
        if(blinkCoroutine != null) {
            StopCoroutine(blinkCoroutine);
            eyes = blinkOpenEyeType;
            blinkCoroutine = null;
        }
    }

    void Update() {
        mouseOverDispatcher.Dispatch(col2D);
    }

    float _opacity;
    public float opacity {
        get { return _opacity;}
        set {
            Color color = eyesRenderer.color;
            Color newColor = new Color(color.r, color.b, color.g, value);
            eyesRenderer.color = newColor;
            mouthRenderer.color = newColor;
            cheeksRenderer.color = newColor;
            _opacity = value;
        }
    }

    private Coroutine currentFadeCoroutine;
    public float fadeAmount = 0.05f;
    public float fadeMin = 0.2f;
    private bool _onMouseHoverEnabled = true;
    public bool onMouseHoverEnabled {
        get { return _onMouseHoverEnabled;}
        set { _onMouseHoverEnabled = value;}
    }
    private class FaceHandlerMouseOverDispatcher : CustomMouseOverDispatcher
    {
        private FaceHandler faceHandler;
        public FaceHandlerMouseOverDispatcher(FaceHandler faceHandler) {
            this.faceHandler = faceHandler;
        }
        protected override void OnMouseExit()
        {
            if(faceHandler._onMouseHoverEnabled) faceHandler.StartFadeIn();
        }

        protected override void OnMouseEnter()
        {
            if(faceHandler._onMouseHoverEnabled) faceHandler.StartFadeOut();
        }
    }

    public void StartFadeOut(){
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }
        currentFadeCoroutine = StartCoroutine(_FadeOut());
    }

    public void StartFadeIn(){
        if (currentFadeCoroutine != null)
        {
            StopCoroutine(currentFadeCoroutine);
        }
        currentFadeCoroutine = StartCoroutine(_FadeIn());
    }

    private IEnumerator _FadeIn(){
        while (opacity < 1f)
        {
            opacity += fadeAmount;
            yield return null;
        }
        opacity = 1f;
        currentFadeCoroutine = null;
    }
    private IEnumerator _FadeOut(){
        while (opacity > fadeMin)
        {
            opacity -= fadeAmount;
            yield return null;
        }
        opacity = fadeMin;
        currentFadeCoroutine = null;
    }

}
