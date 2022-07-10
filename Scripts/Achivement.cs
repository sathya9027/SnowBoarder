using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Achivement : MonoBehaviour
{
    [Header("Advanc Attrib")]
    [SerializeField] AdvancementSO[] advancSo;
    [SerializeField] Sprite advancOff;
    [SerializeField] Sprite advancOn;

    [Header("All Advance")]
    [SerializeField] TextMeshProUGUI[] advancTitle;
    [SerializeField] TextMeshProUGUI[] advancDescription;
    [SerializeField] Image[] advancIcon;
    [SerializeField] Image[] advancCheck;
    [SerializeField] Slider[] advancProgressSlider;

    private void Start()
    {
        ResetAdvancement();
    }

    private void ResetAdvancement()
    {
        for (int i = 0; i < advancSo.Length; i++)
        {
            switch (advancSo[i].GetAdvancPhrase())
            {
                case ((int)PhraseType.Once):
                    if (!advancSo[i].GetAdvancCompleted())
                    {
                        advancSo[i].SetAdvancProgress(0);
                    }
                    break;
            }
        }

        //Reset For Testing Purpose
        //for (int i = 0; i < advancSo.Length; i++)
        //{
        //    advancSo[i].SetAdvancCompleted(false);
        //    advancSo[i].SetAdvancProgress(0);
        //}
    }

    private void Update()
    {
        DisplayAdvancemnt();
    }

    private void DisplayAdvancemnt()
    {
        for (int i = 0; i < advancSo.Length; i++)
        {
            advancTitle[i].text = advancSo[i].GetAdvancTitle();
            advancDescription[i].text = advancSo[i].GetAdvancDesc();
            advancIcon[i].sprite = advancSo[i].GetAdvancIcon();
            advancProgressSlider[i].maxValue = advancSo[i].GetAdvancRequirment();
            advancProgressSlider[i].value = advancSo[i].GetAdvancProgress();
            if (advancSo[i].GetAdvancCompleted())
            {
                advancCheck[i].sprite = advancOn;
            }
            else
            {
                advancCheck[i].sprite = advancOff;
            }
        }
    }
}
