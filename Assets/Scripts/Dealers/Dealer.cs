using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dealer : MonoBehaviour
{
    private MainLogic _main;
    private Player _player;

    public Action<int> OnBuyEvent;
    public Action<int> OnSellEvent;
    public event Action OnInteract;

    [SerializeField] private float sellCoef;
    [SerializeField] private float sellCoefTreasure;
    [SerializeField] private Canvas _dealerCanvas;

    public List<Item> salesSheet = new List<Item>();

    private void Awake()
    {
        _main = MainLogic.main;
        _player = _main.player;
    }

    private void OnEnable()
    {
        _player.OnKeyEInteract += Interact;
    }

    private void OnDisable()
    {
        _player.OnKeyEInteract -= Interact;
    }

    public bool Buy(int num)
    {
        if (salesSheet[num].Cost > _main.money)
        {
            print("Not enough money");
            return false;
        }

        // Получаем номер свободного места в инвентаре игрока
        int lastFreeWeaponPlace = 0;
        for (int i = 0; i < _main.player.weapons.Count; i++)
        {
            if (_main.player.weapons[i] == null)
            {
                lastFreeWeaponPlace = i;
                break;
            }
        }

        bool buy = _player.SetItem(salesSheet[num]);
        salesSheet[num] = null;
        OnBuyEvent?.Invoke(lastFreeWeaponPlace);

        return buy;
    }

    public void SellItem(int num)
    {
        if (num >= _player.weapons.Count)
            return;

        if (_player.weapons[num] == null)
            return;

        Weapon copy = Instantiate(_player.weapons[num]);
        salesSheet.Add(copy);

        OnSellEvent?.Invoke(salesSheet.Count - 1);

        _main.money += Convert.ToInt32(_player.weapons[num].Cost / sellCoef);

        Destroy(_player.weapons[num].gameObject);
        _player.weapons[num] = null;
    }

    public void SellTreasure(int num)
    {
        if (num >= _player.playerTreasures.Count)
            return;

        if (_player.playerTreasures[num] == null)
            return;

        _main.money += Convert.ToInt32(_player.playerTreasures[num].cost * sellCoefTreasure);
        _player.playerTreasures[num] = null;
    }

    public void Interact()
    {
        GameObject curObj = Player.instance.CheckIfPlayerSee();
        if (curObj == null)
            return;

        if (curObj.TryGetComponent(out Dealer localDealer))
        {
            _main.sceneAndCanvasManager.FlipCanvas(_main.mainCanvas, _dealerCanvas);
            _main.NoPause(false);
            OnInteract?.Invoke();
        }
    }
}
