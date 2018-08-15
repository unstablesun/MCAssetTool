using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine.Networking;


/*
 * ------------------------------------------------------------------------

                            Master Asset Manifest


                        Tool Version 0.0.1


 * ------------------------------------------------------------------------
 */

public class MasterAssetManifest : MonoBehaviour
{
    public enum LoadSequenceState
    {
        InitLoadSeq = 0,
        DownLoadFromS3 = 1,
        LoadFromTempCache = 2,
        LoadComplete = 3,
        InTool = 4
    }
    private LoadSequenceState _loadSequenceState = LoadSequenceState.InitLoadSeq;

    public enum MCSystem
    {
        PantryItems = 0,
        SearchTags = 1,
        PlatingTables = 2,
        IgniteEvents = 3
    }

    public enum ManifestAction
    {
        Nominal = 0,
        Refresh = 1,
        Update = 2,
        Delete = 3
    }

    public class AssetBundleRecord
    {
        public int action;
        public int system;
        public string filename;
        public int version;


    }

    [System.Serializable]
    public class AssetManifestData
    {
        public string AssetBundleDataSet { get; set; }
        public string version { get; set; }
        public bool forceUpdate { get; set; }

        public List<AssetBundleRecord> AssetBundleList { get; set; }

    }

    //working variables
    private TextAsset jsonRawText = null;
    bool loadPending = true;
    bool loadS3 = false;
    public bool LoadComplete = false;

    AssetManifestData _assetManifestData;



    static MasterAssetManifest s_instance;
    public static MasterAssetManifest Instance
    {
        get
        {
            return s_instance as MasterAssetManifest;
        }
    }

    void Awake()
    {
        s_instance = this;

        LoadComplete = false;

        //COMMENT OUT FOR GAME
        _loadSequenceState = LoadSequenceState.InTool;

        if (_loadSequenceState != LoadSequenceState.InTool)
        {
            _loadSequenceState = LoadSequenceState.DownLoadFromS3;
            loadS3 = false;
            StartCoroutine(DownloadAssetManifestTagsS3());
        }
        else
        {

            CreateManifestData();
        }
    }


    void Update()
    {

        switch (_loadSequenceState)
        {
            case LoadSequenceState.InitLoadSeq:
                break;

            case LoadSequenceState.DownLoadFromS3:

                if (loadS3 == true)
                {
                    _loadSequenceState = LoadSequenceState.LoadFromTempCache;
                    StartCoroutine(LoadAssetManifestAsync());
                }

                break;

            case LoadSequenceState.LoadFromTempCache:


                if (loadPending == true)
                {
                    loadPending = LoadAssetManifestJsonData();
                }
                else
                {
                    _loadSequenceState = LoadSequenceState.LoadComplete;
                }

                break;

            case LoadSequenceState.LoadComplete:
                LoadComplete = true;
                break;
        }

    }



    public AssetBundleRecord CreateRecord(int action, int mcSystem, string filename, int version)
    {
        AssetBundleRecord r = new AssetBundleRecord();

        r.action = action;
        r.filename = "MasterAssetManifest";
        r.system = mcSystem;
        r.version = 1;

        return r;
    }


    public void SaveJsonData()
    {
        var jsonString = JsonConvert.SerializeObject(_assetManifestData);

        string path = Application.dataPath + "/Resources/MasterAssetManifest.json";


        File.WriteAllText(path, jsonString);

    }

    public void CreateManifestData()
    {
        _assetManifestData = new AssetManifestData();

        _assetManifestData.AssetBundleDataSet = "MasterAssetManifest";
        _assetManifestData.version = "0.0.1";

        _assetManifestData.AssetBundleList = new List<AssetBundleRecord>();


        AssetBundleRecord record = CreateRecord
        (
            (int)ManifestAction.Nominal,
            (int)MCSystem.PantryItems,
            "pantryitems001",
            1
        );
        _assetManifestData.AssetBundleList.Add(record);

        record = CreateRecord
        (
            (int)ManifestAction.Nominal,
            (int)MCSystem.PlatingTables,
            "platingtables",
            1
        );
        _assetManifestData.AssetBundleList.Add(record);

        record = CreateRecord
        (
            (int)ManifestAction.Nominal,
            (int)MCSystem.SearchTags,
            "mastertaglistbundle",
            1
        );
        _assetManifestData.AssetBundleList.Add(record);

        record = CreateRecord
        (
            (int)ManifestAction.Update,
            (int)MCSystem.IgniteEvents,
            "noop",
            1
        );
        _assetManifestData.AssetBundleList.Add(record);

        SaveJsonData();

    }






    IEnumerator DownloadAssetManifestTagsS3()
    {
        Debug.Log("FUEL_S3 DownloadAssetManifestTagsS3");

#if UNITY_ANDROID
        Debug.Log("FUEL_S3 DownloadPantryItemsS3 trying to download pantryitems001");
        var uwr = new UnityWebRequest("https://s3.amazonaws.com/master-chef-debug/asset-bundles-android/mastermanifestbundle", UnityWebRequest.kHttpVerbGET);
#elif UNITY_IOS
        Debug.Log("FUEL_S3 DownloadPantryItemsS3 trying to download pantryitems001");
        var uwr = new UnityWebRequest("https://s3.amazonaws.com/master-chef-debug/asset-bundles-ios/mastermanifestbundle", UnityWebRequest.kHttpVerbGET);
#endif

        string path = Path.Combine(Application.temporaryCachePath, "mastermanifestbundle");
        uwr.downloadHandler = new DownloadHandlerFile(path);

        uwr.chunkedTransfer = false;

        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError || uwr.isHttpError)
        {
            Debug.LogError(uwr.error);
            Debug.Log("FUEL_S3 error " + uwr.error);
        }
        else
        {
            Debug.Log("FUEL_S3 DownloadAssetManifestTagsS3 : File successfully downloaded and saved to " + path);

            loadS3 = true;
        }
    }

    [HideInInspector]
    AssetBundle assetManifestBundle;

    IEnumerator LoadAssetManifestAsync()
    {
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.temporaryCachePath, "mastermanifestbundle"));
        while (!bundleLoadRequest.isDone)
        {
            yield return null;
        }

        assetManifestBundle = bundleLoadRequest.assetBundle;
        if (assetManifestBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }
    }

    public bool LoadAssetManifestJsonData()
    {
        if (assetManifestBundle == null)
        {
            return true;
        }
        else
        {
            jsonRawText = assetManifestBundle.LoadAsset<TextAsset>("MasterTagList.json") as TextAsset;
            if (jsonRawText == null)
                Debug.Log("LoadSearchTagJsonData LoadDefaults : jsonRawText load failed");
            else
                Debug.Log("LoadSearchTagJsonData LoadDefaults : jsonRawText success! = " + jsonRawText.ToString());

            _assetManifestData = JsonConvert.DeserializeObject<AssetManifestData>(jsonRawText.ToString());
        }

        Debug.Log("FUEL_LOG: LoadAssetManifestJsonData success!");


        return false;
    }





}
