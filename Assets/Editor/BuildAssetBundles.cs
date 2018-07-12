using UnityEditor;
using System.IO;

public class CreateAssetBundlesAndroid
{
    [MenuItem("Assets/Build AssetBundles Android")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles/Android";
        if(!Directory.Exists(assetBundleDirectory))
		{
    		Directory.CreateDirectory(assetBundleDirectory);
		}

		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}

public class CreateAssetBundlesIOS
{
    [MenuItem("Assets/Build AssetBundles iOS")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles/iOS";
        if(!Directory.Exists(assetBundleDirectory))
		{
    		Directory.CreateDirectory(assetBundleDirectory);
		}

		BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.iOS);
    }
}


public class CreateAssetBundlesAll
{
    [MenuItem("Assets/Build AssetBundles All")]
    static void BuildAllAssetBundles()
    {
        string assetBundleDirectory = "Assets/AssetBundles/iOS";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.iOS);


        assetBundleDirectory = "Assets/AssetBundles/Android";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }

        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}

