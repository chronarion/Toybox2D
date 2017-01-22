using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ToyController : MonoBehaviour {
    private Rigidbody2D rb;
  
    public Toybox.direction release_direction { get; set; }
    public bool reverse_sprite = false;
    public AnimationCurve powerCurve;

    private Animator animator;
    
    public Transform parent { get; set; }

    protected abstract float DECAY_LIFETIME { get; }
    protected abstract float MAX_VELOCITY { get; }
    protected abstract float FULL_POWER { get; }
    /// <summary>
    /// higher winds down faster
    /// </summary>
    protected abstract float WIND_DOWN_RATE { get; }    

    private const string ANIMATION_STATE = "state";
    private const int STATE_IDLE = 0;
    private const int STATE_HIT = 1;
    private const int STATE_RUN_RIGHT = 2;
    private const int STATE_RUN_LEFT = 3;
    private const int STATE_STOPPED = 4;

    private ToyMode mode;
    /// <summary>
    /// max is 100
    /// </summary>
    private float power;
    private float stopTime;
 

    enum ToyMode
    {
        CHARGING,
        RUNNING,
        STOPPED
    }

    void LateUpdate()
    {
        if (reverse_sprite)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.flipX = true;            
        }
    }

    void Awake()
    {
             
    }
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
      
        animator = GetComponent<Animator>();

        ChargeToy();
        
    }

    public void ChargeToy()
    {
        if (parent)
        {
            transform.position = parent.transform.position;
            transform.parent = parent;
        }
        mode = ToyMode.CHARGING;
        rb.simulated = false;                        
    }
    /// <summary>
    /// release toy
    /// </summary>
    /// <param name="releasepower">max is 100, min is 0</param>
    public void ReleaseToy(float releasepower, Vector3 location)
    {        
        mode = ToyMode.RUNNING;
        power = releasepower;
        rb.simulated = true;
        transform.parent = null;
        transform.position = location;   
    }

    // Update is called once per frame
    void Update()
    {
        float timeSinceInitialization = Time.timeSinceLevelLoad - stopTime;
        
        if (mode == ToyMode.STOPPED && timeSinceInitialization > DECAY_LIFETIME)
        {
            Destroy(this.gameObject);            
        }

        if (mode == ToyMode.CHARGING)
        {
            animator.SetInteger(ANIMATION_STATE, STATE_IDLE);
        }

        if (mode == ToyMode.STOPPED)
        {
            animator.SetInteger(ANIMATION_STATE, STATE_STOPPED);
        }
    }

    public void DestroyAttachedToy()
    {
        Destroy(this.gameObject);
    }

    void FixedUpdate() {        
     
        if (mode == ToyMode.RUNNING)
        {
            float result_power = powerCurve.Evaluate(power / 100f);
            result_power*=FULL_POWER;
            result_power*=Time.deltaTime;            

            power -= WIND_DOWN_RATE * Time.deltaTime;
            if (power <= 0)
            {
                power = 0;
                StopToy();
            }

            if (release_direction == Toybox.direction.RIGHT)
            {
                animator.SetInteger(ANIMATION_STATE, STATE_RUN_RIGHT);
                rb.AddForce(new Vector2(result_power, 0));
            }
            else if (release_direction == Toybox.direction.LEFT)
            {
                animator.SetInteger(ANIMATION_STATE, STATE_RUN_LEFT);
                rb.AddForce(new Vector2(result_power * -1, 0));
            }
           
            if (rb.velocity.x > MAX_VELOCITY)
            {
                rb.velocity = new Vector2(MAX_VELOCITY, rb.velocity.y);
            }
        }
       
    }

    private void StopToy()
    {
        mode = ToyMode.STOPPED;
        stopTime = Time.timeSinceLevelLoad;
    }
}
