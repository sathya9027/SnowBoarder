using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class AdvancCkeck : MonoBehaviour
{
    [SerializeField] AdvancementSO[] advancement;
    [SerializeField] TextMeshProUGUI advancTitle;

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckAdvancement();
    }

    private void CheckAdvancement()
    {
        for (int i = 0; i < advancement.Length; i++)
        {
            if (!advancement[i].GetAdvancCompleted())
            {
                switch (advancement[i].GetAdvancPhrase())
                {
                    case ((int)PhraseType.Once):
                        switch (advancement[i].GetAdvancType())
                        {
                            case ((int)AdvancementType.FrontFlip):
                                FrontFlipOnce(i);
                                break;
                            case ((int)AdvancementType.BackFlip):
                                BackFlipOnce(i);
                                break;
                            case ((int)AdvancementType.Score):
                                ScoreOnce(i);
                                break;
                        }
                        break;

                    case ((int)PhraseType.AllTime):
                        switch (advancement[i].GetAdvancType())
                        {
                            case ((int)AdvancementType.Distance):
                                if (advancement[i].GetAdvancProgress() >= advancement[i].GetAdvancRequirment())
                                {
                                    advancTitle.text = advancement[i].GetAdvancTitle();
                                    anim.Play("AdvancAnim");
                                    advancement[i].SetAdvancCompleted(true);
                                }
                                else
                                {
                                    advancement[i].SetAdvancCompleted(false);
                                    advancement[i].SetAdvancProgress(FindObjectOfType<TerrainCreation>().playerTravelDistance);
                                    Debug.Log(advancement[i].GetAdvancProgress());
                                }
                                break;
                        }
                        break;
                }
            }
        }
    }

    private void FrontFlipOnce(int i)
    {
        if (advancement[i].GetAdvancProgress() >= advancement[i].GetAdvancRequirment())
        {
            advancTitle.text = advancement[i].GetAdvancTitle();
            anim.Play("AdvancAnim");
            advancement[i].SetAdvancCompleted(true);
        }
        else
        {
            advancement[i].SetAdvancCompleted(false);
            advancement[i].SetAdvancProgress(FindObjectOfType<UIController>().frontFlipCount);
        }
    }

    private void BackFlipOnce(int i)
    {
        if (advancement[i].GetAdvancProgress() >= advancement[i].GetAdvancRequirment())
        {
            advancTitle.text = advancement[i].GetAdvancTitle();
            anim.Play("AdvancAnim");
            advancement[i].SetAdvancCompleted(true);
        }
        else
        {
            advancement[i].SetAdvancCompleted(false);
            advancement[i].SetAdvancProgress(FindObjectOfType<UIController>().backFlipCount);
        }
    }

    private void ScoreOnce(int i)
    {
        if (advancement[i].GetAdvancProgress() >= advancement[i].GetAdvancRequirment())
        {
            advancTitle.text = advancement[i].GetAdvancTitle();
            anim.Play("AdvancAnim");
            advancement[i].SetAdvancCompleted(true);
        }
        else
        {
            advancement[i].SetAdvancCompleted(false);
            advancement[i].SetAdvancProgress(FindObjectOfType<UIController>().score);
        }
    }
}