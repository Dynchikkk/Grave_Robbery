using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public class EnemiesPoint : BaseMonoBehaviour
{
    [field: SerializeField] public List<GameObject> Checkpoints { get; private set; }
}
