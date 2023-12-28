using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapCell : MonoBehaviour
{
    enum Columns
    {
        One = 1, Two = 2, Three = 3
    }

    [SerializeField] private Columns row;
    [SerializeField] private Columns column;

    [SerializeField] private Button button;
    [SerializeField] private GameController gameController;

    private LocationCell currentLocation;

    private void Start()
    {
        currentLocation = gameController.locationRows[((int)row - 1)].columns[(int)column - 1];

        if (currentLocation.isHome) return;

        UpdateButtonColor();
    }

    private void UpdateButtonColor()
    {
        LocationCellStatus status = currentLocation.status;

        if (status == LocationCellStatus.Red)
        {
            this.SetButtonColors(new Color(0.8f, 0, 0, 1));
        }

        if (status == LocationCellStatus.Yellow)
        {
            this.SetButtonColors(Color.yellow);
        }

        if (status == LocationCellStatus.Green)
        {
            this.SetButtonColors(new Color(0, 0.8f, 0, 1));
        }
    }

    private void SetButtonColors(Color color)
    {
        ColorBlock colors = button.colors;
        Color highlightValue = new Color(0.1f, 0.1f, 0.1f, 0);
        Color selectedValue = new Color(0.1f, 0.1f, 0.1f, 0);
        Color pressedValue = new Color(0.2f, 0.2f, 0.2f, 0);

        colors.normalColor = color;
        colors.highlightedColor = color + highlightValue;
        colors.selectedColor = color - selectedValue;
        colors.pressedColor = color + pressedValue;


        button.colors = colors;
    }

    public void OnClickCell()
    {
        Debug.Log(currentLocation.score);

        if (currentLocation.isHome) return;

        Loader.LoadScene(Loader.Scene.Game);

        // For Test Change Button color
        /*       
         *       if(currentLocation.status == LocationCellStatus.Red)
                {
                    currentLocation.status = LocationCellStatus.Green;
                }
                else if (currentLocation.status == LocationCellStatus.Yellow)
                {
                    currentLocation.status = LocationCellStatus.Red;
                }
                else if (currentLocation.status == LocationCellStatus.Green)
                {
                    currentLocation.status = LocationCellStatus.Yellow;
                }

                UpdateButtonColor();*/
    }
}
