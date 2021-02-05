
public static class data_controller
{
	private static int score = 0;
	private static int level = 1;
	public static bool buff_speed;
	public static bool buff_size;
	public static bool buff_power;
	public static int get_score()
	{
		return score;
	}
	public static void set_score(int new_socre)
	{
		score = new_socre;
	}
	public static int get_level()
	{
		return level;
	}
	public static void set_level(int new_level)
	{
		level = new_level;
	}
	public static bool get_buff(int which)
	{
		if (which == 1)
		{
			return buff_speed;
		}
		if (which == 2)
		{
			return buff_size;
		}
		if (which == 3)
		{
			return buff_power;
		}
		else
		{
			return false;
		}
	}
	public static void set_buff(int which,bool how)
	{
		if (which == 1)
		{
			buff_speed = how;
		}
		if (which == 2)
		{
			buff_size = how;
		}
		if (which == 3)
		{
			buff_power = how;
		}
	}
}
