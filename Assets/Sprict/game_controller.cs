using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game_controller : MonoBehaviour
{
    public Text text_time;
    public Text text_score;
    public Text text_information;
    public GameObject game_manage;
    public GameObject fish_0;
    public GameObject fish_1;
    public GameObject fish_2;
    public GameObject fish_3;
    public Rigidbody2D rb_boat;
    public GameObject main_ui;
    private GameObject[] fish_table;
    public int score_now;
    private float time_remaining;
    public int time_perlevel;
    public int score_perlevel;
    public GameObject top_left, bot_right;
    private float min_x, min_y, max_x, max_y;
    public int max_score;
    private int total_score;
    private int level;
    private int target_score;
    private bool do_spawn = true;
    private int max_random ;
    // Start is called before the first frame update

    void Start()
    {
        level = data_controller.get_level();
        target_score = score_now + score_perlevel;
        score_now = data_controller.get_score();


        min_x = top_left.transform.position.x;
        max_x = bot_right.transform.position.x;
        min_y = top_left.transform.position.y;
        max_y = bot_right.transform.position.y;
        max_random = 20;
        fish_table = new GameObject[] { fish_0, fish_1 , fish_2 , fish_3 };
        time_remaining = time_perlevel;
        target_score = level * score_perlevel;

        text_information.text = "level#" + level.ToString() +" this level's pass score is: " + target_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (time_remaining <= 0) level_controll();
        if (time_remaining >= time_perlevel) news_controller();
        if(do_spawn) spwan_fish();
        time_control();
        score_control();
 
    }
    void news_controller()
	{
        //Debug.Log("news");
        if(level == 1)
		{
            main_ui.GetComponent<main_ui_controller>().new_feature(1);
        }
        if (level == 2)
        {
            max_random = 90;
            main_ui.GetComponent<main_ui_controller>().new_feature(2);
        }
        if (level == 3)
        {
            max_random = 120;
            main_ui.GetComponent<main_ui_controller>().new_feature(3);
        }
        if (level == 4)
        {
            max_random = 140;
            main_ui.GetComponent<main_ui_controller>().new_feature(4);
        }
    }
    void level_controll()
	{
        if (score_now < target_score)
		{
            //Debug.Log("game end! final score is:" + score_now.ToString());
            text_information.text = "game end! final score is:" + score_now.ToString();
            sent_score();
            SceneManager.LoadScene("End");
            time_remaining = 99999;
		}
		else
		{

            level += 1;
            data_controller.set_level(level);
            data_controller.set_score(score_now);
            SceneManager.LoadScene("main");
        }

	}
    public void sent_score()
	{
        data_controller.set_score(score_now);
    }
    void time_control()
	{
        time_remaining -= Time.deltaTime;
        text_time.text = ((int)time_remaining).ToString();
  
    }
    void spwan_fish()
	{
        int index_fish = Random.Range(0, max_random);
        if (index_fish < 50) index_fish = 0;
        else if (index_fish < 90) index_fish = 1;
        else if (index_fish < 120) index_fish = 2;
        else index_fish = 3;

        
        if (total_score <= max_score)
		{
            //spawn a new fish
            Vector3 new_position = random_position();
            GameObject new_fish =  Instantiate(fish_table[index_fish], new_position, Quaternion.identity);
            //increae currence total score
            if (index_fish == 1) total_score += fish_table[index_fish].GetComponent<fish_controller_topdown>().fish_value;
            else if (index_fish == 2) total_score += fish_table[index_fish].GetComponent<fish_controller_bigsmall>().fish_value;
            else total_score += fish_table[index_fish].GetComponent<fish_controller>().fish_value;

            if (index_fish == 1)
            {
                new_fish.GetComponent<fish_controller_topdown>().rb_boat = rb_boat;
                new_fish.GetComponent<fish_controller_topdown>().game_manage = game_manage;
            }
            else if (index_fish == 2)
            {
                new_fish.GetComponent<fish_controller_bigsmall>().rb_boat = rb_boat;
                new_fish.GetComponent<fish_controller_bigsmall>().game_manage = game_manage;
            } else {
                new_fish.GetComponent<fish_controller>().rb_boat = rb_boat;
                new_fish.GetComponent<fish_controller>().game_manage = game_manage;
            }
		}
		else
		{
            do_spawn = false;
		}
       
    }
    private Vector3 random_position()
	{
        float x_value = Random.Range(min_x, max_x);
        float y_value = Random.Range(min_y, max_y);
        Vector3 new_position = new Vector3(x_value, y_value, 0);
        return new_position;
	}
    void score_control()
    {
       text_score.text = score_now.ToString();
    }
    public void add_score(int new_score)
    {
        score_now += new_score;
        total_score -= new_score;
    }
}
