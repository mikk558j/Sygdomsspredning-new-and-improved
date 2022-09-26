using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TidsStyre : MonoBehaviour
{
    public Vector3 skyRotation = new Vector3();
    private float timeState;
    public float time;
    public float lengthOfDayInMin;
    private float runTime;
    public int daysPast;

    // Start is called before the first frame update
    void Start()
    {
        daysPast = 0;
    }

    // Update is called once per frame
    void Update()
    {

        runTime = runTime + Time.deltaTime;

        //print(runTime);

        time = time + Time.deltaTime;
        
        skyRotation = new Vector3(-20+(time*(220/(lengthOfDayInMin*60))), 0, 0);
        
        if (skyRotation.x>200)
        {
            skyRotation = new Vector3(-20, 0, 0);
            time = 0;
            daysPast++;
        }

        print(daysPast);

        this.transform.rotation = Quaternion.Euler(skyRotation);
    }
}