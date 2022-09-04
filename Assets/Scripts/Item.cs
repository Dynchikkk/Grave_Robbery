using Project.Architecture;
using UnityEngine;

public abstract class Item : BaseMonoBehaviour
{
    [SerializeField] public int Cost { get; protected set; }
}
