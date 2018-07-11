using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

public class SearchTags : MonoBehaviour 
{

    public class SearchTagRecord
    {
        public string TagName;
        public int column;

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

	void Start () 
    {
        //----------------------------------
        //MCAssetTool Only!
        //OneTimeInit();
        //WriteTagsToScriptEnums();
        //----------------------------------
 	}
	
	void Update () 
    {
        if (loadPending == true)
        {
            loadPending = LoadSearchTagJsonData();
        }
	}


    public void OneTimeInit()
    {
        _searchTagData = new SearchTagData();


        _searchTagData.SearchTagDataSet = "set";
        _searchTagData.version = "0.0.1";
        _searchTagData.SearchTagList = new List<SearchTagRecord>();

        //fill in categories

        //------------------MAIN-------------------
        SearchTagRecord record = CreateRecord("Main", 0);
        _searchTagData.SearchTagList.Add(record);


            SearchTagRecord subRecord = CreateRecord("Pizza", 1);
            record.SearchTagList.Add(subRecord);

            subRecord = CreateRecord("Veggies", 1);
            record.SearchTagList.Add(subRecord);

            subRecord = CreateRecord("Chicken", 1);
            record.SearchTagList.Add(subRecord);

            subRecord = CreateRecord("Pancakes", 1);
            record.SearchTagList.Add(subRecord);



        //-----------------------------------------


        //----------------APPETIZER----------------
        record = CreateRecord("Appetizer", 0);
        _searchTagData.SearchTagList.Add(record);

            subRecord = CreateRecord("Bread", 1);
            record.SearchTagList.Add(subRecord);


        //-----------------------------------------

        //DESERT
        record = CreateRecord("Desert", 0);
        _searchTagData.SearchTagList.Add(record);

            subRecord = CreateRecord("Dounut", 1);
            record.SearchTagList.Add(subRecord);

            subRecord = CreateRecord("Icecream", 1);
            record.SearchTagList.Add(subRecord);

            subRecord = CreateRecord("Cookie", 1);
            record.SearchTagList.Add(subRecord);

        //COLD DRINKS
        record = CreateRecord("ColdDrinks", 0);
        _searchTagData.SearchTagList.Add(record);

            subRecord = CreateRecord("Beer", 1);
            record.SearchTagList.Add(subRecord);

            subRecord = CreateRecord("Wine", 1);
            record.SearchTagList.Add(subRecord);

            subRecord = CreateRecord("Mix", 1);
            record.SearchTagList.Add(subRecord);

        //HOT DRINKS
        record = CreateRecord("HotDrinks", 0);
        _searchTagData.SearchTagList.Add(record);

            subRecord = CreateRecord("Tea", 1);
            record.SearchTagList.Add(subRecord);

        //PLATES
        record = CreateRecord("Plates", 0);
        _searchTagData.SearchTagList.Add(record);

            subRecord = CreateRecord("Dinner", 1);
            record.SearchTagList.Add(subRecord);

        //PLACEMATS
        record = CreateRecord("PlaceMats", 0);
        _searchTagData.SearchTagList.Add(record);

            subRecord = CreateRecord("Carbon", 1);
            record.SearchTagList.Add(subRecord);

        //CUTLERY
        record = CreateRecord("Cutlery", 0);
        _searchTagData.SearchTagList.Add(record);

            subRecord = CreateRecord("Knife", 1);
            record.SearchTagList.Add(subRecord);

            subRecord = CreateRecord("Fork", 1);
            record.SearchTagList.Add(subRecord);

            subRecord = CreateRecord("Spoon", 1);
            record.SearchTagList.Add(subRecord);

        //CENTER PIECES
        record = CreateRecord("CenterPieces", 0);
        _searchTagData.SearchTagList.Add(record);

            subRecord = CreateRecord("Vase", 1);
            record.SearchTagList.Add(subRecord);

            subRecord = CreateRecord("Candle", 1);
            record.SearchTagList.Add(subRecord);


        SaveJsonData();

    }


    public SearchTagRecord CreateRecord(string tag, int column)
    {
        SearchTagRecord r = new SearchTagRecord();
        r.TagName = tag;
        r.column = column;
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




    public void WriteTagsToScriptEnums()
    {
        string path = "Assets/Scripts/SearchTagEnums.cs";
        StreamWriter writer = new StreamWriter(path, false);

        writer.WriteLine("/* This is file is generated by MCAssetTool.  Do not edit */");
        writer.WriteLine("using UnityEngine;");
        writer.WriteLine("using System.Collections;");
        writer.WriteLine("using System.Collections.Generic;");
        writer.WriteLine(" ");

        writer.WriteLine("[System.Serializable]");

        writer.WriteLine("public class mcSearchTags");
        writer.WriteLine("{");

        //writer.WriteLine("\tpublic int tagID;");
        writer.WriteLine("\tpublic enum eSearchTags");

        writer.WriteLine("\t{");

        //loop
        foreach(SearchTagRecord sr in _searchTagData.SearchTagList)
        {
            writer.WriteLine("\t\t" + sr.TagName + "_" + sr.column + ",");

            if(sr.SearchTagList.Count > 0)
            {
                foreach (SearchTagRecord sr1 in sr.SearchTagList)
                {
                    writer.WriteLine("\t\t\t" + sr1.TagName + "_" + sr1.column + ",");

                    if (sr1.SearchTagList.Count > 0)
                    {
                        foreach (SearchTagRecord sr2 in sr1.SearchTagList)
                        {
                            writer.WriteLine("\t\t\t\t" + sr2.TagName + "_" + sr2.column + ",");

                            if (sr2.SearchTagList.Count > 0)
                            {
                                foreach (SearchTagRecord sr3 in sr2.SearchTagList)
                                {
                                    writer.WriteLine("\t\t\t\t\t" + sr3.TagName + "_" + sr3.column + ",");
                                }
                            }
                       }
                    }
                }
            }
        }
 
        writer.WriteLine("\t}");
        writer.WriteLine(" ");
        writer.WriteLine("\tpublic eSearchTags eTag = eSearchTags.Main_0;");
        writer.WriteLine("}");

        writer.Close();
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
        foreach(SearchTagRecord tagRecord in tagList)
        {
            
            string tag_column = tagRecord.TagName + "_" + tagRecord.column;

            if(tag_column == searchTagColumn)
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

    public void OnButtonClickTestSearch()
    {

        List<SearchTagRecord> result = GetTopLevelTagList();

        foreach(SearchTagRecord str in result)
        {
            Debug.Log("Top Level = " + str.TagName);
        }

        result = SearchForTagList("ColdDrinks_0");

        foreach (SearchTagRecord str in result)
        {
            Debug.Log("Sub Level = " + str.TagName);
        }

    }


}
