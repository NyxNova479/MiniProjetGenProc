using System.Collections;
using UnityEngine;

public class Maze : MonoBehaviour
{
    GameObject[,] cases;
    [SerializeField]
    GameObject casePrefab;
    
    // Il me faut au d�marrage une g�n�ration al�atoire d'un labyrinthe:
    // Des cases avec des ouvertures orient�es de fa�on � cr�er des chemins
    // 1) G�n�rations des cases


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


    // Je veux au d�marrage que les murs clignotes 5 fois pour laisser entrevoir la position du tr�sor
    // 1) D�sactiver les murs un instant 
    // 2) R�activer les murs 
    // 3) Recommencer l'it�ration
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
