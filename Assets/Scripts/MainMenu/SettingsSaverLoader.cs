using UnityEngine;
using System.Collections.Generic;

public abstract class SaveObj
{
    public SaveObj(string id)
    {
        identifier = id;
    }
    public abstract string GetJsonStr();
    public readonly string identifier;
}

public class SettingsSaverLoader : MonoBehaviour
{
    readonly static string saveFile = Application.persistentDataPath + "Save.phat";

    public static void SaveObject(SaveObj obj)
    {
        string jsonstr = JsonUtility.ToJson(obj);
        jsonStrings.Add(jsonstr);
    }
    public static SaveObj LoadObject(string identifier)
    {
        foreach (SaveObj item in readJsonObjects)
        {
            if(identifier == item.identifier)
            {
                return item;
            }
        }
        return null;
    }

    static List<SaveObj> readJsonObjects = new List<SaveObj>();
    static List<string> jsonStrings = new List<string>();    

    public static void PushToFile()
    {
        System.IO.File.WriteAllLines(saveFile, jsonStrings);
    }

    private void Awake() 
    {
        if(System.IO.File.Exists(saveFile))
        {
            string[] inStr = System.IO.File.ReadAllLines(saveFile);
            jsonStrings = new List<string>(inStr);
            foreach(string jsonStr in jsonStrings)
            {
                SaveObj obj = JsonUtility.FromJson<SaveObj>(jsonStr);
                readJsonObjects.Add(obj);
            }
        }
    }
}