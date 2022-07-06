using UnityEngine;

public abstract class SaveableData : ScriptableObject
{
    public abstract void Clear();
    public abstract void PreSave();
    public abstract void PostLoad();

}
