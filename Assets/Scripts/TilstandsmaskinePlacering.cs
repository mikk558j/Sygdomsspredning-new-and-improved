using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class TilstandsmaskinePlacering : MonoBehaviour
{
    public enum states
    {
        home, school, work, free
    }
    public float time;
    public float age;
    public NavMeshAgent npc;



    states state;
    // Start is called before the first frame update
    void Start()
    {
        state = states.home;
    }



    // Update is called once per frame
    void Update()
    {
        //Her beskrives hvordan tiden virker i spillet



        //Dette beskriver hvordan en NPC skrifter mellem de forskellige "states" eller lokationer og opførsel som den har i løbet af dagen
        switch (state)
        {
            case states.home:

                //Her tjekkes der for om NPC'en skal skifter over til sin skole, arbejder eller om den har fri
                if (8 < time && time < 14 && 0 < age && age < 10)
                {
                    state = states.school;
                    break;
                }
                else if (8 < time && time < 16 && 10 < age && age < 30)
                {
                    state = states.work;
                    break;
                }
                else if (10 < time && time < 16 && 30 < age && age < 40)
                {
                    state = states.free;
                    break;
                }


                //Her skal der så skrives noget kode der beskriver hvordan NPC'en navigere til sit hus og bliver der

                npc.SetDestination(new Vector3(-4, 0, -2));

                break;
            case states.school:

                if (14 < time && time < 18)
                {
                    state = states.free;
                    break;
                }

                //Her skal der være noget kode som beskriver hvordan NPC'en kommer til skolen og hvad den så gør der

                break;
            case states.work:

                if (16 < time && time < 18)
                {
                    state = states.free;
                    break;
                }

                //Igen, her skal der være noget kode som beskriver hvordan NPC'en skal opføre sig

                break;
            case states.free:

                if (18 < time && time < 24 || 0 < time && time < 8)
                {
                    state = states.home;
                    break;
                }

                break;
            default:

                break;
        }
        Debug.Log(state);
    }
}