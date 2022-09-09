using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtensController : MonoBehaviour
{
    public bool masksState;
    public bool vaccineState;
    public bool quarantineState;
    public bool curfewState;
    // Start is called before the first frame update
    void Start()
    {
        masksState = false;
        vaccineState = false;
        quarantineState = false;
        curfewState = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Nedenunder er de knapper som styre om restrictionerne er tændte eller ej

    public void masks()
    {
        if(masksState==true)
        {
            masksState = false;
            print(masksState);
        }
        else
        {
            masksState = true;
            print(masksState);
        }
    }

    public void vaccine()
    {
        if(vaccineState==true)
        {
            vaccineState = false;
        }
        else
        {
            vaccineState = true;
        }
    }

    public void quarantine()
    {
        if(quarantineState==true)
        {
            quarantineState = false;
        }
        else
        {
            quarantineState = true;
        }
    }

    public void curfew()
    {
        if(curfewState==true)
        {
            curfewState = false;
        }
        else
        {
            curfewState = true;
        }
    }
}
