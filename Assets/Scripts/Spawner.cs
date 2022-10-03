using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] allHomes;

    public int houseNr;
    public int populationSize;
    public float percentOfPopulationSick;

    // Start is called before the first frame update
    void Start()
    {
        //Leder efter gameobjects som er makeret med tag "Home"
        allHomes = GameObject.FindGameObjectsWithTag("Home");

        //Spawner NPC'er optil og med populationSize 
        for (var i = 0; i < populationSize; i++)
        {
            //Definere hvilket husnummer NPC'en skal spawne i
            houseNr = Random.Range(0, allHomes.Length);
            
            //Spawner en NPC i dets hus
            GameObject agent =  Instantiate(prefab, allHomes[houseNr].transform.position + new Vector3(Random.Range(0f,2f),0,Random.Range(0f,2f)), Quaternion.identity);
            agent.name = "Finn";
            //Giver husnummeret videre til "TilstandsmaskinePlacering"'s scriptet
            agent.GetComponent<TilstandsmaskinePlacering>().houseNr = houseNr;
            
            if (((i/populationSize)*100)<percentOfPopulationSick)
            {
                agent.GetComponent<TilstandsmaskinePlacering>().stageOfDisease = TilstandsmaskinePlacering.stagesOfDisease.ill;
            }else
            {
                agent.GetComponent<TilstandsmaskinePlacering>().stageOfDisease = TilstandsmaskinePlacering.stagesOfDisease.susceptible;
            }
        }
    }
}
