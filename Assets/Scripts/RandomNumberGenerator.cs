using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomNumberGenerator : MonoBehaviour
{
    public static RandomNumberGenerator Instance;
    [SerializeField] GameObject[] Numbers;
    public GameObject randomObject;
    public  int randomNumber;
    // Start is called before the first frame update
    void Start()
    {
        //foreach(GameObject obj in Numbers) 
        //{
        //    obj.GetComponent<Collider2D>().enabled = false;
        //}
       
        Instance = this;
        
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform RandomNumberPosition()
    {
        GenerateRandomNumber();
       
        foreach(GameObject obj in Numbers) 
        {
            
            if (obj.GetComponent<WheelSlotNumberData>()!=null)
            {
                
                if (obj.GetComponent<WheelSlotNumberData>().SlotNumber == randomNumber)
                {
                    obj.GetComponent<Collider2D>().enabled = true;
                    randomObject = obj;
                    
                }

                else
                {
                   
                    obj.GetComponent<Collider2D>().enabled = false;
                }

                    
            }
           
        }
       
        Debug.Log("Random Object = "+randomObject.name);


        return randomObject.transform;
    }

    private void GenerateRandomNumber()
    {
        
        int previousRandomNumber = randomNumber;
     
        while (previousRandomNumber == randomNumber)
        {
            randomNumber = Random.Range(0, Numbers.Length);


        }
        //return randomNumber;



    }

    public float randomSpinTime;
    public float RandomSpinTime(float minSpinTime,float maxSpinTime) // For Calculating the Random Spin Time
    {
        randomSpinTime = Random.Range(minSpinTime, maxSpinTime);
        Debug.Log("Spin Time = " + randomSpinTime);
        return randomSpinTime;

    }



}
