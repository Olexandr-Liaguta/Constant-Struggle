using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum LocationCellStatus
{
    Red,
    Yellow,
    Green,
}

[Serializable]
public class LocationCell
{
    public LocationCellStatus status; 
    public bool isHome = false;
    public int score;
}

[CreateAssetMenu(fileName = "GameController", menuName = "States/GameController")]
public class GameController : ScriptableObject
{

    [Serializable]
    public class TextureList
    {
        public LocationCell[] columns = new LocationCell[3];
    }

    public TextureList[] locationRows = new TextureList[3];
}
