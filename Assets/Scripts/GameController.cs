using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    public GameObject black_screen;
    public GameObject player_1_object;
    public GameObject player_2_object;
    public GameObject win_panel;

    private const int MAX_SCORE = 10;
    private PlayerController p1;
    private PlayerController p2;
    

    // Use this for initialization
    void Start () {
        GameObject o = Instantiate(black_screen);
        FadeController f = o.GetComponent<FadeController>();
        f.StartFadeOut();
        p1 = player_1_object.GetComponent<PlayerController>();
        p2 = player_2_object.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        Text winner_text = win_panel.GetComponent<Text>();        

        if (p1.score >= MAX_SCORE)
        {
            win_panel.SetActive(true);
            winner_text.text = p1.player_name + " wins!";
        }
        else if (p2.score >= MAX_SCORE)
        {
            win_panel.SetActive(true);
            winner_text.text = p2.player_name + " wins!";
        }
	}
}
