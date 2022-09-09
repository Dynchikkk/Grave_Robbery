using Project.Architecture;
using UnityEngine;

public abstract class Item : BaseMonoBehaviour
{
    [field: SerializeField] public int Cost { get; protected set; }
    public Sprite icon;
}
