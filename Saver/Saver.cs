using Newtonsoft.Json;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName ="New saver",menuName ="Rudy/Saver/Saver")]
public class Saver : ScriptableObject
{
    private string path;
    private const string SAVE_FILE_DIRECTORY_NAME = "/Saves/";
    public string savefileName;
    public SaveableData dataToSave;

    private void OnEnable()
    {
        path = Application.persistentDataPath;
    }

    public string Fullpath()
    {
        return  path + SAVE_FILE_DIRECTORY_NAME;
    }

    public void Save()
    {
        Debug.Log("Saving data to "+ Fullpath() + savefileName + "...");

        if (!Directory.Exists(Fullpath()))
        {
            Directory.CreateDirectory(Fullpath());
            Debug.Log("Save directory created");
        }
        dataToSave.PreSave();
        string json = JsonUtility.ToJson(dataToSave, true);

        StreamWriter writer = new StreamWriter(Fullpath()+savefileName, false);
        writer.WriteLine(json);
        writer.Close();

        Debug.Log("Save done");
    }

    public void Load()
    {
        Debug.Log("Loading "+ Fullpath() + savefileName + "..." );
        if (File.Exists(Fullpath() + savefileName))
        {
            StreamReader reader = new StreamReader(Fullpath() + savefileName);
            string json = reader.ReadToEnd();
            reader.Close();

            JsonUtility.FromJsonOverwrite(json, dataToSave);
            dataToSave.PostLoad();

            Debug.Log("Load done");
        }
        else
        {
            Debug.Log("No save found");
        }

    }

    public void Clear()
    {
        dataToSave.Clear();
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(Saver))]
public class SaverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Saver myTarget = (Saver)target;

        if (GUILayout.Button("Load"))
        {
            myTarget.Load();
        }
        if (GUILayout.Button("Save"))
        {
            myTarget.Save();
        }
        if (GUILayout.Button("Clear"))
        {
            myTarget.Clear();
        }
    }
}
#endif