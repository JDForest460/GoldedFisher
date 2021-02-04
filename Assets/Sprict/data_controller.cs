
public static class data_controller
{
	private static int score = 0;
	private static int level = 1;

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
}
