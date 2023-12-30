using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    static public GameManager Instance { get; private set; }


    [SerializeField] private CinemachineFreeLook camera;

    private bool isFirstUpdate = true;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
            SaveManager.Instance.Load();
        }
    }


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
