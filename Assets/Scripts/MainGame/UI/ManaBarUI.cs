using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBarUI : MonoBehaviour
{
    [SerializeField] private Image manaImage;

    [SerializeField] private TextMeshProUGUI manaText;


    private void Start()
    {
        PlayerStats.Instance.OnManaChange += UpdateMana;
    }

    public void UpdateMana(object sender, PlayerStats.OnManaChangeArgs args)
    {
        float manaPercent = args.mana / args.maxMana;

        manaImage.fillAmount = manaPercent;

        manaText.text = (int)args.mana + " / " + (int)args.maxMana;
    }
}
