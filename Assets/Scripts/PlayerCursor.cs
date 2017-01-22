using System;
using ProgressBar;
using UnityEngine;

public class PlayerCursor : MonoBehaviour {
    public Rigidbody2D toy1;
    public Rigidbody2D toy2;

    private Rigidbody2D toy_selected;

    public string Player_Vertical_Axis;
    public string Player_Horizontal_Axis;
    public string Player_Fire_Button;
    public Toybox.direction toy_direction;
    public bool sprite_faces_left;

    public GameObject powerBar;
    public GameObject chargingToyHolder;
 
    private ToyController chargingToy;

    private Vector2 originalCursorPos;
    private Vector2 targetCursorPos;
    private float movementProgress;

    private bool vertical_isAxisInUse;
    private bool horizontal_isAxisInUse;
    
    private const float REPEAT_DELAY = 0.07f;   
    private const float CURSOR_SPEED = 10;
   
    private float movementBuffer;
    private float charge_power;
    private const float CHARGE_TIME = 5f;
    private const float CHARGE_RATE = 100 / CHARGE_TIME;


    // Use this for initialization
    void Start () {
        originalCursorPos = transform.position;
        targetCursorPos = transform.position;
        movementProgress = 1;
        vertical_isAxisInUse = false;
        horizontal_isAxisInUse = false;
        movementBuffer = 0;
        toy_selected = toy1;
    }
		       
    void FixedUpdate()
    {        
               
    }

    void Update()
    {
        HandleCursorMovement();
        Charge();

        if (Input.GetButtonDown(Player_Fire_Button))
        {
            releaseToy();
        }
      
        // Note this handles the case when after we init there is no toy!
        if (chargingToy == null)
        {            
            chargingToy = attachNewToy(chargingToyHolder.transform);
        }

        HandleToySwitching();
    }

    private void HandleToySwitching()
    {
        float direction = Input.GetAxisRaw(Player_Horizontal_Axis);
        if (direction == -1 && !horizontal_isAxisInUse)
        {
            nextToy();
            horizontal_isAxisInUse = true;
        }
        else if (direction == 1 && !horizontal_isAxisInUse)
        {
            nextToy();
            horizontal_isAxisInUse = true;
        }
        else if (direction == 0)
        {
            horizontal_isAxisInUse = false;
        }
    }

    private void nextToy()
    {
       Debug.Log("NEXT");
       if (toy_selected == toy1)
        {
            toy_selected = toy2;
            
        } else
        {
            toy_selected = toy1;
        }

        
        chargingToy.DestroyAttachedToy();
        chargingToy = null;
    }

    private ToyController attachNewToy(Transform toyParent)
    {
        Rigidbody2D newToyBody = Instantiate(toy_selected, toyParent);        
        ToyController newToy = newToyBody.GetComponent<ToyController>();
        newToy.parent = toyParent;
        newToy.release_direction = toy_direction;

        if ((toy_direction == Toybox.direction.LEFT && !sprite_faces_left)
            || (toy_direction == Toybox.direction.RIGHT && sprite_faces_left))
        {
            newToy.reverse_sprite = true;
        }
        return newToy;
    }

    private void releaseToy()
    {
        // release under cursor, so take transform.position
        chargingToy.ReleaseToy(charge_power, this.transform.position);
        charge_power = 0;
        chargingToy = null;
        AudioSource release_sound = GetComponent<AudioSource>();
        release_sound.Play();
    }

    private void Charge()
    {
        charge_power += CHARGE_RATE * Time.deltaTime;
        if (charge_power >= 100) charge_power = 100;
        ProgressRadialBehaviour pb = powerBar.GetComponent<ProgressRadialBehaviour>();
        pb.Value = charge_power;
    }

    private void HandleCursorMovement()
    {
        if (movementProgress >= 1)
        {
            float direction = Input.GetAxisRaw(Player_Vertical_Axis);
            if (direction != 0 && (!vertical_isAxisInUse || movementBuffer <= 0))
            {
                vertical_isAxisInUse = true;
                originalCursorPos = transform.position;
                targetCursorPos = new Vector2(transform.position.x, transform.position.y + (direction));
                if (targetCursorPos.y < Toybox.MIN_Y) targetCursorPos.y = Toybox.MIN_Y;
                if (targetCursorPos.y > Toybox.MAX_Y) targetCursorPos.y = Toybox.MAX_Y;
                movementProgress = 0;
            }
            if (direction == 0)
            {
                vertical_isAxisInUse = false;
            }
        }

        float delta = Time.deltaTime * CURSOR_SPEED;
        movementProgress += delta;
        movementBuffer -= Time.deltaTime;
        // flag arrival
        if (movementProgress > 1 && originalCursorPos != targetCursorPos)
        {
            movementProgress = 1;
            originalCursorPos = targetCursorPos;
            movementBuffer = REPEAT_DELAY; // incur delay for repeat
        }
        transform.position = Vector2.Lerp(originalCursorPos, targetCursorPos, movementProgress);
    }
}
