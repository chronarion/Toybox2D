using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public int score = 0;
    public string player_name;

    // Use this for initialization
    void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
        Text text = this.GetComponent<Text>();
        text.text = "" + score;
    }

    public void AddScore()
    {
        score++;
    }
}
