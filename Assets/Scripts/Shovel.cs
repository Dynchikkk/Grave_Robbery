using UnityEngine;

public abstract class Shovel : Weapon
{
	public ShovelAttribute shovelAttribute;
	
	public float DigCd { get; protected set; }
	
	[Header("Interaction attribute")]
	// Sphere radius
	[SerializeField] protected float _interactionFault;

	private void Start()
	{
		DigCd = shovelAttribute.digSpeed;
	}

	protected virtual void Update()
	{
		DigCd -= Time.deltaTime;
	}

	protected virtual void Dig()
	{
		if (DigCd > 0)
			return;

		DigCd = shovelAttribute.digSpeed;

		var cameraTransform = Camera.main.transform;
		Vector3 playerPos = cameraTransform.position;
		Vector3 playerLook = cameraTransform.forward;

		if (Physics.Raycast(playerPos, playerLook, out RaycastHit hit, shovelAttribute.shovelRange))
		{
			if (hit.transform.gameObject.TryGetComponent(out EarthLayer earthLayer))
			{
                earthLayer.TakeDamage(shovelAttribute.digDamage);
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
