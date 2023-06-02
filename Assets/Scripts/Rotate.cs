using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float initialspeed = 10;
    [SerializeField] float maxspeed = 20;
    private float currentSpeed;
    private float wheelReduceSpeed;
    // Start is called before the first frame update
    void Start()
    {
        RouletteWheelManager.SpinBtn_evt += IncreaseSpeed;
       RouletteWheelManager.ResetWheel_evt += ResetWheel;

        wheelReduceSpeed = initialspeed / 100 * 15;
        currentSpeed = initialspeed;
    }

    // Update is called once per frame
    void Update()
    {
        RotateObj();
    }
    void RotateObj()
    {
        transform.Rotate(Vector3.forward*currentSpeed*Time.deltaTime);
    }

    void IncreaseSpeed()
    {
        
       currentSpeed = maxspeed;
        Debug.Log("increase speed!");
        StartCoroutine(SpinTimer());
    }

    IEnumerator SpinTimer()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("Spintimer coroutine");
        yield return new WaitForSeconds(RandomNumberGenerator.Instance.randomSpinTime);
        StartCoroutine(ChangeSpeed());
        StopCoroutine(SpinTimer());
    }

    IEnumerator ChangeSpeed()
    {
        while(true) 
        {
            yield return new WaitForSeconds(0.05f);
            if(currentSpeed>initialspeed) 
            {
                currentSpeed -= wheelReduceSpeed;
            
            }
            

        }
       
    }

    void ResetWheel()
    {
        Debug.Log("RESET!!!");
        currentSpeed = initialspeed;
        StopAllCoroutines();
        
    }
    
}
