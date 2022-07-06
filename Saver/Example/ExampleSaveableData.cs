using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Example saveable data", menuName = "Rudy/Saver/Example saveable data")]
public class ExampleSaveableData : SaveableData
{
    public string name;
    public int quantity;

    public override void Clear()
    {
        name = "data cleared";
        quantity = 0;
    }

    public override void PostLoad()
    {
        name = "data loaded";
    }

    public override void PreSave()
    {
        name = "data saved";
        quantity++;
    }
}
