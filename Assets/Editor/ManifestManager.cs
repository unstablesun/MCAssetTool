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



    public static string ImportFolder = "AssetCreationPack1";

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
        ImportFolder = "AssetCreationPack1";
    }
    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation2")]
    private static void SetManifestImportFolder2()
    {
        ImportFolder = "AssetCreationPack2";
    }
    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation3")]
    private static void SetManifestImportFolder3()
    {
        ImportFolder = "AssetCreationPack3";
    }
    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation4")]
    private static void SetManifestImportFolder4()
    {
        ImportFolder = "AssetCreationPack4";
    }
    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation5")]
    private static void SetManifestImportFolder5()
    {
        ImportFolder = "AssetCreationPack5";
    }
    [MenuItem("Manifest/SetManifestImportFolder to AssetCreation6")]
    private static void SetManifestImportFolder6()
    {
        ImportFolder = "AssetCreationPack6";
    }


    //reference
    private static void GetFileList()
    {

        DirectoryInfo dir = new DirectoryInfo("Assets/AssetCreation/");

        FileInfo[] info = dir.GetFiles("*.png");

        foreach (FileInfo f in info)
        {
            Debug.Log(f.ToString());
        }

    }


    [MenuItem("Pantry/Generate Pantry Item IDs (Editor)")]
    private static void GeneratePantryItemIDs()
    {

        GameObject selectedGameObject = Selection.activeGameObject;

        Debug.Log("PANTRY EDIT selectedGameObject = " + selectedGameObject.name);

        foreach (Transform child in selectedGameObject.transform)
        {
            print("Foreach loop: " + child);

            System.Guid _GUID = System.Guid.NewGuid();
            byte[] gb = _GUID.ToByteArray();
            Int32 newId = System.BitConverter.ToInt32(gb, 0);

            mcSceneJsonObj script = child.GetComponent<mcSceneJsonObj>();

            script.Id = newId;
        }

        /*
        Component[] kids = selectedGameObject.GetComponentsInChildren<mcSceneJsonObj>();

        for (int i = 0; i < kids.Length; i++)
        {
            Debug.Log(".... = " + kids[i].name);

            System.Guid _GUID = System.Guid.NewGuid();
            byte[] gb = _GUID.ToByteArray();
            Int32 newId = System.BitConverter.ToInt32(gb, 0);


            mcSceneJsonObj script = kids[i].GetComponent<mcSceneJsonObj>();

            script.Id = newId;


        }
        */




    }

    [MenuItem("Pantry/Pantry Item SetDate String (Editor)")]
    private static void PantryItemSetDateString()
    {
        GameObject selectedGameObject = Selection.activeGameObject;
        mcSceneJsonObj script = selectedGameObject.GetComponent<mcSceneJsonObj>();

        script.ItemCreationTime = System.DateTime.UtcNow.ToString("yyyy-MM-dd");
    }

    [MenuItem("Pantry/Pantry Item ResetID (Editor)")]
    private static void PantryItemSetResetID()
    {
        GameObject selectedGameObject = Selection.activeGameObject;
        mcSceneJsonObj script = selectedGameObject.GetComponent<mcSceneJsonObj>();

        System.Guid _GUID = System.Guid.NewGuid();
        byte[] gb = _GUID.ToByteArray();
        Int32 newId = System.BitConverter.ToInt32(gb, 0);

        script.Id = newId;
    }



}

