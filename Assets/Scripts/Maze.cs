using System.Collections;
using UnityEngine;

public class Maze : MonoBehaviour
{
    GameObject[,] cases;
    [SerializeField]
    GameObject casePrefab;
    GameObject portePrefab;

    // Il me faut au démarrage une génération aléatoire d'un labyrinthe et d'un trésor
    // Labyrinthe = Des cases avec des ouvertures orientées de façon à créer des chemins



    void Start()
    {
        // 1) Générations des cases
        
        cases = new GameObject[100, 100];

        for (int n = 0; n < 100; n++)
        {
            for (int i = 0; i < 100; i++)
            {
                int randRota = Random.Range(0, 4);
                if (randRota == 0)
                {
                    cases[n, i] = Instantiate(casePrefab, new Vector3(n * 2f, 0, i*3f), new Quaternion(0f, 0f, 0f, 0f));

                }
                if (randRota == 1)
                {
                    cases[n, i] = Instantiate(casePrefab, new Vector3(n * 2f, 0, i * 3f), new Quaternion(0f, 90f, 0f, 0f));
                }
                if (randRota == 2)
                {
                    cases[n, i] = Instantiate(casePrefab, new Vector3(n * 2f, 0, i * 3f), new Quaternion(0f, -90f, 0f, 0f));
                }
                if (randRota == 3)
                {
                    cases[n, i] = Instantiate(casePrefab, new Vector3(n *2f, 0, i * 3f), new Quaternion(0f, 180f, 0f, 0f));
                }
                cases[n, i].transform.localScale = new Vector3(2f, 7, 3f);
                
                
            }

        }


        StartCoroutine(Clignotage());


    }

    int indexDeLaCaseCourante = 0;


    // Update is called once per frame
    void Update()
    {
        if (indexDeLaCaseCourante >= 1000) return;
        cases[indexDeLaCaseCourante, 0] = Instantiate(portePrefab, new Vector3 ())
        cases[indexDeLaCaseCourante + 1, 0].GetComponent<Case>().OuvrirOuest();
        indexDeLaCaseCourante++;

    }


    // Je veux au démarrage que les murs clignotes 5 fois pour laisser entrevoir la position du trésor
    // 1) Désactiver les murs un instant 
    // 2) Réactiver les murs 
    // 3) Recommencer l'itération
    IEnumerator Clignotage()
    {
        yield return new WaitForSeconds(0.5f);
        for (int n = 0; n < 3; n++)
        {
            yield return new WaitForSeconds(0.45f);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    cases[i, j].GetComponent<Chemin>().OuvrirTousLesMurs();
                }
            }
            yield return new WaitForSeconds(0.45f);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    cases[i, j].GetComponent<Chemin>().FermerTousLesMurs();
                }

            }
        }
    }
}
