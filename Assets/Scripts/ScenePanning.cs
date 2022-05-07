using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePanning : MonoBehaviour
{
    private Camera mainCamera;

    [Header("Camera")]
    public float panAmount = 500f;

    [Header("Scene Boundary")]
    public float SceneWidth = 1080f;
    public float SceneHeight = 1920f;


    private void Awake()
    {
        mainCamera = GameObject.FindObjectOfType<Camera>();
        if(mainCamera == null)
        {
            Debug.Log("main camera not found");
        }
    }

    private void Start()
    {
        
    }

    public void PanLeft()
    {
        if((mainCamera.transform.position.x) > -SceneWidth)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x - panAmount, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
        
    }

    // Update is called once per frame
    public void PanRight()
    {
        if ((mainCamera.transform.position.x ) < SceneWidth)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x + panAmount, mainCamera.transform.position.y, mainCamera.transform.position.z);
        }
    }

    public void PanUp()
    {
        if ((mainCamera.transform.position.y) < SceneHeight)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y + panAmount, mainCamera.transform.position.z);
        }
    }

    public void PanDown()
    {
        if ((mainCamera.transform.position.y) > -SceneHeight)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y - panAmount, mainCamera.transform.position.z);
        }
    }
}