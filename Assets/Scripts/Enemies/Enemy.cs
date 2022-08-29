using System.Collections;
using System.Collections.Generic;
using Project.Architecture;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : BaseMonoBehaviour
{
    [SerializeField] private float _viewAngle;
    [SerializeField] private float _viewDistanse;
    [SerializeField] private float _speed;
    [SerializeField] private float _stoppingDistance;

    private Player _player;
    private MainLogic _main;
    private NavMeshAgent _agent;

    private GameObject _currentTarget;

    private int _curPointNum = 0;
    private List<GameObject> _checkpoints = new();


    private void Awake()
    {
        _main = MainLogic.main;
        _player = Player.instance;
        _agent = GetComponent<NavMeshAgent>();
        _main.allEnemies.Add(this);
        //_checkpointParent = GetComponentInParent<EnemiesPoint>();

        SetStartCharacteristic();
    }

    private void Update()
    {
        SetCurrentTarget();
        CheckPlayerInFieldOfVision();
    }

    private void SetStartCharacteristic()
    {
        _agent.speed = _speed;
    }

    // Не находится ли игрок в поле зрения
    private void CheckPlayerInFieldOfVision()
    {
        if (Vector3.Angle(transform.forward, _player.transform.position - transform.position) <= _viewAngle)
        {
            RaycastHit hit;
            Vector3 pos = transform.position;
            Vector3 plPos = _player.transform.position - transform.position;

            if (Physics.Raycast(pos, plPos, out hit, _viewDistanse))
            {
                if (hit.transform.CompareTag("Player") is false)
                    return;

                print("see player");
                _currentTarget = _player.gameObject;
            }
            else
            {
                ChoosePoint();
            }
        }
        else
        {
            ChoosePoint();
        }
    }

    // Определяем цель
    private void SetCurrentTarget()
    {
        if (_currentTarget == null)
            ChoosePoint();

        if (Vector3.Distance(transform.position, _currentTarget.transform.position) <= _stoppingDistance)
        {
            if (_currentTarget.CompareTag("Player"))
            {
                print("busted");
                MainLogic.main.winLoose.Lose();
            }
            else
            {
                _curPointNum += 1;
            }
        }

        _agent.SetDestination(_currentTarget.transform.position);
    }

    // Смотри на текущую точку по маршруту
    public void ChoosePoint()
    {
        if (_curPointNum >= _checkpoints.Count)
            _curPointNum = 0;

        _currentTarget = _checkpoints[_curPointNum];
    }

    public void SetPoints(List<GameObject> points)
    {
        for (int i = 0; i < points.Count; i++)
        {
            _checkpoints.Add(points[i]);
        }
    }
}
