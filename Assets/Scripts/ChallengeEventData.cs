using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;

namespace MasterChef.data
{
    public enum PrizeType
    {
        pantryItem = 0,
        diamonds = 1,
        coins = 2,
    }

    [System.Serializable]
    public class PrizeRecord
    {
        public PrizeType type;
        public Int32 itemID;
        public int amount;
        public int prizeLevel;
    }


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
        public string idKey;

        public List<PlatingRecord> PlatingList { get; set; }
        public List<PrizeRecord> PrizeList { get; set; }
    }

    public class ChallengeEventData
    {
        public string MCEventDataSet { get; set; }
        public string version { get; set; }

        public ChallengeRecord challengeRecord { get; set; }
    }
}

