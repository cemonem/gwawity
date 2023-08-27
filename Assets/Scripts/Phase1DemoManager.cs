using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Phase1DemoManager : MonoBehaviour
{
    // Start is called before the first frame update

    private bool paused;
    public GameObject player;
    public Collider2D playerCollider2D;
    public GameObject Lv2Spawn;
    public GameObject PausedSign;
    public Rect screenRect;
    public GameObject cameraGameObject;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            paused = !paused;
            Time.timeScale = paused ? 0f : 1f;
            PausedSign.SetActive(paused);

        }
        if(Input.GetKeyDown(KeyCode.R)){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(Input.GetKeyDown(KeyCode.N)){
            player.transform.rotation = Quaternion.identity;
            player.transform.position = Lv2Spawn.transform.position;
            // Vector2 cameraRelative = cameraGameObject.transform.position-player.transform.position;
            // float x = Mathf.Clamp(cameraRelative.x, screenRect.xMin, screenRect.xMax);
            // float y = Mathf.Clamp(cameraRelative.y, screenRect.yMin, screenRect.yMax);
            // cameraGameObject.transform.position = new Vector3(x,y,cameraGameObject.transform.position.z) + player.transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other == playerCollider2D) SceneManager.LoadScene(SceneManager.GetActiveScene().name);      
    }

    void LateUpdate(){
        Vector2 cameraRelative = cameraGameObject.transform.position-player.transform.position;
        float x = Mathf.Clamp(cameraRelative.x, screenRect.xMin, screenRect.xMax);
        float y = Mathf.Clamp(cameraRelative.y, screenRect.yMin, screenRect.yMax);
        cameraGameObject.transform.position = new Vector3(x,y,cameraGameObject.transform.position.z) + player.transform.position;
    }
}
