using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.IO;using MasterChef.data;

public class mcSceneJsonController : MonoBehaviour 
{
    public List<GameObject> bundleList = null;

    public string dataSetStr = "base";
    public string versionStr = "0.0.0";


    PantryManager.ItemData pantryItemData = null;


	void Start () 
    {
        
        pantryItemData = new PantryManager.ItemData();
        pantryItemData.ItemList = new List<PantryManager.ItemRecord>();

	}


    public void onButtonClickParseChildren()
    {
        pantryItemData.version = versionStr;
        pantryItemData.IngredientDataSet = dataSetStr;

        foreach(GameObject go in bundleList)
        {
            foreach (Transform childObj in go.transform)
            {
                GameObject go2 = childObj.gameObject;

                mcSceneJsonObj jsonObj = go2.GetComponent<mcSceneJsonObj>();

                if (jsonObj != null)
                {
                    if (jsonObj.IncludeInExport == true)
                    {
                        PantryManager.ItemRecord pantryItemRecord = new PantryManager.ItemRecord();

                        pantryItemRecord.filename = jsonObj.name;
                        pantryItemRecord.IsPrize = jsonObj.IsPrize.ToString();
                        pantryItemRecord.NameLabel = jsonObj.ItemName;
                        pantryItemRecord.PriceLabel = jsonObj.ItemPrice;
                        pantryItemRecord.DescLabel = jsonObj.ItemDesc;
                        pantryItemRecord.CreationTime = jsonObj.ItemCreationTime;

                        //Debug.Log("filename = " + jsonObj.name);

                        pantryItemRecord.TagList = new List<PantryManager.ItemTag>();

                        //get enum tags and search for 
                        foreach (mcSearchTags mcSTag in jsonObj.tagList)
                        {
                            PantryManager.ItemTag pantryTagItem = new PantryManager.ItemTag();

                            string tagStr = mcSTag.eTag.ToString();
                            pantryTagItem.Tag = tagStr;
                            pantryItemRecord.TagList.Add(pantryTagItem);
                        }

                        pantryItemData.ItemList.Add(pantryItemRecord);
                    }
                }
            }
        }

        SaveMasterList();

    }

    public void SaveMasterList()
    {
        var jsonString = JsonConvert.SerializeObject(pantryItemData);
        string path = Application.dataPath + "/Resources/PantryItemMasterList.json";
        File.WriteAllText(path, jsonString);

    }

}
