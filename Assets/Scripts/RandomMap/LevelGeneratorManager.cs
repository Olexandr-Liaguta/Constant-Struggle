using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneratorManager : MonoBehaviour
{
    static public LevelGeneratorManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public float centerVertexZ;
    public float maxDistanceZ;

}
