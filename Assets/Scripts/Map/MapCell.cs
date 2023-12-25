using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapCell : MonoBehaviour
{

    enum CellStatus
    {
        Red,
        Yellow,
        Green,
    }

    [SerializeField ] private bool isHome = false;

    [SerializeField] private CellStatus status;
    [SerializeField] private Button button;

    private void Start()
    {
        if (isHome) return;


        if (this.status == CellStatus.Red)
        {
            this.SetButtonColors(new Color(0.9f, 0, 0, 1));
        }

        if (this.status == CellStatus.Yellow)
        {
            this.SetButtonColors(Color.yellow);
        }

        if (this.status == CellStatus.Green)
        {
            this.SetButtonColors(new Color(0, 0.9f, 0, 1));
        }

    }

    private void SetButtonColors(Color color)
    {
        ColorBlock colors = button.colors;
        Color highlightValue = new Color(0.1f, 0.1f, 0.1f, 0);

        colors.normalColor = color;
        colors.highlightedColor = color + highlightValue;

        button.colors = colors;
    }

    public void OnClickCell()
    {
        if(isHome)
        {
            // Load Home scene
        } else
        {
          SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
