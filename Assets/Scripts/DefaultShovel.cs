public class DefaultShovel : Shovel
{
    public override void UseItem(Weapon weapon)
    {
        if (weapon == this)
            print("i use default shovel");
    }
}
