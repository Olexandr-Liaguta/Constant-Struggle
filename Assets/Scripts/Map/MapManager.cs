using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    void Start()
    {
        SaveManager.Instance.Load();
    }

}
