public class GoldenShovel : Shovel
{
    public override void UseItem(Weapon weapon)
    {
        if (weapon == this)
            print("i use golden shovel");
    }
}
