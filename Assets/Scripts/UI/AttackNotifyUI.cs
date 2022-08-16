using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackNotifyUI : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private float upSpeed = 15f;
    private float fadeOutSpeed = 0.5f;

    // Start is called before the first frame update
    void Awake()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetText(string newText)
    {
        textMesh.text = newText;
    }

    public void SetCriticalTextColor()
    {
        textMesh.color = Color.red;
    }

    public void SetMissColor()
    {
        textMesh.color = Color.cyan;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = new(
            textMesh.rectTransform.position.x, 
            textMesh.rectTransform.position.y + (Time.deltaTime * upSpeed), 
            textMesh.rectTransform.position.z
        );

        textMesh.rectTransform.position = pos;

        textMesh.color = Color.Lerp(textMesh.color, Color.clear, Time.deltaTime * fadeOutSpeed);

        if (textMesh.color.a < 0.2)
        {
            Destroy(gameObject);
        }
    }
}
