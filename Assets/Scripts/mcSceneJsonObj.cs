using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MasterChef.data;

public class mcSceneJsonObj : MonoBehaviour 
{
    [System.Serializable]
    public class TagInfo
    {
        public int tagID;

        public enum topTags
        {
            tagMeat,
            tagBread,
            tagDrink,
            tagColorWhite,
            tagColorBrown,
            tagColorRed,
            tagColorGreen,
        }
        public topTags tt = topTags.tagMeat;
    }
 
    public List<TagInfo> tagInfo = new List<TagInfo>();

    public string ItemPrice = "1000";
    public string ItemName = "NameID";
    public string ItemDesc = "DescID";




	void Start () 
    {
        Image attachedImage = GetComponent<Image>();


        int depth = attachedImage.depth;
        float pWidth = attachedImage.preferredWidth;
        float pHeight = attachedImage.preferredHeight;

        Sprite sImage = attachedImage.sprite;
        string sName = sImage.name;

        System.Guid myGUID = System.Guid.NewGuid();
        Debug.Log("myGUID ->" + myGUID.ToString());


        /*
        Debug.Log("ObjInfo ->");
        Debug.Log("... depth = " + depth);
        Debug.Log("... pWidth = " + pWidth);
        Debug.Log("... pHeight = " + pHeight);
        Debug.Log("... sName = " + sName);
        */


	
	}
	
	void Update () 
    {
		
	}


    public void GetObjStruct()
    {

    }

}
