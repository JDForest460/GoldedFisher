using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class end_controller : MonoBehaviour
{
    public Text text_score;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
       score = data_controller.get_score();
       text_score.text = " Thanks for playing \n your final score is:" + score.ToString();


    }
    public void retry()
	{
        //bug.Log("retury");
        data_controller.set_score(0);
        data_controller.set_level(1);
        data_controller.set_buff(1, false);
        data_controller.set_buff(2, false);
        data_controller.set_buff(3, false);
        SceneManager.LoadScene("main");
    }
    public void QuitGame()
	{
        Application.Quit();
	}

}
