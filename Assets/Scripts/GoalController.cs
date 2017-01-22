using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour {
    public GameObject player;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.tag.Equals("toy"))
        {
            Destroy(other.gameObject);

            PlayerController pc = player.GetComponent<PlayerController>();            
            pc.AddScore();

            AudioSource asource = GetComponent<AudioSource>();
            asource.Play();
        }
    }
}
