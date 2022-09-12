using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grafExample : MonoBehaviour
{
    graf testIt;
    string GrafId1, GrafId2, GrafId3;
    int cnt;
    public int antalsusceptivle;
    public int antalinfected;
    public int antalrecovered;


    // Start is called before the first frame update
    void Start()
    {
        testIt = GameObject.FindGameObjectWithTag("graf").GetComponent<graf>();
        GrafId1 = testIt.CreateGraf("Hest",Color.green);
        GrafId2 = testIt.CreateGraf("Hund",Color.blue);
        GrafId3 = testIt.CreateGraf("Kat", Color.yellow);

        InvokeRepeating("updateGrafInvoke", 1, 1);
    }

    void updateGrafInvoke()
    {
        CountDiseaseStages();
        // Kalder "CountDiseaseStages" metoden 
        testIt.AddData(GrafId1, new Vector2(cnt, antalsusceptivle));
        testIt.AddData(GrafId2, new Vector2(cnt, antalinfected));
        testIt.AddData(GrafId3, new Vector2(cnt, antalrecovered));
        cnt++;
        testIt.updateGraf();
    }


    void CreateData(string nameId, float t)
    {
        for (int n = 0; n <= 20; n++)
        {
            Vector2 tmp;
            tmp = new Vector2(n * 0.5f, 0.25f*t * n * n);
            testIt.AddData(nameId, tmp);
         }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CountDiseaseStages()
    {
        // Sætter de tre variabler til 0 så at de bliver talt op hver gang vi kører vores foreach loop
        antalinfected = 0;
        antalrecovered = 0;
        antalsusceptivle = 0; 
        
        // Vi finder alle NPCer med NPC tagget og vi putter dem i et array så vi kan få fat i deres components og tælle dem. 
        GameObject[] NpcList = GameObject.FindGameObjectsWithTag("NPC");
        foreach (GameObject NPC in NpcList)
        {
            if(NPC.GetComponent<Agent>().minsygdomstilstand == Agent.sygdomstilstand.susceptivle)
            {
                antalsusceptivle++;
            }
            if (NPC.GetComponent<Agent>().minsygdomstilstand == Agent.sygdomstilstand.infected)
            {
                antalinfected++;
            }
            if (NPC.GetComponent<Agent>().minsygdomstilstand == Agent.sygdomstilstand.recovered)
            {
                antalrecovered++;
            }
        }
    }
}
