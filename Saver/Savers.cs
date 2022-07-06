using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Savers : MonoBehaviour
{
    public List<Saver> saverList = new List<Saver>();

    public void Reset()
    {
        foreach(Saver saver in saverList)
        {
            saver.Clear();
            saver.Save();
        }
    }

    public void Load()
    {
        foreach (Saver saver in saverList)
        {
            saver.Load();
        }
    }

    public void Save()
    {
        foreach (Saver saver in saverList)
        {
            saver.Save();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Savers))]
    public class SaversEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Savers myTarget = (Savers)target;

            if (GUILayout.Button("Reset"))
            {
                myTarget.Reset();
            }if (GUILayout.Button("Load"))
            {
                myTarget.Load();
            }if (GUILayout.Button("Save"))
            {
                myTarget.Save();
            }
        }
    }
#endif
}
