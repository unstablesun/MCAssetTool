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



    public static string ImportFolder = "AssetCreation";

    void Awake()
    {
        Instance = this;
    }

    void Start() 
    {}

    void Update() 
    {}


    static public Sprite[] assetSprites;

    //This can be called when editing
    [MenuItem("Manifest/Load ManifestList Prefabs (Editor)")]
    private static void LoadManifestListPrefabs()
    {
        assetSprites = Resources.LoadAll<Sprite>(ImportFolder);


        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/" + ImportFolder + "/");
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

                //RectTransform rectTransform = go.GetComponent<RectTransform>();
                //rectTransform.localPosition = new Vector2(-1000, -1000);

                Image image = go.GetComponent<Image>();
                image.SetNativeSize();

                image.sprite = assetSprites[findex];

                go.name = f.Name;

                findex++;
            }

        }
    }

    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation1")]
    private static void SetManifestImportFolder1()
    { 
        ImportFolder = "AssetCreation";
    }
    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation2")]
    private static void SetManifestImportFolder2()
    {
        ImportFolder = "AssetCreation";
    }
    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation3")]
    private static void SetManifestImportFolder3()
    {
        ImportFolder = "AssetCreation";
    }
    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation4")]
    private static void SetManifestImportFolder4()
    {
        ImportFolder = "AssetCreation";
    }
    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation5")]
    private static void SetManifestImportFolder5()
    {
        ImportFolder = "AssetCreation";
    }
    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation6")]
    private static void SetManifestImportFolder6()
    {
        ImportFolder = "AssetCreation";
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


}

