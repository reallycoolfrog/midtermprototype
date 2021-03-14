using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetCat : MonoBehaviour
{
    [SerializeField] private Sprite[] idleArray;
    [SerializeField] private Sprite[] mouseoverArray;
    [SerializeField] private Sprite[] problemArray;
    [SerializeField] private Sprite[] mouseoverproblemArray;
    private int currentFrame;
    private float timer;
    
    public float framerate = .5f;

    private SpriteRenderer myRenderer;
    private bool mouseOn;
    
    private float problemTimer;
    private bool problemState = false;
    private bool problemtimerStarted = false;

    private float petTimer;

    // Start is called before the first frame update
    void Start(){
        mouseOn = false;
        myRenderer = gameObject.GetComponent<SpriteRenderer>();
        problemState = false;
        problemtimerStarted = false;
        createProblemTimer();
    }

    void createProblemTimer(){
    if (!problemtimerStarted && !problemState){ //Timer hasnt starter and not in problem state
        problemTimer = Random.Range(5.0f, 10.0f); //Pick a random amnt of time
        problemtimerStarted = true; //Set the timer
        }
    }

    void Update() {
        if (!mouseOn){ //Mouse not over object
        Idle();
        }

        if (!problemState && problemtimerStarted){ //if not in problem state, and the timer starter
        problemTimer -= Time.deltaTime; //Timer counts down
        Debug.Log(problemTimer);
        }

        if(problemTimer <= 0){ //Once random timer reaches zero
        problemTimer = 0; //cant go below zero
        problemState = true; //Now in the problem state
        problemtimerStarted = false; //End timer
        Debug.Log("Problem state started");
        }

        if (problemState){ //In problem state
            CheckClick(); //Play mouse click function
        }

        if(!problemState && !problemtimerStarted){ //ended problem state
        createProblemTimer(); //Restart problem state timer
        problemtimerStarted = true;
        }
    }

    void CheckClick(){ //Check for clicking 
    if (mouseOn && Input.GetMouseButtonDown(0)){ //Click while mouseover & in problem state
            petTimer = 3f; //Setting pet timer
            OnMouseDrag();
        }
    }

    void OnMouseDrag(){
        if (problemState){ //If you're dragging mouse in problem state
        petTimer -= Time.deltaTime; //pet timer counts down
        Debug.Log(petTimer);
            if(petTimer <= 0){ //Once pet timer reaches zero
            petTimer = 0; //cant go below zero
            problemState = false; //end problem state
            createProblemTimer(); //Cue problem timer again
            }
        }
    }

    void Idle(){ //Mouse is not over obj. plays idle animation
        if (!problemState) { //If not in problem state
            timer += Time.deltaTime;
            if (timer >= framerate){
                timer -= framerate;
                currentFrame = (currentFrame + 1) % idleArray.Length;
                myRenderer.sprite = idleArray[currentFrame];
            } //Play the normal idle
        } else if (problemState) { //In the problem state
            timer += Time.deltaTime;
            if (timer >= framerate){
                timer -= framerate;
                currentFrame = (currentFrame + 1) % problemArray.Length;
                myRenderer.sprite = problemArray[currentFrame]; //Play problem animation
            }
        }
    }

    void OnMouseOver() { //Mouse is over object, want to play specific animations
        mouseOn = true;
        if (!problemState) { //Obj not in the problem state
        timer += Time.deltaTime;
        if (timer >= framerate){
            timer -= framerate;
            currentFrame = (currentFrame + 1) % mouseoverArray.Length;
            myRenderer.sprite = mouseoverArray[currentFrame]; //Play normal animation
            }
        } else if (problemState){ //Obj in problem state
        timer += Time.deltaTime;
        if (timer >= framerate){
            timer -= framerate;
            currentFrame = (currentFrame + 1) % mouseoverproblemArray.Length;
            myRenderer.sprite = mouseoverproblemArray[currentFrame]; //Play problem anim
            }
        }
    }

    void OnMouseExit(){
        mouseOn = false; //Mouse no longer on object
    }
}
