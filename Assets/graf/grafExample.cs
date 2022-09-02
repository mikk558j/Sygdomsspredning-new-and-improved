using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grafExample : MonoBehaviour
{
    graf testIt;
    string GrafId1, GrafId2, GrafId3;
    int cnt;
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
        testIt.AddData(GrafId1, new Vector2(cnt, -0.25f * cnt*cnt));
        testIt.AddData(GrafId2, new Vector2(cnt, 0.50f * cnt * cnt));
        testIt.AddData(GrafId3, new Vector2(cnt, Random.Range(5,10)));
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
}
