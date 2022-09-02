using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Instantiates 10 copies of Prefab each 2 units apart from each other


public class Spawner : MonoBehaviour
{
    public GameObject prefab;

    public int xmax;
    public int zmax;
    public int xmin;
    public int zmin;
    public int x;
    public int z; 

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < 20; i++)
        {
            x = Random.Range(xmin, xmax);
            z = Random.Range(zmin, zmax); 
            Instantiate(prefab, new Vector3(x, 0, z), Quaternion.identity);
        }
    }
        
}
