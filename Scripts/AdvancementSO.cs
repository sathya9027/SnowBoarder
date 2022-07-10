using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Achievements", fileName = "Advanc")]
[System.Serializable]
public class AdvancementSO : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] string advancementTitle = "Enter new advancement title here";
    [TextArea(2, 6)]
    [SerializeField] string advancementDescription = "Enter new advancement description here";
    [SerializeField] Sprite advancementIcon;
    [SerializeField] AdvancementType advancementType;
    [SerializeField] PhraseType advancementPhrase;
    [SerializeField] float advancementRequirement;

    bool isCompleted;
    float advancProgress;

    public string GetAdvancTitle()
    {
        return advancementTitle;
    }

    public string GetAdvancDesc()
    {
        return advancementDescription;
    }

    public Sprite GetAdvancIcon()
    {
        return advancementIcon;
    }

    public int GetAdvancPhrase()
    {
        return ((int)advancementPhrase);
    }

    public int GetAdvancType()
    {
        return ((int)advancementType);
    }

    public float GetAdvancRequirment()
    {
        return advancementRequirement;
    }

    public bool GetAdvancCompleted()
    {
        return isCompleted;
    }

    public float GetAdvancProgress()
    {
        return advancProgress;
    }

    public void SetAdvancCompleted(bool complete)
    {
        isCompleted = complete;
    }

    public void SetAdvancProgress(float progress)
    {
        advancProgress = progress;
    }

}
