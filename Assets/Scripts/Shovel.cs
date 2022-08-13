using UnityEngine;

public abstract class Shovel : Weapon
{
    public ShovelAttribute shovelAttribute;

    [Header("Interaction attribute")]
    // Sphere radius
    [SerializeField] private float interactionFault;

    private Player _player;

    private void Start()
    {
        _player = Player.Instance;
    }

    protected virtual void Dig()
    {
        var cameraTransform = Camera.main.transform;
        Vector3 playerPos = cameraTransform.position;
        Vector3 playerLook = cameraTransform.forward;

        if (Physics.Raycast(playerPos, playerLook, out RaycastHit hit, shovelAttribute.shovelRange))
        {
            print(hit.transform.gameObject.name);
        }
    }
}

[System.Serializable]
public class ShovelAttribute
{
    public string name;
    public float digDamage;
    public float digSpeed;
    // Length you can dig
    public float shovelRange; 
}
