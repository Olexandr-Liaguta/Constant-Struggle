using System;
using System.Collections;
using System.Collections.Generic;


[Serializable]
public static class GameMapData
{
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

    [Serializable]
    public class TextureList
    {
        public List<LocationCell> columns = new();
    }

    public static List<TextureList> locationRows = new()
    {
        new TextureList {
            columns = new List < LocationCell >
            {
                new LocationCell {isHome = false, score = 50, status= LocationCellStatus.Yellow},
                new LocationCell {isHome = false, score = 75, status= LocationCellStatus.Red},
                new LocationCell {isHome = false, score = 100, status= LocationCellStatus.Red},
            }
        },
        new TextureList {
            columns = new List < LocationCell >
            {
                new LocationCell {isHome = false, score = 25, status= LocationCellStatus.Green},
                new LocationCell {isHome = false, score = 50, status= LocationCellStatus.Yellow},
                new LocationCell {isHome = false, score = 75, status= LocationCellStatus.Red},
            }
        },
        new TextureList {
            columns = new List < LocationCell >
            {
                new LocationCell {isHome = true},
                new LocationCell {isHome = false, score = 25, status= LocationCellStatus.Green},
                new LocationCell {isHome = false, score = 50, status= LocationCellStatus.Yellow},
            }
        },
    };

    public static void SetLocationRows(List<TextureList> rows)
    {
        locationRows = rows;
    }
}
