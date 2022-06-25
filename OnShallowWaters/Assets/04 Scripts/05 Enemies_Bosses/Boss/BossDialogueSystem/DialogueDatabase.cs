using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

[System.Serializable]
public class BossDialogue
{
    public List<Dialogue> dialogueList;
}

[System.Serializable]
public class AllBoss
{
    public List<BossDialogue> bossDialogue;
}

public class DialogueDatabase : MonoBehaviour
{
    public string path;
    public AllBoss allBoss;
    public TextAsset bossJson;
    string json;

    void Start()
    {
        json = bossJson.text;
        allBoss = JsonConvert.DeserializeObject<AllBoss>(json);
    }
}
