using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class main_ui_controller : MonoBehaviour
{
    public GameObject boat;
    public GameObject game_manage;
    public Button button_puse;
    public Button button_resue;
    public Button button_quit;
    public GameObject main_ui;
    public GameObject ui_button_puse;
    public GameObject ui_menu;
    public GameObject plane_passlevel;
    public Transform news_poistion;
    public GameObject news_BlueFish;
    public GameObject news_RedFish;
    public GameObject news_PufferFish;
    public GameObject news_swordFish;
    public GameObject news_squidFish;
    public Button button_nextlevel;
    public Button buy1, buy2, buy3;
    public Image image_good1,image_good2,image_good3;
    public GameObject ui_text;
    public Text text_scoreleft;
    public Text text_ScoreNextLevel;
    public Text text_score;
    public Text text_level;
    public Sprite Sprite_sold1;
    public Sprite Sprite_sold2;
    public Sprite Sprite_sold3;
    public GameObject plane_NoEnoughScore;
    public Button button_NoEnoughScore;
    public Slider slider_force;
    private string news_name;
    // Start is called before the first frame update
    void Start()
    {
        //new_feature(1);
        text_level.text = data_controller.get_level().ToString();
        ui_menu.SetActive(false);
        button_puse.onClick.AddListener(click_puse);
        button_resue.onClick.AddListener(click_resue);
        button_quit.onClick.AddListener(click_quit);
        button_nextlevel.onClick.AddListener(click_Next_level);
        buy1.onClick.AddListener(click_buy1);
        buy2.onClick.AddListener(click_buy2);
        buy3.onClick.AddListener(click_buy3);
        button_NoEnoughScore.onClick.AddListener(click_close);
    }

    // Update is called once per frame
    void Update()
    {
        set_SliderForce();
    }
    public void pass_level(int level,int score_now)
	{

        Time.timeScale = 0;
        ui_text.SetActive(false);
        boat.SetActive(false);
        boat.GetComponent<boat_controller>().set_puse(true);
        level += 1;
        data_controller.set_level(level);
        data_controller.set_score(score_now);
        update_score();
        plane_passlevel.SetActive(true);
        

    }
    public void set_SliderForce()
	{
        slider_force.value = boat.GetComponent<boat_controller>().get_line_len() / boat.GetComponent<boat_controller>().get_max_line_len();

    }
    private void click_close()
	{
        plane_NoEnoughScore.SetActive(false);
	}
    private void click_Next_level()
    {
        // Debug.Log("next level");
        ui_text.SetActive(true);
        boat.GetComponent<boat_controller>().set_puse(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("main");
    }
    private void click_puse()
    {
        //Debug.Log("puse");
        ui_button_puse.SetActive(false);
        ui_menu.SetActive(true);
        boat.GetComponent<boat_controller>().set_puse(true);
        Time.timeScale = 0;
    }
    private void click_resue()
    {
        //Debug.Log("resue");
        ui_button_puse.SetActive(true);
        ui_menu.SetActive(false);
        boat.GetComponent<boat_controller>().set_puse(false);
        Time.timeScale = 1;
    }
    private void click_quit()
    {
        //Debug.Log("quit");
        game_manage.GetComponent<game_controller>().sent_score();
        boat.GetComponent<boat_controller>().set_puse(false);
        Time.timeScale = 1;
        SceneManager.LoadScene("End");
    }
    private void click_continue()
    {
        //Debug.Log("continue");
        ui_button_puse.SetActive(true);
        boat.GetComponent<boat_controller>().set_puse(false);
        Time.timeScale = 1;
        Destroy(main_ui.transform.Find(news_name).gameObject);
    }
    private void update_score()
	{
        text_scoreleft.text = data_controller.get_score().ToString();

    }
    private void click_buy1()
    {
		if (!data_controller.get_buff(1) && data_controller.get_score() > 10)
		{
            data_controller.set_buff(1, true);
            data_controller.set_score(data_controller.get_score() - 10);
            image_good1.sprite = Sprite_sold1;
            update_score();
		}else
		{
            plane_NoEnoughScore.SetActive(true);
            Debug.Log("cant buy");
		}

    }
    private void click_buy2()
    {
        if (!data_controller.get_buff(2) && data_controller.get_score() > 10)
        {
            data_controller.set_buff(2, true);
            data_controller.set_score(data_controller.get_score() - 10);
            image_good2.sprite = Sprite_sold2;
            update_score();
        }
        else
        {
            plane_NoEnoughScore.SetActive(true);
            Debug.Log("cant buy");
        }
    }
    private void click_buy3()
    {
        if (!data_controller.get_buff(3) && data_controller.get_score() > 10)
        {
            data_controller.set_buff(3, true);
            data_controller.set_score(data_controller.get_score() - 10);
            image_good3.sprite = Sprite_sold3;
            update_score();
        }
        else
        {
            plane_NoEnoughScore.SetActive(true);
            Debug.Log("cant buy");
        }
    }
    public void update_scoretext()
	{
        text_score.text = game_manage.GetComponent<game_controller>().get_scorenow().ToString() + "/" + game_manage.GetComponent<game_controller>().get_nextscore().ToString();
    }
    public void new_feature(int which)
	{
        Time.timeScale = 0;
        ui_button_puse.SetActive(false);
        //Debug.Log("news!!!");
        boat.GetComponent<boat_controller>().set_puse(true);
        if(which == 1)
		{
            news_name = "news_BlueFish(Clone)";
            GameObject new_feature = Instantiate(news_BlueFish, news_poistion.position, Quaternion.identity);
            new_feature.transform.SetParent(main_ui.transform,false);
            Button new_continue_button = new_feature.GetComponentInChildren<Button>();
            new_continue_button.onClick.AddListener(click_continue);
        }
        if(which == 2)
		{
            news_name = "news_RedFish(Clone)";
            GameObject new_feature = Instantiate(news_RedFish, news_poistion.position, Quaternion.identity);
            new_feature.transform.SetParent(main_ui.transform,false);
            Button new_continue_button = new_feature.GetComponentInChildren<Button>();
            new_continue_button.onClick.AddListener(click_continue);
        }
        if (which == 3)
        {
            news_name = "news_PufferFish(Clone)";
            GameObject new_feature = Instantiate(news_PufferFish, news_poistion.position, Quaternion.identity);
            new_feature.transform.SetParent(main_ui.transform,false);
            Button new_continue_button = new_feature.GetComponentInChildren<Button>();
            new_continue_button.onClick.AddListener(click_continue);
        }
        if (which == 4)
        {
            news_name = "news_SwordFish(Clone)";
            GameObject new_feature = Instantiate(news_swordFish, news_poistion.position, Quaternion.identity);
            new_feature.transform.SetParent(main_ui.transform,false);
            Button new_continue_button = new_feature.GetComponentInChildren<Button>();
            new_continue_button.onClick.AddListener(click_continue);
        }
        if (which == 5)
        {
            news_name = "news_SquidFish(Clone)";
            GameObject new_feature = Instantiate(news_squidFish, news_poistion.position, Quaternion.identity);
            new_feature.transform.SetParent(main_ui.transform, false);
            Button new_continue_button = new_feature.GetComponentInChildren<Button>();
            new_continue_button.onClick.AddListener(click_continue);
        }

    }
    public void set_nextScore(int i)
	{
        text_ScoreNextLevel.text = i.ToString();
	}
}
