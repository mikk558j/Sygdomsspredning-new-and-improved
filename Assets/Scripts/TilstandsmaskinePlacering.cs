using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class TilstandsmaskinePlacering : MonoBehaviour
{
    public enum stageOfDiseaseEnum
    {
        susceptible, ill, sick, hospitalised, immune
    }
    stageOfDiseaseEnum stageOfDisease = new stageOfDiseaseEnum();

    public float time;
    public float age;
    public NavMeshAgent npc;

    // Start is called before the first frame update
    void Start()
    {
        stageOfDisease = stageOfDiseaseEnum.susceptible;
    }

    // Update is called once per frame
    void Update()
    {
        switch (stageOfDisease)
        {
            case stageOfDiseaseEnum.susceptible:

                susceptible();

                break;

            case stageOfDiseaseEnum.ill:

                ill();

                break;

            case stageOfDiseaseEnum.sick:

                sick();

                break;

            case stageOfDiseaseEnum.hospitalised:

                hospitalised();

                break;

            case stageOfDiseaseEnum.immune:

                immune();

                break;
        }
    }

    private void susceptible()
    {

    }

    private void ill()
    {

    }

    private void sick()
    {

    }

    private void hospitalised()
    {

    }

    private void immune()
    {

    }
}