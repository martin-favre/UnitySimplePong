using UnityEngine;
using System.Collections.Generic;
using System;
[Serializable]
public abstract class SaveObj
{
    public SaveObj(string id)
    {
        identifier = id;
    }
    public abstract string GetJsonStr();
    [SerializeField]
    string identifier;
    public string GetIdentifier()
    {
        return identifier;
    }
}

public class SettingsSaverLoader : MonoBehaviour
{
    static List<string> readJsonObjects = new List<string>();
    static List<string> jsonStrings = new List<string>();    
    static string GetSaveFilePath()
    {
        return Application.persistentDataPath + "Save.phat";
    }
    public static void SaveObject(SaveObj obj)
    {
        string jsonstr = obj.GetJsonStr();
        Debug.Log("Saving object " + jsonstr);
        jsonStrings.Add(jsonstr);
    }

    // Will remove the object on loading, if successful
    public static T LoadObject<T>(string identifier) where T : SaveObj
    {
        foreach (string item in readJsonObjects)
        {
            T outItem = JsonUtility.FromJson<T>(item);
            if(outItem != null && identifier == outItem.GetIdentifier())
            {
                readJsonObjects.Remove(item);
                return outItem;
            }
        }
        Debug.LogWarning("Failed to load object with identifier: " + identifier);
        return default(T);
    }


    public static void PushToFile()
    {
        Debug.Log("Pushing all saves to " + GetSaveFilePath());
        System.IO.File.WriteAllLines(GetSaveFilePath(), jsonStrings);
    }

    private void Awake() 
    {
        if(System.IO.File.Exists(GetSaveFilePath()))
        {
            Debug.Log("Found save file " + GetSaveFilePath());
            string[] inStr = System.IO.File.ReadAllLines(GetSaveFilePath());
            jsonStrings = new List<string>(inStr);
            Debug.Log("Found " + jsonStrings.Count + " lines of savefile");
            foreach(string jsonStr in jsonStrings)
            {
                readJsonObjects.Add(jsonStr);
            }
        }
        else
        {
            Debug.Log("Did not find any save file");
        }
    }
}