using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.IO;
using MasterChef.data;

public class JsonDataManager : MonoBehaviour 
{
    [HideInInspector]
    public PantryManager.ItemData pantryItemData;

	void Start () 
    {}
	


    //Probably only be saving here
    public void LoadMasterList()
    {
        TextAsset jsonObj = Resources.Load("PantryItemMasterList") as TextAsset;

        if (jsonObj == null)
            Debug.Log("LoadDefaults : jsonRawText load failed");
        //else
        //    Debug.Log("LoadDefaults : jsonRawText success! = " + jsonObj.ToString());

        pantryItemData = JsonConvert.DeserializeObject<PantryManager.ItemData>(jsonObj.ToString());

    }


    public void SaveMasterList()
    {
        var jsonString = JsonConvert.SerializeObject(pantryItemData);
        string path = Application.dataPath + "/Resources/PantryItemMasterList2.json";
        File.WriteAllText(path, jsonString);



    }

    private void DebugPrint()
    {

    }

}
