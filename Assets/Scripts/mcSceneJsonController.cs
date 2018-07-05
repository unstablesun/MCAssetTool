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


    PantryItemData pantryItemData = null;


	void Start () 
    {
        
        pantryItemData = new PantryItemData();
        pantryItemData.PantryItemList = new List<PantryItemRecord>();

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
                        PantryItemRecord pantryItemRecord = new PantryItemRecord();

                        pantryItemRecord.filename = jsonObj.name;
                        pantryItemRecord.ItemIsPrize = jsonObj.IsPrize.ToString();
                        pantryItemRecord.ItemNameLabel = jsonObj.ItemName;
                        pantryItemRecord.ItemPriceLabel = jsonObj.ItemPrice;
                        pantryItemRecord.ItemDescLabel = jsonObj.ItemDesc;
                        pantryItemRecord.ItemCreationTime = jsonObj.ItemCreationTime;

                        //Debug.Log("filename = " + jsonObj.name);

                        pantryItemRecord.TagList = new List<PantryItemTag>();

                        //get enum tags and search for 
                        foreach (mcSearchTags mcSTag in jsonObj.tagList)
                        {
                            PantryItemTag pantryTagItem = new PantryItemTag();

                            string tagStr = mcSTag.eTag.ToString();
                            pantryTagItem.ItemTag = tagStr;
                            pantryItemRecord.TagList.Add(pantryTagItem);
                        }

                        pantryItemData.PantryItemList.Add(pantryItemRecord);
                    }
                }
            }
        }

        SaveMasterList();

    }

    public void SaveMasterList()
    {
        var jsonString = JsonConvert.SerializeObject(pantryItemData);
        string path = Application.dataPath + "/Resources/PantryItemMasterList3.json";
        File.WriteAllText(path, jsonString);

    }

}
