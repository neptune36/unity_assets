using UnityEngine;

[CreateAssetMenu(fileName = "New void event", menuName = "Game/Event/Void")]
public class VoidEvent : BaseGameEvent<Void>
{
    public void Raise() => Raise(new Void());
}
