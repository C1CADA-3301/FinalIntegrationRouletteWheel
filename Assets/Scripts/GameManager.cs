using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject[] currentQueue;
    [SerializeField] Transform deleteButton;
    [SerializeField] Transform undoButton;
    public float smoothTime = 1000F;
    private Vector3 velocity = Vector3.zero;
    Vector3 curPos;

    float waitTime;
    // GameObject currentObject;

    //bool targetReached = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DestroyImages()
    {
        foreach(GameObject currentObject in currentQueue)
        {
            if(currentObject != null)
            {
                StartCoroutine(MoveLerp(currentObject, deleteButton));
            }
           
        }
             
    }

    public void DeleteImage()
    {
        for(int i = 0 ; i < currentQueue.Length; i++)
        {
            if(currentQueue[i] != null)
            {
                if(i == currentQueue.Length-1 || currentQueue[i+1] == null )
                {
                    Debug.Log(currentQueue[i]);
                    StartCoroutine(MoveLerp(currentQueue[i], undoButton));
                }
            }   
        }
    }

    IEnumerator MoveLerp(GameObject currentObject,Transform button)
    {
        float progress = 0;
        while (progress < 1)
        {
        progress = Mathf.Clamp01(progress + Time.deltaTime * 10);
        // move this objects position = Lerp(starting position, lerp to this position, point of movement along timeline);
        currentObject.transform.position = Vector3.Lerp(currentObject.transform.position, button.position, progress);
        yield return new WaitForSeconds(0.00001f);
        } 
        Destroy(currentObject,0.00001f);
    }
}
