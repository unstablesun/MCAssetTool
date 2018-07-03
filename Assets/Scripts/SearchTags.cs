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

        //------------------MAIN-------------------
        SearchTagRecord record = CreateRecord("Main");
        _searchTagData.SearchTagList.Add(record);


        SearchTagRecord subRecord = CreateRecord("Beef");
        record.SearchTagList.Add(subRecord);

        subRecord = CreateRecord("Veggies");
        record.SearchTagList.Add(subRecord);

        subRecord = CreateRecord("Chicken");
        record.SearchTagList.Add(subRecord);

        subRecord = CreateRecord("Fish");
        record.SearchTagList.Add(subRecord);

        //-----------------------------------------


        //----------------APPETIZER----------------
        record = CreateRecord("Appetizer");
        _searchTagData.SearchTagList.Add(record);

        subRecord = CreateRecord("Soup");
        record.SearchTagList.Add(subRecord);

        subRecord = CreateRecord("Bread");
        record.SearchTagList.Add(subRecord);

        //-----------------------------------------

        //DESERT
        record = CreateRecord("Desert");
        _searchTagData.SearchTagList.Add(record);


        //COLD DRINKS
        record = CreateRecord("ColdDrinks");
        _searchTagData.SearchTagList.Add(record);



        //HOT DRINKS
        record = CreateRecord("HotDrinks");
        _searchTagData.SearchTagList.Add(record);

        //PLATES
        record = CreateRecord("Plates");
        _searchTagData.SearchTagList.Add(record);

        //PLACEMATS
        record = CreateRecord("PlaceMats");
        _searchTagData.SearchTagList.Add(record);

        //CUTLERY
        record = CreateRecord("Cutlery");
        _searchTagData.SearchTagList.Add(record);

        //CENTER PIECES
        record = CreateRecord("CenterPieces");
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

    public void LoadJsonData()
    {

    }


}
