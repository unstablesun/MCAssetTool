using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.IO;
using MasterChef.data;


/*
 * ------------------------------------------------------------------------

                            PlatingChallengeManager


                        Tool Version 0.0.1


 * ------------------------------------------------------------------------
 */

public class PlatingChallengeManager : MonoBehaviour 
{
    public enum ePlatingState
    {
        Initialize,
        Load,
        Ready,
    }
    ePlatingState _state = ePlatingState.Initialize;



    public GameObject StagingImage;


    ChallengeEventData _challengeEventData;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch(_state)
        {
            case ePlatingState.Initialize:
                {
                    if (ChallengeManager.Instance.IsEventDataLoaded() == true)
                    {

                        _challengeEventData = ChallengeManager.Instance.GetChallengeEventData();

                        _state = ePlatingState.Load;
                    }
                }

                break;

            case ePlatingState.Load:


                Sprite s = ChallengeManager.Instance.GetChallengeImage(_challengeEventData.challengeRecord.platingImageFilename);


                StagingImage.GetComponent<Image>().sprite = s;
                StagingImage.GetComponent<Image>().SetNativeSize();


                //load pins

                _state = ePlatingState.Ready;

                break;


            case ePlatingState.Ready:

                break;




        }

    }



}
