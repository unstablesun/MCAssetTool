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


    [HideInInspector]
    public List<GameObject> PinObjectList = null;

    public GameObject PinObjectContainer;

    public GameObject StagingImage;


    ChallengeEventData _challengeEventData;

    void Start()
    {
        PinObjectList = new List<GameObject>();

    }

    public void MapPinClicked(int ID)
    {
        Debug.Log("MapPinClicked ID = " + ID);

        PlatingObject objectScript = QueryGetSelectedPin(ID);

        List<PantryManager.ItemRecord> validList = PantryManager.Instance.SearchForItemsWithTag("ColdDrinks_0");
        PantryManager.ItemRecord selected = validList[1];
        Sprite s = PantryManager.Instance.GetPantryItemImage(selected.filename);

        objectScript.SetItemImage(s);
    }

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

                int _id = 0;
                List<PlatingRecord> platingList = _challengeEventData.challengeRecord.PlatingList;
                foreach(PlatingRecord pr in platingList)
                {
                    Vector2 pinOffset = pr.relativePos;

                    GameObject _pinObj = Instantiate(Resources.Load("PlatingObject", typeof(GameObject))) as GameObject;

                    if (_pinObj != null)
                    {

                        if (PinObjectContainer != null)
                        {
                            _pinObj.transform.parent = PinObjectContainer.transform;
                        }
                        _pinObj.name = "pinObj";

                        _pinObj.transform.localPosition = new Vector2(pinOffset.x, pinOffset.y);
                        PlatingObject objectScript = _pinObj.GetComponent<PlatingObject>();

                        objectScript.ID = _id;
                        _id++;

                        PinObjectList.Add(_pinObj);
                    }
                }

                _state = ePlatingState.Ready;

                break;

            case ePlatingState.Ready:

                break;
        }
    }


    public PlatingObject QueryGetSelectedPin(int _id)
    {
        foreach (GameObject _pinObj in PinObjectList)
        {
            PlatingObject objectScript = _pinObj.GetComponent<PlatingObject>();
            if (objectScript.ID == _id)
            {
                return objectScript;
            }
        }
        return null;
    }


}
