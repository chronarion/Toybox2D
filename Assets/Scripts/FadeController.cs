using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour {
    public enum FadeDirection
    {
        FADE_IN,
        FADE_OUT,
        STATIC
    }
    private FadeDirection direction = FadeDirection.STATIC;
    public const float FADE_TIME_SECONDS = 3f;
    public const float FADE_TIME_FACTOR = 1 / (FADE_TIME_SECONDS);
    private Color FADE_COLOR = Color.black;

    private float elapsed_lerp_t;
    

	// Use this for initialization
	void Start () {
		
	}

    public void StartFadeOut()
    {
        direction = FadeDirection.FADE_OUT;
        elapsed_lerp_t = 0;
    }

    public void StartFadeIn()
    {
        direction = FadeDirection.FADE_OUT;
        elapsed_lerp_t = 0;
    }

    // Update is called once per frame
    void Update() {

        SpriteRenderer sr = GetComponent<SpriteRenderer>();        
        if (direction == FadeDirection.FADE_OUT) {
            elapsed_lerp_t += Time.deltaTime * FADE_TIME_FACTOR;
            sr.color = Color.Lerp(FADE_COLOR, Color.clear, elapsed_lerp_t);            
        }

        if (direction == FadeDirection.FADE_IN)
        {
            elapsed_lerp_t += Time.deltaTime * FADE_TIME_FACTOR;
            sr.color = Color.Lerp(Color.clear, FADE_COLOR, elapsed_lerp_t);
        }

        if (elapsed_lerp_t >= 1)
        {
            elapsed_lerp_t = 0;
            direction = FadeDirection.STATIC;
        }
    }
}
