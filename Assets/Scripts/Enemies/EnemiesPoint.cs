using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Architecture;

public class EnemiesPoint : BaseMonoBehaviour
{
    [SerializeField] private List<Points> _checkpoints = new();

    private void OnEnable()
    {
        MainLogic.main.OnLevelSet += SetCheckPointsToEnemy;
    }

    private void OnDisable()
    {
        MainLogic.main.OnLevelSet -= SetCheckPointsToEnemy;
    }

    private void SetCheckPointsToEnemy()
    {
        MainLogic main = MainLogic.main;
        for (int i = 0; i < main.allEnemies.Count; i++)
        {
            var curEn = main.allEnemies[i];
            for (int j = 0; j < _checkpoints.Count; j++)
            {
                var curPoint = _checkpoints[j];
                if (curEn.name.Replace("(Clone)", "") == curPoint.enemy.name && curPoint.busy is false)
                {
                    curEn.SetPoints(curPoint.checkpoints);
                    curPoint.busy = true;
                    break;
                }
            }
        }
    }
}

[System.Serializable]
public class Points
{
    public Enemy enemy;
    public bool busy = false;
    public List<GameObject> checkpoints = new();
}
