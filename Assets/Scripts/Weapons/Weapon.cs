using UnityEngine;

public abstract class Weapon : Item
{
    [SerializeField] protected Player _player;

    private void OnEnable()
    {
        _player = Player.instance;

        _player.OnUseItemAction += UseItem;
    }

    private void OnDisable()
    {
        _player.OnUseItemAction -= UseItem;
    }

    public abstract void UseItem(Weapon weapon);
}
