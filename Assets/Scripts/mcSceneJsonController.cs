using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.IO;using MasterChef.data;
using System;

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


        string path = "Assets/Resources/PantryItemIDs.csv";
        if (File.Exists(path) == true)
        {
            File.Delete(path);
        }


        pantryItemData.versionStr = versionStr;
        pantryItemData.IngredientDataSet = dataSetStr;

        System.Guid _GUID_V = System.Guid.NewGuid();
        byte[] gb_v = _GUID_V.ToByteArray();
        Int64 versionId = System.BitConverter.ToInt64(gb_v, 0);
        pantryItemData.versionId = versionId;

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

                        pantryItemRecord.Id = jsonObj.Id;
                        pantryItemRecord.filename = jsonObj.name;
                        pantryItemRecord.NameLabel = jsonObj.ItemName;
                        pantryItemRecord.PriceLabel = jsonObj.ItemPrice;
                        pantryItemRecord.DescLabel = jsonObj.ItemDesc;
                        pantryItemRecord.CreationTime = jsonObj.ItemCreationTime;
                        pantryItemRecord.Quantity = jsonObj.ItemQuantity;
                        pantryItemRecord.PurchaseCurrency = (int)jsonObj.PurchaceCurrency;

                        Vector2 cp = jsonObj.CenterOffset.transform.localPosition;
                        pantryItemRecord.CenterOffset = new Vector2(cp.x, cp.y);

                        Vector2 sp1 = jsonObj.StackOffset1.transform.localPosition;
                        Vector2 sp2 = jsonObj.StackOffset2.transform.localPosition;
                        Vector2 sp3 = jsonObj.StackOffset3.transform.localPosition;
                        Vector2 sp4 = jsonObj.StackOffset4.transform.localPosition;
 
                        //Main tag for this item
                        pantryItemRecord.TagList = new List<PantryManager.ItemTag>();
                        foreach (mcSearchTags mcSTag in jsonObj.tagList)
                        {
                            PantryManager.ItemTag pantryTagItem = new PantryManager.ItemTag();

                            string tagStr = mcSTag.eTag.ToString();
                            pantryTagItem.Tag = tagStr;
                            pantryItemRecord.TagList.Add(pantryTagItem);
                        }

                        //init stack list for all stackables
                        pantryItemRecord.StackObjectList = new List<PantryManager.StackObject>();


                        //Stack 1
                        if(jsonObj.stackTagList1.Count > 0)
                        {
                            PantryManager.StackObject sObj = new PantryManager.StackObject();
                            sObj.StackTagList = new List<PantryManager.ItemTag>();
                            sObj.StackOffset = sp1;

                            foreach (mcSearchTags mcStackTag in jsonObj.stackTagList1)
                            {
                                PantryManager.ItemTag pantryTagItem = new PantryManager.ItemTag();

                                string tagStr = mcStackTag.eTag.ToString();
                                pantryTagItem.Tag = tagStr;
                                sObj.StackTagList.Add(pantryTagItem);
                            }

                            pantryItemRecord.StackObjectList.Add(sObj);
                        }


                        //Stack 2
                        if (jsonObj.stackTagList2.Count > 0)
                        {
                            PantryManager.StackObject sObj = new PantryManager.StackObject();
                            sObj.StackTagList = new List<PantryManager.ItemTag>();
                            sObj.StackOffset = sp2;

                            foreach (mcSearchTags mcStackTag in jsonObj.stackTagList2)
                            {
                                PantryManager.ItemTag pantryTagItem = new PantryManager.ItemTag();

                                string tagStr = mcStackTag.eTag.ToString();
                                pantryTagItem.Tag = tagStr;
                                sObj.StackTagList.Add(pantryTagItem);
                            }

                            pantryItemRecord.StackObjectList.Add(sObj);
                        }


                        //Stack 3
                        if (jsonObj.stackTagList3.Count > 0)
                        {
                            PantryManager.StackObject sObj = new PantryManager.StackObject();
                            sObj.StackTagList = new List<PantryManager.ItemTag>();
                            sObj.StackOffset = sp3;

                            foreach (mcSearchTags mcStackTag in jsonObj.stackTagList3)
                            {
                                PantryManager.ItemTag pantryTagItem = new PantryManager.ItemTag();

                                string tagStr = mcStackTag.eTag.ToString();
                                pantryTagItem.Tag = tagStr;
                                sObj.StackTagList.Add(pantryTagItem);
                            }

                            pantryItemRecord.StackObjectList.Add(sObj);
                        }


                        //Stack 4
                        if (jsonObj.stackTagList4.Count > 0)
                        {
                            PantryManager.StackObject sObj = new PantryManager.StackObject();
                            sObj.StackTagList = new List<PantryManager.ItemTag>();
                            sObj.StackOffset = sp4;

                            foreach (mcSearchTags mcStackTag in jsonObj.stackTagList4)
                            {
                                PantryManager.ItemTag pantryTagItem = new PantryManager.ItemTag();

                                string tagStr = mcStackTag.eTag.ToString();
                                pantryTagItem.Tag = tagStr;
                                sObj.StackTagList.Add(pantryTagItem);
                            }

                            pantryItemRecord.StackObjectList.Add(sObj);
                        }


                        pantryItemRecord.Flags = 0;

                        pantryItemData.ItemList.Add(pantryItemRecord);
                    }
                }
            }
        }

        SaveMasterList();

        WritePantryItemIDs();

    }

    public void SaveMasterList()
    {
        var jsonString = JsonConvert.SerializeObject(pantryItemData);
        string path = Application.dataPath + "/Resources/PantryItemMasterList.json";
        File.WriteAllText(path, jsonString);

    }




    public void WritePantryItemIDs()
    {
        
        string path = "Assets/Resources/PantryItemIDs.csv";
        StreamWriter writer = new StreamWriter(path, false);

        writer.WriteLine("Name,ID,Date,Price,Currency,Quantity,File");



        //loop
        foreach (PantryManager.ItemRecord iR in pantryItemData.ItemList)
        {
            string purchaseType = "coins";
            if (iR.PurchaseCurrency == (int)PantryManager.PurchaseCurrency.diamonds)
                purchaseType = "diamonds";


            writer.WriteLine(iR.NameLabel + "," + iR.Id + "," + iR.CreationTime + "," + iR.PriceLabel + "," + purchaseType + "," + iR.Quantity + "," + iR.filename);
        }

        //writer.WriteLine(" ");
 
        writer.Close();
    }



}
