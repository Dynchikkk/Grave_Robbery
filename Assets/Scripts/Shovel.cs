using UnityEngine;

public abstract class Shovel : Weapon
{
    public ShovelAttribute shovelAttribute;

    [HideInInspector]
    public float digCd;

    [Header("Interaction attribute")]
    // Sphere radius
    [SerializeField] private float interactionFault;

    private void Start()
    {
        digCd = shovelAttribute.digSpeed;
    }

    protected virtual void Update()
    {
        digCd -= Time.deltaTime;
    }

    protected virtual void Dig()
    {
        if (digCd > 0)
            return;

        digCd = shovelAttribute.digSpeed;

        var cameraTransform = Camera.main.transform;
        Vector3 playerPos = cameraTransform.position;
        Vector3 playerLook = cameraTransform.forward;

        if (Physics.Raycast(playerPos, playerLook, out RaycastHit hit, shovelAttribute.shovelRange))
        {
            if (hit.transform.gameObject.TryGetComponent(out Grave grave))
            {
                grave.TakeDamage(shovelAttribute.digDamage);
            }
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
