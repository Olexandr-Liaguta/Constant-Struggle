using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField]
    private CinemachineFreeLook camera;


    public void StackCameraAndShowCursor()
    {
        camera.enabled = false;
        Cursor.visible = true;
    }
    
    public void UnstackCameraAndHideCursor()
    {
        camera.enabled = true;
        Cursor.visible = false;
    }
}
