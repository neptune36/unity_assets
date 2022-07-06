using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class AsyncRequest : MonoBehaviour
{
    public static AsyncRequest Generate(string title, bool visible = true)
    {
        GameObject go= Instantiate(Resources.Load("AsyncRequest")) as GameObject;
        go.transform.SetParent(FindObjectOfType<Canvas>().transform, false);
        LoadingPanel lp = go.GetComponent<LoadingPanel>();
        lp.loadingText = title;
        lp.visible = visible;
        return go.GetComponent<AsyncRequest>();
    }

    public void GET(string url, Action<string> onSuccess, Action<string> onError)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        StartCoroutine(Send(www, onSuccess, onError));
    }
    
    public void POSTJSON(string url,string json, Action<string> onSuccess, Action<string> onError)
    {
        UnityWebRequest www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        www.uploadHandler = new UploadHandlerRaw(bodyRaw);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        StartCoroutine(Send(www, onSuccess, onError));
    }

    private IEnumerator Send(UnityWebRequest www, Action<string> onSuccess, Action<string> onError)
    {
        yield return www.SendWebRequest();

        if(www.result == UnityWebRequest.Result.ProtocolError || www.result == UnityWebRequest.Result.ConnectionError)
        {
            onError(www.error);
        }
        else
        {
            onSuccess(www.downloadHandler.text);
        }
        Destroy(gameObject);
    }
}
