using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetDetailsRenderer : MonoBehaviour
{
    private int population = 0;
    public Text populationText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 100) == 0) { 
            population += Random.Range(0, 1000);
        }
        populationText.text = "Population: " + population;
    }
}
