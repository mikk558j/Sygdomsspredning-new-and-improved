using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class TilstandsmaskinePlacering : MonoBehaviour
{
    public NavMeshAgent agent;
    //public ButtensController knapStates;
    public TidsStyre timeState;
    
    public enum stagesOfDisease
    {
        susceptible, ill, sick, hospitalized, immune
    }
    public stagesOfDisease stageOfDisease = new stagesOfDisease();

    public enum placeToBe
    {
        home, school, work, free, hospital, hospitalized
    }
    public placeToBe placeToGo = new placeToBe();

    public GameObject[] allHomes;
    public GameObject currentHouse;
    public GameObject[] allSchools;
    public GameObject currentSchool;
    public GameObject[] allWork;
    public GameObject currentWork;
    public GameObject[] allFree;
    public GameObject currentFree;
    public GameObject[] allNpc;

    public int houseNr;
    public int schoolNr;
    public int workNr;
    public int freeNr;
    public float time;
    public int age;
    public int ageStart;
    public int ageOfDeath;
    public int ageOfBirth;
    public bool foundFree;
    

    // Start is called before the first frame update
    void Start()
    {
        //knapStates = GameObject.Find("GameController").GetComponent<ButtensController>();
        timeState = GameObject.Find("Directional Light").GetComponent<TidsStyre>();
        allHomes = GameObject.FindGameObjectsWithTag("Home");
        allSchools = GameObject.FindGameObjectsWithTag("School");
        allWork = GameObject.FindGameObjectsWithTag("Work");
        allFree = GameObject.FindGameObjectsWithTag("Free");
        allNpc = GameObject.FindGameObjectsWithTag("Npc");

        schoolNr = Random.Range(0, allSchools.Length);
        workNr = Random.Range(0, allWork.Length);

        currentHouse = allHomes[houseNr];
        currentSchool = allSchools[schoolNr];
        currentWork = allWork[workNr];

        placeToGo = placeToBe.home;
        goHome();

        ageStart = Random.Range(0,40);
        age = ageStart;
        ageOfDeath = 20 + Random.Range(0, 20);
        ageOfBirth = 0 ;



        foundFree = false;
    }

    // Update is called once per frame
    void Update()
    {

        agent.speed = 50f / timeState.lengthOfDayInMin;
        agent.angularSpeed = 500f / timeState.lengthOfDayInMin;
        agent.acceleration = 1000f / timeState.lengthOfDayInMin;

        time = timeState.skyRotation.x;

        //Her skal jeg skrive noget kode som beskriver hvordan deres alder ændre sig
        age = ageStart + timeState.daysPast - ageOfBirth;
        if (age>ageOfDeath)
        {
            death();
        }

        //Her skal der styres med hvordan sygdommen går



        //Her skiftes mellem hvilket sted NPC'en skal gå til (langt fra done)
        switch (placeToGo)
        {
            case placeToBe.home:
                goHome();
                break;

            case placeToBe.school:
                goSchool();
                break;

            case placeToBe.work:
                goWork();
                break;

            case placeToBe.free:
                goFree();
                break;

            case placeToBe.hospital:
                goHospital();
                break;

            case placeToBe.hospitalized:
                goHospitalized();
                break;
        }
    }

    private void goVaccinate()
    {

    }

    private void goHospital()
    {

    }

    private void goHospitalized()
    {

    }

    private void goHome()
    {
        if(time>30&&time<120&&age<10)
        {
            placeToGo = placeToBe.school;
        }else if(time>30&&time<150&&age>10&&age<30)
        {
            placeToGo = placeToBe.work;
        }else if(time>60&&time<165&&age>30)
        {
            placeToGo = placeToBe.free;
        }else
        {
            agent.SetDestination(currentHouse.transform.position);
        }
    }

    private void goSchool()
    {
        if (time > 120)
        {
            placeToGo = placeToBe.free;
        }
        else
        {
            agent.SetDestination(currentSchool.transform.position);
        }
    }

    private void goWork()
    {
        if (time>150)
        {
            placeToGo = placeToBe.free;
        }else
        {
            agent.SetDestination(currentWork.transform.position);
        }
    }

    private void goFree()
    {
        if (time>165)
        {
            placeToGo = placeToBe.home;
            foundFree = false;
        }
        else if (foundFree == false)
        {
            freeNr = Random.Range(0, allFree.Length);
            currentFree = allFree[freeNr];
            foundFree = true;

        }
        else if (foundFree == true)
        {
            agent.SetDestination(currentFree.transform.position);
        }
    }

    private void death()
    {
        this.transform.position = currentHouse.transform.position+ new Vector3(Random.Range(0f, 2f), 0, Random.Range(0f, 2f));

        ageStart = 0;
        ageOfBirth = timeState.daysPast;
        ageOfDeath = 20 + Random.Range(0, 20);

        schoolNr = Random.Range(0, allSchools.Length);
        workNr = Random.Range(0, allWork.Length);
    }
}