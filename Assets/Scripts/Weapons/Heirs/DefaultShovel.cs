public class DefaultShovel : Shovel
{
	public override void UseItem(Weapon weapon)
	{
		if (weapon != this) 
			return;
		print("i use default shovel");
		Dig();
	}
}
