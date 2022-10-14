using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class TilstandsmaskinePlacering : MonoBehaviour
{
    //Her deklereres hvilke ting, der importeres fra unity.
    public NavMeshAgent agent;
    public ButtensController knapStates;
    public TidsStyre timeState;
    
    //Dette enum er et fossil af de forberedelser som blev lavet til implimenteringen af sygdomsspredning.
    public enum stagesOfDisease
    {
        susceptible, ill, sick, hospitalized, immune
    }
    public stagesOfDisease stageOfDisease = new stagesOfDisease();

    //Dette enum bruges sammen med en switch-case til navigation
    public enum placeToBe
    {
        home, school, work, free, hospital, hospitalized
    }
    public placeToBe placeToGo = new placeToBe();

    //Her deklereres lister af gameobjects og enekelte gameobjects.
    public GameObject[] allHomes;
    public GameObject currentHouse;
    public GameObject[] allSchools;
    public GameObject currentSchool;
    public GameObject[] allWork;
    public GameObject currentWork;
    public GameObject[] allFree;
    public GameObject currentFree;
    public GameObject[] allNpc;
    public GameObject[] allHospitals;

    //Her defineres nolge integers til at navigere i listerne af gameobejcts. 
    public int houseNr;
    public int schoolNr;
    public int workNr;
    public int freeNr;

    //Her nogle variabler til styring af alder og tid.
    public float time;
    public int age;
    public int ageStart;
    public int ageOfDeath;
    public int ageOfBirth;
    
    //Denne bool bliver brugt i goFree().
    public bool foundFree = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //Her indhentes spillets tid og restrictions-knappernes tilstande til senere brug.
        knapStates = GameObject.Find("Buttens Controler Object").GetComponent<ButtensController>();
        timeState = GameObject.Find("Directional Light").GetComponent<TidsStyre>();

        //Her defineres de forskellige lister til at indeholde de gameobejct som de skal.
        allHomes = GameObject.FindGameObjectsWithTag("Home");
        allSchools = GameObject.FindGameObjectsWithTag("School");
        allWork = GameObject.FindGameObjectsWithTag("Work");
        allFree = GameObject.FindGameObjectsWithTag("Free");
        allNpc = GameObject.FindGameObjectsWithTag("Npc");
        allHospitals = GameObject.FindGameObjectsWithTag("Hospital");

        //Her defineres hvilken skole og arbejdsplads de skal starte med at have.
        schoolNr = Random.Range(0, allSchools.Length);
        workNr = Random.Range(0, allWork.Length);

        //Her findes så det specifikke hus, skole og arbejdsplads som de starter med. Det er lavet sådan at de har det samme hus også efter deres død.
        currentHouse = allHomes[houseNr];
        currentSchool = allSchools[schoolNr];
        currentWork = allWork[workNr];

        //Her defineres hvilket sted i den senere switch-case de skal starte med at være i.
        placeToGo = placeToBe.home;
        goHome();

        //Her indstilles deres start alder. Deres død af alderdom styres lidt senere.
        ageStart = Random.Range(0,40);
        age = ageStart;
        ageOfDeath = 20 + Random.Range(0, 20);
        ageOfBirth = 0 ;
    }

    // Update is called once per frame
    void Update()
    {
        //Her indstilles NPC'ens hastighed og accelleration.
        agent.speed = 50f / timeState.lengthOfDayInMin;
        agent.angularSpeed = 500f / timeState.lengthOfDayInMin;
        agent.acceleration = 1000f / timeState.lengthOfDayInMin;

        //Her opdaters tiden til senere brug.
        time = timeState.skyRotation.x;

        //Her bestemmes hvordan NPC'ens alder ændre sig og hvornår de så dør. 
        age = ageStart + timeState.daysPast - ageOfBirth;
        if (age>ageOfDeath)
        {
            death();
        }

        //Her skiftes mellem hvilket sted NPC'en skal gå til.
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

    //Følgende 3 metoder er igen fossiler af prep til implimenteringen af smidte.
    private void goVaccinate()
    {

    }

    private void goHospital()
    {

    }

    private void goHospitalized()
    {

    }

    //Følgende 4 metoder er til for at gøre switch-casen mere overskuelig. Noget af koden ligner meget hinanden, da det er meget af det samme, men ikke helt.
    private void goHome()
    {
        //I de forskellige if-statements testes der for om alderen og tiden er korrekt til at de kan gå videre til det næste sted.
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
            //Hvis ikke de falder igennem if-statementsne, så navigere de bare hjem.
            agent.SetDestination(currentHouse.transform.position);
        }
    }

    private void goSchool()
    {
        //Her igen tjekkes der for om tiden er rigtig til at de kan gå videre til noget andet, og hvis ikke, navigere til deres skole.
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
        //Igen tjek på tid om de skal gå videre, ellers navigeres til deres arbejde.
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
        //Først et tjek på om de skal gå videre.
        if (time>165)
        {
            placeToGo = placeToBe.home;
            foundFree = false;
        }
        //Ellers kigges der efter om de har fundet et random fritidssted at gå hen. Hvis ikke, så regner de det, og hvis ja gå de så der hen. 
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
        //Hvis en NPC dør, bliver den sat tilbage til deres hus, får en ny alder, fødselsdag, dødsalder, skole og arbejdsplas. Dvs. at det eneste der ikke genereres igen er selve gameobejctet og dets husnr.
        this.transform.position = currentHouse.transform.position+ new Vector3(Random.Range(0f, 2f), 0, Random.Range(0f, 2f));

        ageStart = 0;
        ageOfBirth = timeState.daysPast;
        ageOfDeath = 20 + Random.Range(0, 20);

        schoolNr = Random.Range(0, allSchools.Length);
        workNr = Random.Range(0, allWork.Length);
    }
}