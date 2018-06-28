﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;


// Usage
// 1. Put this script to Assets/Editor/
// 2. Execute [Menu] -> [Build] -> [Build Asset Bundle]
public class UploadToS3
{
    /*
    // Enter your s3 informations
    private const string s3Bucket = "";
    private const string s3KeyBase = "";
    private const string s3AccessKeyId = "";
    private const string s3SecretAccessKey = "";

    private const string baseDir = "AssetBundles";
    private const string s3PostUrl = "http://" + s3Bucket + ".s3.amazonaws.com/";

    private static readonly BuildTarget[] buildTargets =
        new BuildTarget[] { BuildTarget.iOS, BuildTarget.Android };

    private static readonly Dictionary<string, string> postParams = new Dictionary<string, string> {
        { "acl", "public-read" },
        { "Content-Type", "application/octet-stream" },
        { "x-amz-meta-uuid", "14365123651274" },
        { "AWSAccessKeyId", s3AccessKeyId },
    };

    private const string policyBase = "{{" +
        "\"expiration\": \"{0}\"," +
        "\"conditions\": [" +
        "    {{\"bucket\": \"" + s3Bucket + "\"}}," +
        "    [\"starts-with\", \"$key\", \"{1}\"]," +
        "    {{\"acl\": \"public-read\"}}," +
        "    [\"starts-with\", \"$Content-Type\", \"application/octet-stream\"]," +
        "    {{\"x-amz-meta-uuid\": \"14365123651274\"}}" +
        "  ]" +
        "}}";


    [MenuItem("Build/Build AssetBundles")]
    public static void Build()
    {
        if (Directory.Exists(baseDir))
        {
            Directory.Delete(baseDir, true);
        }
        Directory.CreateDirectory(baseDir);

        foreach (BuildTarget target in buildTargets)
        {
            buildForPlatform(target);
        }
    }

    private static void buildForPlatform(BuildTarget target)
    {
        var platform = target.ToString().ToLower();
        string dir = Path.Combine(baseDir, platform);
        Directory.CreateDirectory(dir);

        BuildPipeline.BuildAssetBundles(dir,
                                        BuildAssetBundleOptions.ChunkBasedCompression,
                                        target);

        var dirInfo = new DirectoryInfo(dir);
        foreach (var fileInfo in dirInfo.GetFiles())
        {
            Debug.Log("upload : " + fileInfo.FullName);
            uploadToS3(platform, fileInfo);
        }
    }

    private static void uploadToS3(string platform, FileInfo fileInfo)
    {

        var form = new WWWForm();
        foreach (KeyValuePair<string, string> pair in postParams)
        {
            form.AddField(pair.Key, pair.Value);
        }

        string s3Key = Path.Combine(s3KeyBase, platform + "/" + fileInfo.Name);
        form.AddField("key", s3Key);

        string expiration = DateTime.Now.AddMinutes(1).ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
        string policy = string.Format(policyBase, expiration, s3Key);
        string base64Policy = Convert.ToBase64String(Encoding.UTF8.GetBytes(policy));

        var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(s3SecretAccessKey));
        string signature = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(base64Policy)));

        form.AddField("Policy", base64Policy);
        form.AddField("Signature", signature);

        byte[] fileBytes = File.ReadAllBytes(fileInfo.FullName);
        form.AddBinaryData("file", fileBytes, fileInfo.Name, "application/octet-stream");

        if (!postRequestWithRetry(s3Key, form))
        {
            Debug.LogError("Upload failed : " + s3Key);
        }
    }

    private static bool postRequestWithRetry(string s3Key, WWWForm form, int retryLimit = 5)
    {
        for (int i = 0; i < retryLimit; i++)
        {
            if (postRequest(s3Key, form))
            {
                return true;
            }
            Debug.Log("Retry : " + (i + 1));
        }
        return false;
    }

    private static bool postRequest(string s3Key, WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(s3PostUrl, form))
        {
            var s = www.Send();
            long startTick = DateTime.Now.Ticks;
            while (!s.isDone)
            {
                if (DateTime.Now.Ticks > startTick + 10L * 10000000L)
                {
                    Debug.LogWarning("Timeout");
                    break;
                }
            }

            if (string.IsNullOrEmpty(www.error))
            {
                Debug.Log("Uploaded : " + s3PostUrl + s3Key);
                return true;

            }
            else
            {
                Debug.LogWarning("Error : " + www.error);
                return false;
            }
        }
    }
    */

}