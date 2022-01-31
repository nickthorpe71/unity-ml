using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PopulationManager : MonoBehaviour
{
    public GameObject personPrefab;
    int populationSize = 10;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    int trialTime = 7;
    int generation = 1;

    GUIStyle guiStyle = new GUIStyle();
    private void OnGUI()
    {
        guiStyle.fontSize = 50;
        guiStyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guiStyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)elapsed, guiStyle);
    }

    void Start()
    {
        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            RandomizeDNA(go);
            population.Add(go);
        }
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed > trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }

    void BreedNewPopulation()
    {
        List<GameObject> BreedNewPopulation = new List<GameObject>();
        // get rid of unfit individuals
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeOfDeath).ToList();

        population.Clear();

        // breed upper half of sorted list
        for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++)
        {
            // need to add two new to keep population the same size
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        // destroy all parents and previous population
        for (int i = 0; i < sortedList.Count; i++)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(-9, 9), Random.Range(-4.5f, 4.5f), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();

        offspring.GetComponent<DNA>().r = Random.Range(0.0f, 10.0f) < 5 ? dna1.r : dna2.r;
        offspring.GetComponent<DNA>().g = Random.Range(0.0f, 10.0f) < 5 ? dna1.g : dna2.g;
        offspring.GetComponent<DNA>().b = Random.Range(0.0f, 10.0f) < 5 ? dna1.b : dna2.b;
        // offspring.GetComponent<DNA>().size = Random.Range(0.0f, 10.0f) < 5 ? dna1.size : dna2.size;

        // if (Random.Range(0, 1000) < 5)
        // {
        //     // swap parent DNA
        //     // 50% chance to inherit either parents dna for a color value
        //     offspring.GetComponent<DNA>().r = Random.Range(0.0f, 10.0f) < 5 ? dna1.r : dna2.r;
        //     offspring.GetComponent<DNA>().g = Random.Range(0.0f, 10.0f) < 5 ? dna1.g : dna2.g;
        //     offspring.GetComponent<DNA>().b = Random.Range(0.0f, 10.0f) < 5 ? dna1.b : dna2.b;
        //     offspring.GetComponent<DNA>().size = Random.Range(0.0f, 10.0f) < 5 ? dna1.size : dna2.size;
        // }
        // else
        // {
        //     // mutation
        //     RandomizeDNA(offspring);
        // }
        return offspring;
    }

    void RandomizeDNA(GameObject go)
    {
        DNA dna = go.GetComponent<DNA>();
        dna.r = Random.Range(0.0f, 1.0f);
        dna.g = Random.Range(0.0f, 1.0f);
        dna.b = Random.Range(0.0f, 1.0f);
        // dna.size = Random.Range(0.1f, 1.0f);
    }
}
