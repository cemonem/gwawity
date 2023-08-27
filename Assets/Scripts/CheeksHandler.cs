using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CheekType { None, Plump, Starved }

public class CheeksHandler : MonoBehaviour
{
    public Sprite plump, starved;
    private SpriteRenderer cheeksRenderer;
    void Start() {
        cheeksRenderer = GetComponent<SpriteRenderer>();
    }
    private CheekType _cheeks;
    public CheekType cheeks { get { return _cheeks;}
        set {
            switch(value){
                case CheekType.None: cheeksRenderer.enabled = false; break;
                case CheekType.Plump: cheeksRenderer.enabled = true; cheeksRenderer.sprite = plump; break;
                case CheekType.Starved: cheeksRenderer.enabled = true; cheeksRenderer.sprite = starved; break;
                default: throw new UnityException("invalid type");
            }
            _cheeks = value;
        }
    }
}
