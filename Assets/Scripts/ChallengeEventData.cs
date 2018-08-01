using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace MasterChef.data
{
    /*
    public class PrizeRecord
    {
        public string title;
        public string subTitle;
        public string diamondLevel;
        public string starLevel;
        public string imageFilename;
    }
    */

    public class PlatingRecord
    {
        public bool isRequired;
        public int placementOrder;
        public List<string> tagList;
        public Vector2 relativePos;
        public Vector2 pinOffset;
    }

    public class ChallengeRecord
    {
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

        public List<PlatingRecord> PlatingList { get; set; }
    }

    public class ChallengeEventData
    {
        public string IngredientDataSet { get; set; }
        public string version { get; set; }

        public ChallengeRecord challengeRecord { get; set; }
    }
}

