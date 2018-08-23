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
 * -----------------------------------------------------------------------------
 * 
 *      mcChallengeContainer - tool only container for creating json
 * 
 * 
 * -----------------------------------------------------------------------------
 */
public class mcChallengeContainer : MonoBehaviour 
{
    public string dataSetStr = "base";
    public string versionStr = "0.0.0";

    public string title;
    public string style;
    public string location;
    public string cost;
    public string earn;
    public string descShort;
    public string descLong;
    public string time;
    public string platingImageFilename;
    public string challengeImageFilename;
    public string requirements;

    public List<PrizeRecord> Prizes = null;

    public InputField EventTagINPUT;
    public InputField EventOutPutCopy;
    public Text MCEventOutPutFileName;
    public Toggle ToggleLock;
    public Text ExportStatusText;



    [HideInInspector]
    public ChallengeEventData challengeEventData;

    private string DateISOString;

    private string MCEventFileName;

	void Start () 
    {

        challengeEventData = new ChallengeEventData();
        challengeEventData.challengeRecord = new ChallengeRecord();

        DateISOString = System.DateTime.UtcNow.ToString("yyyy-MM-dd");

        MCEventFileName = "MCEvent_" + DateISOString;

	}
    void Update()
    {
    }

	public void onButtonClickedExportContainer () 
    {
        challengeEventData.MCEventDataSet = dataSetStr;
        challengeEventData.version = versionStr;

        Image image = GetComponent<Image>();
        challengeEventData.challengeRecord.platingImageFilename = image.sprite.name;


        challengeEventData.challengeRecord.title = title;
        challengeEventData.challengeRecord.style = style;
        challengeEventData.challengeRecord.location = location;
        challengeEventData.challengeRecord.cost = cost;
        challengeEventData.challengeRecord.earn = earn;
        challengeEventData.challengeRecord.descShort = descShort;
        challengeEventData.challengeRecord.descLong = descLong;
        challengeEventData.challengeRecord.time = time;
        challengeEventData.challengeRecord.requirements = requirements;

        //prizes
        challengeEventData.challengeRecord.PrizeList = new List<PrizeRecord>();
        if(Prizes != null)
        {
            foreach(PrizeRecord pR in Prizes)
            {
                challengeEventData.challengeRecord.PrizeList.Add(pR);
            }
        }




        //Plating list
        challengeEventData.challengeRecord.PlatingList = new List<PlatingRecord>();

        foreach (Transform childObj in transform)
        {
            GameObject go = childObj.gameObject;

            mcPlatingJsonObj jsonObj = go.GetComponent<mcPlatingJsonObj>();

            if (jsonObj != null)
            {
                if (jsonObj.IsPlatingObj == true)
                {
                    Vector2 xyPos = jsonObj.transform.localPosition;

                    GameObject offsetMarker = jsonObj.OffetMarker;
                    Vector2 pinOffset = offsetMarker.transform.localPosition;

                    Debug.Log("xyPos = " + xyPos.ToString());


                    PlatingRecord platingRecord = new PlatingRecord();
                    platingRecord.relativePos = xyPos;
                    platingRecord.pinOffset = pinOffset;
                    platingRecord.tagList = new List<string>();

                    foreach (mcSearchTags st in jsonObj.tagList)
                    {
                        platingRecord.tagList.Add(st.eTag.ToString());
                    }


                    platingRecord.isRequired = jsonObj.IsRequired;
                    platingRecord.placementOrder = jsonObj.PlacementOrder;


                    challengeEventData.challengeRecord.PlatingList.Add(platingRecord);
                } 
                else//special case - get challenge splash image
                {
                    Image image2 = jsonObj.GetComponent<Image>();
                    challengeEventData.challengeRecord.challengeImageFilename = image2.sprite.name;

                }

            }
        }



        SaveMasterList();
	}


    public void SaveMasterList()
    {
        if(ToggleLock.isOn == true)
        {
            MCEventFileName = MCEventOutPutFileName.text;
        }
        else
        {
            MCEventFileName = MCEventFileName + "_" + EventTagINPUT.text;

            MCEventOutPutFileName.text = MCEventFileName;
            EventOutPutCopy.text = MCEventFileName;

        }

        var jsonString = JsonConvert.SerializeObject(challengeEventData);
        string path = Application.dataPath + "/Resources/MCEventJsonData/" + MCEventFileName + ".json";
        File.WriteAllText(path, jsonString);


        ExportStatusText.text = "Export Complete!";

    }

}
