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



    public ChallengeEventData challengeEventData;


	void Start () 
    {

        challengeEventData = new ChallengeEventData();
        challengeEventData.challengeRecord = new ChallengeRecord();

	}
    void Update()
    {
    }

	public void onButtonClickedExportContainer () 
    {
        challengeEventData.IngredientDataSet = dataSetStr;
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

                    Debug.Log("xyPos = " + xyPos.ToString());


                    PlatingRecord platingRecord = new PlatingRecord();
                    platingRecord.relativePos = xyPos;
                    platingRecord.tagList = new List<string>();

                    foreach (mcSearchTags st in jsonObj.tagList)
                    {
                        platingRecord.tagList.Add(st.eTag.ToString());
                    }


                    platingRecord.isRequired = jsonObj.IsRequired;


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
        var jsonString = JsonConvert.SerializeObject(challengeEventData);
        string path = Application.dataPath + "/Resources/challengeEventData1.json";
        File.WriteAllText(path, jsonString);

    }

}
