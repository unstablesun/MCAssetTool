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
using MasterChef.data;

/*
 * ------------------------------------------------------------------------

                            ChallengeManager


                        Tool Version 0.0.1


 * ------------------------------------------------------------------------
 */

public class ChallengeManager : MonoBehaviour 
{

    ChallengeEventData _challengeEventData;

    //working variables
    private TextAsset jsonRawText = null;
    bool loadPending = true;

    static ChallengeManager s_instance;
    public static ChallengeManager Instance
    {
        get
        {
            return s_instance as ChallengeManager;
        }
    }

    void Awake()
    {
        s_instance = this;

        StartCoroutine(LoadChallengeDataAsync());
    }


    void Update()
    {
        if (loadPending == true)
        {
            loadPending = LoadCallengeJsonData();
        }
    }


    public bool IsEventDataLoaded()
    {
        return loadPending == false;
    }


    [HideInInspector]
    AssetBundle challengeDataBundle;

    IEnumerator LoadChallengeDataAsync()
    {
        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(Application.streamingAssetsPath, "realdatatest01"));
        while (!bundleLoadRequest.isDone)
        {
            yield return null;
        }

        challengeDataBundle = bundleLoadRequest.assetBundle;
        if (challengeDataBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }
    }

    public bool LoadCallengeJsonData()
    {
        bool pending = false;

        if (challengeDataBundle == null)
        {
            pending = true;
        }
        else
        {
            jsonRawText = challengeDataBundle.LoadAsset<TextAsset>("challengeEventData1.json") as TextAsset;
            if (jsonRawText == null)
                Debug.Log("LoadDefaults : jsonRawText load failed");
            else
                Debug.Log("LoadDefaults : jsonRawText success! = "/* + jsonRawText.ToString()*/);

            _challengeEventData = JsonConvert.DeserializeObject<ChallengeEventData>(jsonRawText.ToString());

            pending = false;
        }


        return pending;
    }

    public ChallengeEventData GetChallengeEventData()
    {
        Assert.IsNotNull(_challengeEventData);

        return _challengeEventData;
    }

    public Sprite GetChallengeImage(string filename)
    {
        Assert.IsNotNull(challengeDataBundle);

        return challengeDataBundle.LoadAsset<Sprite>(filename);
    }


}
