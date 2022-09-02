using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TidsStyre : MonoBehaviour
{
    Vector3 skyRotation = new Vector3();
    public float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time = time + Time.deltaTime;
        skyRotation = new Vector3(time, 0, 0);
        this.transform.rotation = Quaternion.Euler(skyRotation);
    }
}