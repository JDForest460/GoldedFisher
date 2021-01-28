using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class boat_controller : MonoBehaviour
{

    public Rigidbody2D rb_bow;
    public Rigidbody2D rb_hook;
    public Transform hook_top;
    public LineRenderer fish_line;
    public Text ui_score;
    private int score = 0;

    public float bow_rotate_speed;
    public float bow_shot_speed;
    public float line_len;
    private bool go_right = true;
    private bool stop_rotate = false;
    private bool is_shot = false;
    private bool is_pull = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        score_control();
        draw_line();
        if(!stop_rotate) rotate_bow();
        shot_hook();
    }
    void rotate_bow()
	{
        float angel_now = Vector3.Angle(rb_bow.transform.up, Vector3.right);
        // Debug.Log(angel_now);


        if (go_right)
		{
            if (angel_now <= 170)
			{
                Vector3 angel_next = new Vector3(0f, 0f, bow_rotate_speed);
                rb_bow.transform.Rotate(angel_next * Time.deltaTime);
			}
			else
			{
                go_right = false;
			}
              
		}
		else
		{
            if (angel_now >= 10)
            {
                Vector3 angel_next = new Vector3(0f, 0f, -bow_rotate_speed);
                rb_bow.transform.Rotate(angel_next * Time.deltaTime);
            }
            else
            {
                go_right = true;
            }
        }
         
	}
    void draw_line()
	{
        fish_line.SetPosition(0, hook_top.position);
        fish_line.SetPosition(1, rb_hook.transform.position);
    }
    void shot_hook()
	{
        Vector3 position_before_shoot = new Vector3();
        if (!is_shot && !is_pull)
		{
            if (Input.GetMouseButtonDown(0))
            {
                //Debug.Log("click left mouse");
                stop_rotate = true;
                is_shot = true;
                position_before_shoot = rb_hook.transform.localPosition;
            }
        }

        
        if (is_shot)
        {
 
            if (Vector3.Distance(hook_top.position, rb_hook.position) <= line_len)
            {
                rb_hook.transform.position += -rb_hook.transform.up * bow_shot_speed * Time.deltaTime;
			}
			else
			{
                is_shot = false;
                is_pull = true;
			}
            
		}
		if(is_pull)
		{
            
            //Debug.Log(rb_hook.transform.localPosition.y);
            if (Vector3.Distance(hook_top.position, rb_hook.position) >= 0.1 && rb_hook.transform.localPosition.y <=0)
            {
                
                rb_hook.transform.position += rb_hook.transform.up * bow_shot_speed * Time.deltaTime;
			}
			else
			{
                rb_hook.transform.localPosition = position_before_shoot;
                is_pull = false;
                stop_rotate = false;
			}


        }
	}
    public bool get_pullstate()
	{
        return is_pull;
	}
    void score_control()
	{
        ui_score.text = score.ToString();
    }
    public void add_score(int new_score)
	{
        score += new_score;
    }
}
