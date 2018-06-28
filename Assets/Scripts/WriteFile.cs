
using UnityEngine;
using UnityEditor;
using System.IO;
/*
public class HandleTextFile
{
    [MenuItem("Tools/Write file")]
    static void WriteString()
    {
        string path = "Assets/Resources/foodData.json";

		string jsonString = "{\r\n  \"IngredientDataSet\": \"Core\",\r\n  \"version\": \"0.1\",\r\n  \"IngredientList\": [\r\n    {\r\n      \"name\": \"Capers\",\r\n      \"filename\": \"rawIngredients_capers\",\r\n      \"id\": \"001\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Red Peppers\",\r\n      \"filename\": \"rawIngredients_peppers\",\r\n      \"id\": \"002\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Beans\",\r\n      \"filename\": \"rawIngredients_beans\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Asperagus\",\r\n      \"filename\": \"rawIngredients_asperagus\",\r\n      \"id\": \"004\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Chilli Peppers Green\",\r\n      \"filename\": \"rawIngredients_chilliPepperGreen\",\r\n      \"id\": \"005\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Chilli Peppers Red\",\r\n      \"filename\": \"rawIngredients_chilliPepperRed\",\r\n      \"id\": \"006\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Chilli Peppers Dry\",\r\n      \"filename\": \"rawIngredients_chilliPepperDry\",\r\n      \"id\": \"007\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Cranberries\",\r\n      \"filename\": \"rawIngredients_cranberries\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Garlic\",\r\n      \"filename\": \"rawIngredients_garlic\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Sliced Ginger\",\r\n      \"filename\": \"rawIngredients_gingerSliced\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Cut Green Onions\",\r\n      \"filename\": \"rawIngredient_greenOnionCut\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Hot Peppers\",\r\n      \"filename\": \"rawIngredient_hotPeppers\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Lentals\",\r\n      \"filename\": \"rawIngredient_lentals\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Musturd Powder\",\r\n      \"filename\": \"rawIngredient_musturdPowder\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Olives\",\r\n      \"filename\": \"rawIngredient_olives\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n      \"name\": \"Sliced Onions\",\r\n      \"filename\": \"rawIngredient_onionsSliced\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n     {\r\n      \"name\": \"Orange Root\",\r\n      \"filename\": \"rawIngredient_orangeRoot\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n     {\r\n      \"name\": \"Orange Slice\",\r\n      \"filename\": \"rawIngredient_orangeSlice\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n     {\r\n      \"name\": \"Pumpkin Seeds\",\r\n      \"filename\": \"rawIngredient_pumpkinSeeds\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n     {\r\n      \"name\": \"Red Pepper Corns\",\r\n      \"filename\": \"rawIngredient_redPepperCorn\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n     {\r\n      \"name\": \"Safron\",\r\n      \"filename\": \"rawIngredient_safron\",\r\n      \"id\": \"003\",\r\n      \"type\": \"raw\"\r\n    },\r\n    {\r\n    \"name\": \"Walnuts\",\r\n    \"filename\": \"rawIngredient_walnuts\",\r\n    \"id\": \"003\",\r\n    \"type\": \"raw\"\r\n    }\r\n  ]\r\n}";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(jsonString);
        writer.Close();

        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path); 
		//TextAsset asset = Resources.Load(path) as TextAsset;



        //Print the text from the file
        //Debug.Log(asset.text);
    }

    [MenuItem("Tools/Read file")]
    static void ReadString()
    {
        string path = "Assets/Resources/test.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path); 
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

}
*/
