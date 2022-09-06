using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    private MainLogic _main;
    private Player _player;

    [SerializeField] private float sellCoef;
    [SerializeField] private float sellCoefTreasure;

    public List<Item> salesSheet = new List<Item>();

    private void Awake()
    {
        _main = MainLogic.main;
        _player = _main.player;
    }

    public void Buy(Item item)
    {
        if (item.Cost > _main.money)
        {
            print("Not enough money");
            return;
        }

        if (_player.weapons[-1] != null)
        {
            print("Not enough place");
            return;
        }

        for (int i = 0; i < _player.weapons.Count; i++)
        {
            if (_player.weapons[i] == null)
            {
                _player.weapons[i] = (Weapon)item;
                _main.money -= item.Cost;
                break;
            }
        }
    }

    public void SellItem(int num)
    {
        if (num >= _player.weapons.Count)
            return;

        if (_player.weapons[num] == null)
            return;

        _main.money += System.Convert.ToInt32(_player.weapons[num].Cost / sellCoef);
        _player.weapons[num] = null;   
    }

    public void SellTreasure(int num)
    {
        if (num >= _player.playerTreasures.Count)
            return;

        if (_player.playerTreasures[num] == null)
            return;

        _main.money += System.Convert.ToInt32(_player.playerTreasures[num].cost * sellCoefTreasure);
        _player.playerTreasures[num] = null;
    }
}
