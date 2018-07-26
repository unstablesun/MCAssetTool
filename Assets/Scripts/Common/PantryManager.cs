using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine.Assertions;



/*
 * ------------------------------------------------------------------------

                            MasterChef


                        Tool Version 0.0.2


 * ------------------------------------------------------------------------
 */

namespace MasterChef.data
{
    public class PantryManager : MonoBehaviour
    {
        public class BaseItemData
        {
            
        }

        public class ItemTag
        {
            public string Tag;
        }

        //secondary image overlays
        public class ItemImage
        {
            public string filename;
            public float offsetX;
            public float offsetY;
        }


        public class ItemRecord:BaseItemData
        {
            public Int32 Id;
            public string IsPrize;
            public string NameLabel;
            public string DescLabel;
            public string PriceLabel;
            public string CreationTime;
            public int Quantity;

            //primary image
            public string filename;


            public List<ItemTag> TagList { get; set; }
            public List<ItemImage> ImageList { get; set; }

        }

        public class ItemData
        {
            public string IngredientDataSet { get; set; }
            public string versionStr { get; set; }
            public Int64 versionId { get; set; }

            public List<ItemRecord> ItemList { get; set; }

        }

        //working variables
        private TextAsset jsonRawText = null;
        bool loadPending = true;

        ItemData _pantryItemData;

        static PantryManager s_instance;
        public static PantryManager Instance
        {
            get
            {
                return s_instance as PantryManager;
            }
        }

        void Awake()
        {
            s_instance = this;

            StartCoroutine(LoadPantryItemDataAsync());
        }


        void Update()
        {
            if (loadPending == true)
            {
                loadPending = LoadPantryItemJsonData();
            }
        }



        [HideInInspector]
        AssetBundle pantryItemDataBundle;

        IEnumerator LoadPantryItemDataAsync()
        {
            var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "pantryitems001"));
            while (!bundleLoadRequest.isDone)
            {
                yield return null;
            }

            pantryItemDataBundle = bundleLoadRequest.assetBundle;
            if (pantryItemDataBundle == null)
            {
                Debug.Log("Failed to load AssetBundle!");
                yield break;
            }
        }

        public bool LoadPantryItemJsonData()
        {
            bool pending = false;

            if (pantryItemDataBundle == null)
            {
                pending = true;
            }
            else
            {
                jsonRawText = pantryItemDataBundle.LoadAsset<TextAsset>("PantryItemMasterList.json") as TextAsset;
                if (jsonRawText == null)
                    Debug.Log("LoadDefaults : jsonRawText load failed");
                else
                    Debug.Log("LoadDefaults : jsonRawText success! = "/* + jsonRawText.ToString()*/);

                _pantryItemData = JsonConvert.DeserializeObject<ItemData>(jsonRawText.ToString());

                pending = false;
            }


            return pending;
        }


        public ItemData GetItemData()
        {
            Assert.IsNotNull(_pantryItemData);

            return _pantryItemData;
        }


        public List<ItemRecord> SearchForItemsWithTag(string searchTagColumn)
        {
            List<ItemRecord> returnList = new List<ItemRecord>();

            List<ItemRecord> itemList = _pantryItemData.ItemList;
            foreach (ItemRecord itemRecord in itemList)
            {
                List<ItemTag> tagList = itemRecord.TagList;

                foreach (ItemTag it in tagList)
                {
                    if(it.Tag == searchTagColumn)
                    {
                        returnList.Add(itemRecord);
                    }
                }

            }

            return returnList;

        }

        public Sprite GetPantryItemImage(string filename)
        {
            Assert.IsNotNull(pantryItemDataBundle);

            return pantryItemDataBundle.LoadAsset<Sprite>(filename);

        }



        public void OnButtonClickTestSearch()
        {
            //whole list
            //List<ItemRecord> validateList = _pantryItemData.ItemList;

            //search list
            List<ItemRecord> validateList = SearchForItemsWithTag("ColdDrinks_0");



            foreach (ItemRecord item in validateList)
            {
                Debug.Log("Item Filename = " + item.filename);
            }

        }


    }
}


