using System.Collections;
using UnityEngine;

public class Maze : MonoBehaviour
{
    GameObject[,] cases;
    [SerializeField]
    GameObject casePrefab;
    
    // Il me faut au démarrage une génération aléatoire d'un labyrinthe:
    // Des cases avec des ouvertures orientées de façon à créer des chemins
    // 1) Générations des cases


    void Start()
    {
        int scale = 2;
        cases = new GameObject[100, 100];

        for (int n = 0; n < 100; n++)
        {
            for (int i = 0; i < 100; i++)
            {

                cases[n, i] = Instantiate(casePrefab, new Vector3(n * scale, 0, i), Quaternion.identity);
                cases[n, i].transform.localScale = new Vector3(scale, scale, scale);

            }

        }

        StartCoroutine(Clignotage());


    }

    

    // Update is called once per frame
    void Update()
    {


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
