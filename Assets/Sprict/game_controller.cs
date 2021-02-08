using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class game_controller : MonoBehaviour
{
    public Text text_time;
    public Text text_information;
    public GameObject game_manage;
    public GameObject fish_0;
    public GameObject fish_1;
    public GameObject fish_2;
    public GameObject fish_3;
    public GameObject fish_4;
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
    public int level;
    private int target_score;
    private bool do_spawn;
    private int max_random ;
    private bool is_passui;
    // Start is called before the first frame update

    void Start()
    {
        is_passui = false;
        level = data_controller.get_level();
        score_now = data_controller.get_score();
        buff_control();
        news_controller();
        do_spawn = true;
        min_x = top_left.transform.position.x;
        max_x = bot_right.transform.position.x;
        min_y = top_left.transform.position.y;
        max_y = bot_right.transform.position.y;
        fish_table = new GameObject[] { fish_0, fish_1 , fish_2 , fish_3,fish_4 };
        time_remaining = time_perlevel;
        target_score = level * score_perlevel;

        text_information.text = "level#" + level.ToString() +" this level's pass score is: " + target_score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (time_remaining <= 0 && !is_passui) level_controll();
        if (total_score < 10) do_spawn = true;
        if (do_spawn) spwan_fish();
        time_control();
        if (!is_passui) score_control();
 
    }
    void buff_control()
	{
        //Debug.Log("getting buffs");
        //Debug.Log("\n buff1 is" + data_controller.get_buff(1));
        //Debug.Log("\n buff2 is" + data_controller.get_buff(2));
        //Debug.Log("\n buff3 is" + data_controller.get_buff(3));
		if (data_controller.get_buff(1))
		{
            rb_boat.gameObject.GetComponent<boat_controller>().set_buff(1);
		}
        if (data_controller.get_buff(2))
        {
            rb_boat.gameObject.GetComponent<boat_controller>().set_buff(2);
        }
        if (data_controller.get_buff(3))
        {
            rb_boat.gameObject.GetComponent<boat_controller>().set_buff(3);
        }

    }
    void news_controller()
	{
        //Debug.Log("news"+ level);
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
        if (level == 5)
        {
            max_random = 160;
            main_ui.GetComponent<main_ui_controller>().new_feature(5);
        }
            if (level > 4)
		{
            max_random = 160;
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
            data_controller.set_buff(1, false);
            data_controller.set_buff(2, false);
            data_controller.set_buff(3, false);
            main_ui.GetComponent<main_ui_controller>().set_nextScore((level + 1) * score_perlevel);
            main_ui.GetComponent<main_ui_controller>().pass_level(level,score_now);
            is_passui = true;
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
        else if (index_fish < 140) index_fish = 3;
        else index_fish = 4;

        
        if (total_score <= max_score)
		{
            //spawn a new fish
            Vector3 new_position = random_position();
            GameObject new_fish =  Instantiate(fish_table[index_fish], new_position, Quaternion.identity);
            //increae currence total score
            if (index_fish == 0) total_score += fish_table[index_fish].GetComponent<fish_controller>().fish_value;
            if (index_fish == 1) total_score += fish_table[index_fish].GetComponent<fish_controller_topdown>().fish_value;
            if (index_fish == 2) total_score += fish_table[index_fish].GetComponent<fish_controller_bigsmall>().fish_value;
            if (index_fish == 3) total_score += fish_table[index_fish].GetComponent<fish_controller>().fish_value;
            if (index_fish == 4) total_score += fish_table[index_fish].GetComponent<fish_controller_squid>().fish_value;

            if (index_fish == 0)
            {
                new_fish.GetComponent<fish_controller>().rb_boat = rb_boat;
                new_fish.GetComponent<fish_controller>().game_manage = game_manage;
            }
            if (index_fish == 1)
            {
                new_fish.GetComponent<fish_controller_topdown>().rb_boat = rb_boat;
                new_fish.GetComponent<fish_controller_topdown>().game_manage = game_manage;
            }
            if (index_fish == 2)
            {
                new_fish.GetComponent<fish_controller_bigsmall>().rb_boat = rb_boat;
                new_fish.GetComponent<fish_controller_bigsmall>().game_manage = game_manage;
            } 
            if (index_fish == 3)
            {
                new_fish.GetComponent<fish_controller>().rb_boat = rb_boat;
                new_fish.GetComponent<fish_controller>().game_manage = game_manage;
            }
            if (index_fish == 4)
            {
                new_fish.GetComponent<fish_controller_squid>().rb_boat = rb_boat;
                new_fish.GetComponent<fish_controller_squid>().game_manage = game_manage;
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

        main_ui.GetComponent<main_ui_controller>().update_scoretext();
    }
    public int get_scorenow()
	{
        return score_now;
	}
    public int get_nextscore()
	{
        return target_score;
	}
    public void add_score(int new_score)
    {
        score_now += new_score;
        total_score -= new_score;
    }
}
