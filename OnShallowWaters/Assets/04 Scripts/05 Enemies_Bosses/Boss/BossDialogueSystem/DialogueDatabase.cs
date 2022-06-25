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


    // Start is called before the first frame update
    void Start()
    {
        json = bossJson.text;
        allBoss = JsonConvert.DeserializeObject<AllBoss>(json);
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //this is to generate a template for editing later not intended for in-game
        //    string json = JsonUtility.ToJson(allBoss);
        //    //bossJson. = json; 
        //    Debug.Log(json);
        //}


    }
}
