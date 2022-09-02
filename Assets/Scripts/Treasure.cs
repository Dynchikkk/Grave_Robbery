using Project.Architecture;
using UnityEngine;

public class Treasure : BaseMonoBehaviour
{
    public string trasureName;
    public int cost;
    private Player _player;

    private void Awake()
    {
        _player = MainLogic.main.player;
    }

    private void OnEnable()
    {
        _player.OnClickMouseButton += DugOut;
    }

    private void OnDisable()
    {
        _player.OnClickMouseButton -= DugOut;
    }

    public void DugOut()
    {
        GameObject curObj = Player.instance.CheckIfPlayerSee();
        if (curObj == null)
            return;

        if (curObj.TryGetComponent(out Treasure localTreasure))
        {
            _player.OnClickMouseButton -= DugOut;
            _player.AddTreasure(this);
            GetComponent<MeshRenderer>().enabled = false;
            
        }
    }
}
