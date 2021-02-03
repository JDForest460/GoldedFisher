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
    public Transform puse_top_left;
    public Transform puse_bot_right;
    public float bow_rotate_speed;
    public float bow_shot_speed;
    public float line_len;
    private bool go_right = true;
    private bool stop_rotate = false;
    private bool is_shot = false;
    private bool is_pull = false;
    private float top_left_x;
    private float top_left_y;
    private float bot_right_x;
    private float bot_right_y;
    private bool is_puse;
    // Start is called before the first frame update
    void Start()
    {
        top_left_x = puse_top_left.position.x;
        top_left_y = puse_top_left.position.y;
        bot_right_x = puse_bot_right.position.x;
        bot_right_y = puse_bot_right.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(!is_puse)
		{
            draw_line();
            if (!stop_rotate) rotate_bow();
            shot_hook();
        }
 
    }
    bool mouse_isinbutton()
	{
        Vector2 mouse_position = Input.mousePosition;
        if(mouse_position.x > top_left_x && mouse_position.x < bot_right_x && mouse_position.y < top_left_y && mouse_position.y > bot_right_y)
		{
            return true;
		}
		else
		{
            return false;
		}
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
				if (!mouse_isinbutton())
				{
                    stop_rotate = true;
                    is_shot = true;
                    position_before_shoot = rb_hook.transform.localPosition;
				}
				else
				{
                    Debug.Log("in button!!");
                }
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
                stop_shoot();
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
    public void stop_shoot()
	{
        is_shot = false;
        is_pull = true;
	}
    public bool get_ispull()
	{
        return is_pull;
	}
    public void set_puse(bool i)
	{
        is_puse = i;
	}
}
