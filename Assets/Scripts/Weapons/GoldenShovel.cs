public class GoldenShovel : Shovel
{
    public override void UseItem(Weapon weapon)
    {
        if (weapon != this) 
            return;
        print("i use golden shovel");
        Dig();
    }
}
