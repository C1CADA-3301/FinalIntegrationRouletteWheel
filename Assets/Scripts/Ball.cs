using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Ball : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] Transform spinWheel;
    [SerializeField] Animator BallAnim;
    [SerializeField] Transform ballStartPosition;
    [SerializeField] Button SpinBtn;
    [SerializeField] VoidEventSO playUIAnimationEvent;
    [SerializeField] VoidEventSO playCameraAnimationEvent;
    [Header("DATA FIELDS")]
    private float rotateSpeed;
    [SerializeField] float initialRotateSpeed;
    [SerializeField] float maxRotateSpeed;
    [SerializeField] float maxSpinTime =5;
    [SerializeField] float minSpinTime =3;
    private float randomSpinTime;

    [Header("OVERRIDE CONTROLS")]
    [SerializeField] bool isStartSpin;
    [SerializeField] bool canSpin;
    [SerializeField] bool canLerp;
    [SerializeField] bool inCoroutine;
    [SerializeField] bool isDropBall;

    [Header("NOTOFIER BOOLEAN")]
    public bool isPlayEnd;


    private Transform randomNumberPos;
    private float ballReduceSpeed;




    Vector3 _startPos;
    Vector3 _direction;
    void Start()
    {
        rotateSpeed = initialRotateSpeed;
        BallAnim.enabled= false;
        ballReduceSpeed = initialRotateSpeed / 100 * 15;
        canSpin = true;

        RouletteWheelManager.SpinBtn_evt += SpinButton;
        RouletteWheelManager.ResetWheel_evt += ResetBall;
     


    }

    // Update is called once per frame\
    public GameState state;
    void Update()
    {
        state = BetManager.gameState;
        if (canSpin)
        {
            SpinBall();

        }
        if (isStartSpin) 
        {
            StartSpin();
            

        }
      
    }

    public void SpinButton()
    {
        startPos = transform;
        randomNumberPos = RandomNumberGenerator.Instance.randomObject.transform;
       rotateSpeed = maxRotateSpeed;
        isStartSpin = true;
        

    }

    void StartSpin()
    {
       
        if (!inCoroutine) 
        {
           
            StartCoroutine(BallCoroutine());

        }
       
        

        if(canLerp) 
        {
            Lerp();
        }
      
        if(isDropBall) 
        {
            DropBall();
        
        }
    }
    void SpinBall() //For Rotating the Ball around the Wheel
    {   
       
        transform.RotateAround(spinWheel.position,Vector3.forward,rotateSpeed*Time.deltaTime);
        // transform.LookAt(spinWheel.position,Vector2.up);
        transform.right = spinWheel.position - transform.position;
    }


    private Transform startPos;
    private Transform endPos;
    private float fracComplete;
    void Lerp() //For moving the ball to number position
    {
      
        startPos = transform;
        endPos = randomNumberPos;
        if(endPos!= null)
        transform.position = Vector3.Lerp(startPos.position, endPos.position, 0.1f);
    }

    IEnumerator BallCoroutine()
    {
        Debug.Log("InCoroutine");
        inCoroutine = true;  // Prevents Coroutine from being called again in Update function.
       // canSpin= true;      //Ball starts Spinning
  
        yield return new WaitForSeconds(RandomNumberGenerator.Instance.RandomSpinTime(minSpinTime,maxSpinTime)); // Keeps spinning the ball for Some time between MinRotationTime and MaxRotationTime
        isDropBall = true;                                //Starts droping the ball lower
        endPos = randomNumberPos;                        // setting the end position of the ball
        StartCoroutine(ReduceBallSpeed());              //Starts Coroutine to gradually reduce ball speed until it reaches the SlowBallSpeed.
    }
    
    void DropBall()
    {
        _startPos = transform.position;
        _direction = (spinWheel.transform.position - _startPos).normalized;
        transform.Translate(Vector3.right * Time.deltaTime * 2f);
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Number"))
        {
            transform.SetParent(collision.transform);
            canSpin = false;
            BallAnim.SetTrigger("BallTrigger");
            canLerp = true;   
            isDropBall = false;
            StartCoroutine(DisplayWheelOutput());
        }
      
        if(collision.CompareTag("WheelMid"))
        {
            BallAnim.enabled = true;
            BallAnim.speed = 4;
        }
        if (collision.CompareTag("WheelCenter"))
        {
            isDropBall = false;
        }



    }

    
    IEnumerator ReduceBallSpeed()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.05f);

            if (rotateSpeed<0)
            {
                rotateSpeed -= ballReduceSpeed;
            }
        }

    }

    private void ResetBall()
    {
        Debug.Log("@@@@@ BALL RESET @@@@");
        this.transform.SetParent(null, true);
         transform.position = ballStartPosition.position;
        StopAllCoroutines();
        inCoroutine= false;
        isStartSpin = false;
        canSpin = true;
        canLerp = false;
        isDropBall = false;
        rotateSpeed = initialRotateSpeed;
        BallAnim.SetTrigger("Reset");
        BallAnim.enabled= false;
        rotateSpeed = initialRotateSpeed;
       // RouletteWheelManager.Instance.ResetOutput();
        isPlayEnd= false;

    }

    IEnumerator DisplayWheelOutput()
    {
       
        yield return new WaitForSeconds(2f);
        RouletteWheelManager.Instance.DisplayOutPutText();
        StopCoroutine(BallCoroutine());
        StopCoroutine(ReduceBallSpeed());
        yield return new WaitForSeconds(4f);
        isPlayEnd = true;
        RouletteWheelManager.Instance.ResetOutput();
       // 
        playCameraAnimationEvent.RaiseEvent();
        BetManager.gameState = GameState.DISPLAY;

        yield return new WaitForSeconds(3);
      
        RouletteWheelManager.Instance.OutPutDisplayed();
        
    }


}
