using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.IO;
using System;
using UnityEngine.UI;

public class ManifestManager : MonoBehaviour
{
    public static ManifestManager Instance = null;

    void Awake()
    {
        Instance = this;
    }

    //void Start() 
    //{}

    //void Update() 
    //{}

    //[SerializeField]
    //public string PrefabName = "ManifestListX";

    //[SerializeField]
    //public int NumPointsUsed = 0;

    //[SerializeField]
    //public ManifestEntry[] mManifestEntries = null;

    /*
    //This must be called at runtime
    [MenuItem("Manifest/Save ManifestList Prefab (Runtime)")]
    private static void SaveManifestListPrefab()
    {
        if (ManifestManager.Instance != null)
        {

            if (ManifestManager.Instance.mManifestEntries != null)
            {

                ManifestManager.Instance.CreateAndSavePrefab();

            }
        }
    }
    */
    /*
    private void CreateAndSavePrefab()
    {
        GameObject objectPrefab = new GameObject(PrefabName);

        ManifestList scriptRef = objectPrefab.AddComponent<ManifestList>() as ManifestList;

        //int length = WayPointManager.Instance.mWayPointEditList.GetLength (0);
        int length = NumPointsUsed;
        scriptRef.InitList(length);
        for (int i = 0; i < length; i++)
        {

            ManifestEntry me = ManifestManager.Instance.mManifestEntries[i];
            scriptRef.AddManifestEntry(me, i);

        }
        scriptRef.PrefabName = PrefabName;
        scriptRef.NumEntriesUsed = NumPointsUsed;

        UnityEngine.Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/Resources/Prefabs/Manifests/" + objectPrefab.name + ".prefab");
        PrefabUtility.ReplacePrefab(objectPrefab, prefab, ReplacePrefabOptions.ConnectToPrefab);
    }
    */

    static public Sprite[] assetSprites;

    //This can be called when editing
    [MenuItem("Manifest/Load ManifestList Prefabs (Editor)")]
    private static void LoadManifestListPrefabs()
    {
        assetSprites = Resources.LoadAll<Sprite>("AssetCreation");


        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/AssetCreation/");
        FileInfo[] info = dir.GetFiles("*.png");


        GameObject selectedGameObject = Selection.activeGameObject;

        DirectoryInfo dirInfo = new DirectoryInfo("Assets/Resources/ManifestPrefabs/");
        FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");
        foreach (FileInfo file in fileInf)
        {
            
            UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Resources/ManifestPrefabs/" + file.Name, typeof(GameObject));

            int findex = 0;
            foreach (FileInfo f in info)
            {
                GameObject go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                go.transform.parent = selectedGameObject.transform;

                RectTransform rectTransform = go.GetComponent<RectTransform>();
                rectTransform.localPosition = new Vector2(-1000, -1000);

                Image image = go.GetComponent<Image>();
                image.SetNativeSize();


                image.sprite = assetSprites[findex];

                go.name = f.Name;

                findex++;
            }




        }
    }




    private static void GetFileList()
    {

        DirectoryInfo dir = new DirectoryInfo("Assets/AssetCreation/");

        FileInfo[] info = dir.GetFiles("*.png");

        foreach (FileInfo f in info)
        {
            Debug.Log(f.ToString());
        }

    }
    /*
    //This must be called at runtime
    [MenuItem("Manifest/Edit Selected ManifestList (Runtime)")]
    private static void EditSelectedManifestList()
    {

        GameObject selectedGameObject = Selection.activeGameObject;
        Debug.Log("selectedGameObject = " + selectedGameObject.name);

        //this is the WayPointList
        ManifestList meList = selectedGameObject.GetComponent<ManifestList>();


        //this is the WayPointManager
        GameObject WayPointManagerObject = GameObject.Find("ManifestManager");
        ManifestManager meManager = WayPointManagerObject.GetComponent<ManifestManager>();

        int numPoints = meList.NumEntriesUsed;

        meManager.PrefabName = meList.PrefabName;
        meManager.NumPointsUsed = meList.NumEntriesUsed;

        Debug.Log("EditSelectedWayPoint : numPoints = " + numPoints);
        for (int i = 0; i < numPoints; i++)
        {

            ManifestEntry me = meList.GetManifestEntryAtIndex(i);
            ManifestManager.Instance.mManifestEntries[i] = me;

        }

    }
    */


}

