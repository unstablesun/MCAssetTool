using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;





/*
 * ------------------------------------------------------------------------

                            SearchTags


                        Tool Version 0.0.5


 * ------------------------------------------------------------------------
 */

public partial class SearchTags : MonoBehaviour
{
    public string TagItem;

    public int experience;

    public int Level
    {
        get { return experience / 750; }
    }


    public class SearchTagRecord
    {
        public SearchTagRecord parentRecord;
        public string TagName;
        public int column;
        public Int32 id;

        public List<SearchTagRecord> SearchTagList { get; set; }

    }

    [System.Serializable]
    public class SearchTagData
    {
        public string SearchTagDataSet { get; set; }
        public string version { get; set; }

        public List<SearchTagRecord> SearchTagList { get; set; }

    }

    //working variables
    private TextAsset jsonRawText = null;
    //private TextAsset jsonRawList = null;
    bool loadPending = true;

    SearchTagData _searchTagData;



    static SearchTags s_instance;
    public static SearchTags Instance
    {
        get
        {
            return s_instance as SearchTags;
        }
    }

    void Awake()
    {
        s_instance = this;

        StartCoroutine(LoadSearchTagsAsync());
    }

    void Start()
    {

        ReadCSV();
    }

    void Update()
    {
        if (loadPending == true)
        {
            loadPending = LoadSearchTagJsonData();
        }
    }



    public SearchTagRecord CreateRecord(string tag, int column, Int32 id)
    {
        SearchTagRecord r = new SearchTagRecord();
        r.parentRecord = null;
        r.TagName = tag;
        r.column = column;
        r.id = id;
        r.SearchTagList = new List<SearchTagRecord>();

        return r;
    }


    public void SaveJsonData()
    {
        var jsonString = JsonConvert.SerializeObject(_searchTagData);

        string path = Application.dataPath + "/Resources/MasterTagList.json";


        File.WriteAllText(path, jsonString);

    }


    [HideInInspector]
    AssetBundle searchTagBundle;

    IEnumerator LoadSearchTagsAsync()
    {
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "mastertaglistbundle"));
        while (!bundleLoadRequest.isDone)
        {
            yield return null;
        }

        searchTagBundle = bundleLoadRequest.assetBundle;
        if (searchTagBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }
    }

    public bool LoadSearchTagJsonData()
    {
        bool pending = false;

        if (searchTagBundle == null)
        {
            pending = true;
        }
        else
        {
            jsonRawText = searchTagBundle.LoadAsset<TextAsset>("MasterTagList.json") as TextAsset;
            if (jsonRawText == null)
                Debug.Log("LoadDefaults : jsonRawText load failed");
            else
                Debug.Log("LoadDefaults : jsonRawText success! = " + jsonRawText.ToString());

            _searchTagData = JsonConvert.DeserializeObject<SearchTagData>(jsonRawText.ToString());

            pending = false;
        }
        //DebugPrintRawText();


        return pending;
    }





    /*
     * ------------------------------------------------------------------------

                                TAG SEARCH


            ex. Search for Main_0 = find reacords just under Main_0


     * ------------------------------------------------------------------------
     */
    public List<SearchTagRecord> GetTopLevelTagList()
    {
        List<SearchTagRecord> returnList = new List<SearchTagRecord>();


        List<SearchTagRecord> tagList = _searchTagData.SearchTagList;
        foreach (SearchTagRecord tagRecord in tagList)
        {
            returnList.Add(tagRecord);
        }

        return returnList;

    }

    public List<SearchTagRecord> SearchForTagList(string searchTagColumn)
    {
        List<SearchTagRecord> returnList = new List<SearchTagRecord>();


        List<SearchTagRecord> tagList = _searchTagData.SearchTagList;
        foreach (SearchTagRecord tagRecord in tagList)
        {

            string tag_column = tagRecord.TagName + "_" + tagRecord.column;

            if (tag_column == searchTagColumn)
            {
                if (tagRecord.SearchTagList != null && tagRecord.SearchTagList.Count > 0)
                {
                    List<SearchTagRecord> subTagList = tagRecord.SearchTagList;
                    foreach (SearchTagRecord subTagRecord in subTagList)
                    {
                        returnList.Add(subTagRecord);
                    }
                }
                else
                {
                    returnList.Add(tagRecord);
                }
                break;
            }

        }

        return returnList;

    }


    public void CreateLinkedList()
    {

        List<SearchTagRecord> tagList = _searchTagData.SearchTagList;
        foreach (SearchTagRecord tagRecord in tagList)
        {
            //highest level
            if (tagRecord.SearchTagList != null && tagRecord.SearchTagList.Count > 0)
            {
                List<SearchTagRecord> subTagList = tagRecord.SearchTagList;
                foreach (SearchTagRecord subTagRecord in subTagList)
                {
                    subTagRecord.parentRecord = tagRecord;

                    //level 2
                    if (subTagRecord.SearchTagList != null && subTagRecord.SearchTagList.Count > 0)
                    {
                        List<SearchTagRecord> subTagList2 = subTagRecord.SearchTagList;
                        foreach (SearchTagRecord subTagRecord2 in subTagList2)
                        {
                            subTagRecord2.parentRecord = subTagRecord;


                            //level 3
                            if (subTagRecord2.SearchTagList != null && subTagRecord2.SearchTagList.Count > 0)
                            {
                                List<SearchTagRecord> subTagList3 = subTagRecord2.SearchTagList;
                                foreach (SearchTagRecord subTagRecord3 in subTagList3)
                                {
                                    subTagRecord3.parentRecord = subTagRecord2;




                                }
                            }

                        }
                    }
                }
            }
        }
    }



    public void OnButtonClickTestSearch()
    {
        CreateLinkedList();


        List<SearchTagRecord> result = GetTopLevelTagList();

        foreach (SearchTagRecord str in result)
        {
            Debug.Log("Top Level = " + str.TagName);
        }

        result = SearchForTagList("ColdDrinks_0");

        foreach (SearchTagRecord str in result)
        {
            Debug.Log("Sub Level = " + str.TagName + " : parentRecord tagname =  " + str.parentRecord.TagName);
        }

    }


    public void OnButtonClickCreateTags()
    {
        //MCAssetTool Only!

        ReadCSV();
        //OneTimeInit();
        WriteTagsToScriptEnums();
    }



}
