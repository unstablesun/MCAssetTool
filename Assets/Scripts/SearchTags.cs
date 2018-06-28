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

        public List<SearchTagRecord> SearchTagList { get; set; }

    }

    public class SearchTagData
    {
        public string SearchTagDataSet { get; set; }
        public string version { get; set; }

        public List<SearchTagRecord> SearchTagList { get; set; }

    }



    SearchTagData _searchTagData;


	void Start () 
    {
        OneTimeInit();
	}
	
	void Update () 
    {
		
	}


    public void OneTimeInit()
    {
        _searchTagData = new SearchTagData();


        _searchTagData.SearchTagDataSet = "set";
        _searchTagData.version = "0.0.1";
        _searchTagData.SearchTagList = new List<SearchTagRecord>();

        //fill in categories
        SearchTagRecord record = CreateRecord("Main");
        _searchTagData.SearchTagList.Add(record);

        record = CreateRecord("Appies");
        _searchTagData.SearchTagList.Add(record);

        record = CreateRecord("Deserts");
        _searchTagData.SearchTagList.Add(record);

        SaveJsonData();

    }

    public SearchTagRecord CreateRecord(string tag)
    {
        SearchTagRecord r = new SearchTagRecord();
        r.TagName = tag;
        r.SearchTagList = new List<SearchTagRecord>();

        return r;
    }

    public void SaveJsonData()
    {
        var jsonString = JsonConvert.SerializeObject(_searchTagData);

        string path = Application.dataPath + "/Resources/MasterTagList.json";


        File.WriteAllText(path, jsonString);

    }



}
