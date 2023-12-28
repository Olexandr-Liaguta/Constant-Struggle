using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProgressManager : MonoBehaviour
{

    #region Singelton

    static public SceneProgressManager instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    [SerializeField] private GameObject[] enemies;

    public void HandleEnemyDies(GameObject enemy)
    {
        enemies = enemies.Where(val => val.GetInstanceID() == enemy.GetInstanceID()).ToArray();

        if(enemies.Length == 0) {
            Cursor.visible = true;
            Loader.LoadScene(Loader.Scene.Map);
        }
    }
}
