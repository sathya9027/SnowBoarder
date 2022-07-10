using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ProgressData : MonoBehaviour
{
    [SerializeField] string title;
    [SerializeField] string description;
    [SerializeField] float requirement;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Slider progressSlider;
    [SerializeField] AdvancementType advancementType;
    [SerializeField] PhraseType advancementPhrase;

    public void SaveData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/" + title + ".advanc";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, title);
    }
}
