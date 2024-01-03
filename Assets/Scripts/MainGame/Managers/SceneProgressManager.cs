using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneProgressManager : MonoBehaviour
{

    static public SceneProgressManager Instance { get; private set; }
    public readonly float TIME_AFTER_COMPLETE_LEVEL = 10f;

    public event EventHandler OnAllEnemiesDie;

    [SerializeField] private GameObject enemiesContainer;

    private List<Transform> enemies = new();

    private float countdownTimer;
    private bool isLevelComplete = false;
    private bool isCountdownCompleted = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (Transform child in enemiesContainer.transform)
        {
            if (child.gameObject.layer == LayerMask.NameToLayer("Enemy") && child.gameObject.activeSelf)
            {
                enemies.Add(child);
            }
        }
    }

    private void Update()
    {
        if (isLevelComplete)
        {
            if (countdownTimer > 0)
            {
                countdownTimer -= Time.deltaTime;
            }
            else if(!isCountdownCompleted)
            {
                isCountdownCompleted = true;
                Cursor.visible = true;
                GameInputManager.Instance.DisablePlayerActions();
                Loader.LoadScene(Loader.Scene.Map);
            }
        }
    }

    public void HandleEnemyDies(GameObject enemy)
    {
        enemies = enemies.Where(val => val.gameObject.GetInstanceID() != enemy.GetInstanceID()).ToList();

        if (enemies.Count == 0)
        {
            countdownTimer = TIME_AFTER_COMPLETE_LEVEL;
            OnAllEnemiesDie?.Invoke(this, EventArgs.Empty);
            isLevelComplete = true;
        }
    }
}
