using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class game_controller : MonoBehaviour
{
    public Text text_time;
    public GameObject fish_0;
    public GameObject fish_1;
    public GameObject fish_2;
    public GameObject fish_3;
    public Rigidbody2D rb_boat;
    private GameObject[] fish_table;
    public float time_remaining;
    public float min_x, min_y, max_x, max_y;
    private int total_score = 0;
    // Start is called before the first frame update

    void Start()
    {

        fish_table = new GameObject[] { fish_0, fish_1 , fish_2 , fish_3 };
    }

    // Update is called once per frame
    void Update()
    {
        time_control();
        spwan_fish();
    }
    void time_control()
	{
        if (time_remaining > 0)
        {
            time_remaining -= Time.deltaTime;
            text_time.text = ((int)time_remaining).ToString();
        }
    }
    void spwan_fish()
	{
        int index_fish = Random.Range(0, 140);
        if (index_fish < 20) index_fish = 3;
        else if (index_fish < 50) index_fish = 2;
        else if (index_fish < 90) index_fish = 1;
        else index_fish = 0;
        if (index_fish == 1) total_score += fish_table[index_fish].GetComponent<fish_controller_topdown>().fish_value;
        else if (index_fish == 2) total_score += fish_table[index_fish].GetComponent<fish_controller_bigsmall>().fish_value;
        else total_score += fish_table[index_fish].GetComponent<fish_controller>().fish_value;
        if (total_score <= 50)
		{
            Vector3 new_position = random_position();
            GameObject new_fish =  Instantiate(fish_table[index_fish], new_position, Quaternion.identity);
            if (index_fish == 1) new_fish.GetComponent<fish_controller_topdown>().rb_boat = rb_boat;
            else if (index_fish == 2) new_fish.GetComponent<fish_controller_bigsmall>().rb_boat = rb_boat;
            else   new_fish.GetComponent<fish_controller>().rb_boat = rb_boat;
        }
       
    }
    private Vector3 random_position()
	{
        float x_value = Random.Range(min_x, max_x);
        float y_value = Random.Range(min_y, max_y);
        Vector3 new_position = new Vector3(x_value, y_value, 0);
        return new_position;
	}
}
