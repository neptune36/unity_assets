using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class SyncRequest
{
    public static string GET(string uri)
    {
        Debug.Log("send get request to :" + uri);

        UnityWebRequest request = UnityWebRequest.Get(uri);
        request.SendWebRequest();

        DateTime launchTime = DateTime.Now;

        bool timeOut = false;
        while (!request.isDone)
        {
            if (DateTime.Now > launchTime.AddSeconds(5))
            {
                timeOut = true;
                break;
            }
        }

        if (timeOut)
        {
            throw new Exception("timeout");
        }

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            throw new Exception(request.error);
        }
        else
        {
            return request.downloadHandler.text;
        }
    }

    public static string POSTJSON(string uri, string json)
    {
        Debug.Log("send post request to :" + uri);

        UnityWebRequest request = new UnityWebRequest(uri, UnityWebRequest.kHttpVerbPOST);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();

        DateTime launchTime = DateTime.Now;

        bool timeOut = false;
        while (!request.isDone)
        {
            if (DateTime.Now > launchTime.AddSeconds(5))
            {
                timeOut = true;
                break;
            }
        }

        if (timeOut)
        {
            throw new Exception("timeout");
        }

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            throw new Exception(request.error);
        }
        else
        {
            return request.downloadHandler.text;
        }
    }
}
