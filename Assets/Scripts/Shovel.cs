
public abstract class Shovel : Weapon
{
    public ShovelAttribute shovelAttribute;
}

[System.Serializable]
public class ShovelAttribute
{
    public string name;
    public float digDamage;
    public float digSpeed;
}
