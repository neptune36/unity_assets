using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class LinearProgressBar : MonoBehaviour
{
    public Text valueLabel;
    public Image fillImage;
    public string valueLabelFormat = "{value}/{max}";

    private int value,max=100;

    [Header("Test")]
    public int testValue;

    public void Init(int value, int max=100)
    {
        this.value = value;
        this.max = max;
        Set(this.value);
    }

    public void Set(int value)
    {
        this.value = value;

        fillImage.fillAmount = (float)this.value / max;
        valueLabel.text = valueLabelFormat.Replace("{value}", value.ToString()).Replace("{max}", max.ToString());
    }
    
}
#if UNITY_EDITOR
[CustomEditor(typeof(LinearProgressBar))]
public class LinearProgressBarEditor : Editor
{
    
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        LinearProgressBar myTarget = (LinearProgressBar)target;


        if (GUILayout.Button("Test"))
        {
            myTarget.Init(myTarget.testValue);
            SceneView.RepaintAll();
        }
        
      
    }
}
#endif