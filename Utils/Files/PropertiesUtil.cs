using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PropertiesUtil {


    public static Dictionary<string, string> Read(string filename) 
    {
        Dictionary<string, string> properties = new Dictionary<string, string>();

        string[] lines = File.ReadAllLines(@filename);

        foreach (string line in lines)
        {

            if (!line.StartsWith("#"))
            {
                if (line.Contains("="))
                {
                    string[] parts = line.Split('=');
                    properties.Add(parts[0].Trim(), parts[1].Trim());
                }
            }
        }

        return properties;        
    }

    public static string ReadProperty(string propertyKey)
    {
        return Read(Application.streamingAssetsPath + Constants.PROPERTY_FILE)[propertyKey];
    }

}
