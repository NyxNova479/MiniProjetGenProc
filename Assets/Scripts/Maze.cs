using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Maze : MonoBehaviour
{
    GameObject[,] cases;
    [SerializeField]
    GameObject casePrefab;
    [SerializeField]
    GameObject portePrefab;
    [SerializeField]
    GameObject joueur;


    // Il me faut au d�marrage une g�n�ration al�atoire d'un labyrinthe et d'un tr�sor
    // 1) G�n�rations des cases  al�atoirement
    // 2) Placer le tr�sor al�atoirement dans le labyrinthe





    void Start()
    {
        
        
        cases = new GameObject[100, 100];

        for (int n = 0; n < 100; n++)
        {
            for (int i = 0; i < 100; i++)
            {


                cases[n, i] = Instantiate(casePrefab, new Vector3(n * 2f, 0, i * 3f), Quaternion.identity);


            }

        }


        StartCoroutine(Clignotage());


    }

    int ligneCourante = joueur.transform.position.x;
    int colonneCourante = joueur.transform.position.x;

    IEnumerator OuvreToiSesame()
    {
        yield return new WaitForSeconds(3f);
        cases[ligneCourante, colonneCourante].GetComponent<Chemin>().FermerTousLesMurs();
    }
    // Update is called once per frame
    void Update()
    {
        // Je veux qu'� l'appuie sur O, 2 portes apparaissent sur 2 des murs de la case o� se trouve le joueur puis se referme apr�s un instant

        // 1) Il faut ouvrir les 2 Murs al�atoirement selon la case o� est le joueur
        // 2) Remplacer les Murs par des portes 
        // 3) Attendre un temps puis faire la manoeuvre inverse
        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < 2; i++)
            {
                int randWall = Random.Range(0, 4);
                if (ligneCourante == 0)
                {
                    randWall = Random.Range(0, 3);
                }
                if (colonneCourante == 0)
                {
                    randWall = Random.Range(1, 4);
                }
                if (ligneCourante == 0 && colonneCourante == 0)
                {
                    randWall = Random.Range(1, 3);
                }

                if (randWall == 0)
                {

                    cases[ligneCourante, colonneCourante].GetComponent<Chemin>().OuvrirSud();
                    cases[colonneCourante, colonneCourante - 1].GetComponent<Chemin>().OuvrirSud();
                }


                if (randWall == 1)
                {

                    cases[ligneCourante, colonneCourante].GetComponent<Chemin>().OuvrirEst();
                    cases[ligneCourante + 1, colonneCourante].GetComponent<Chemin>().OuvrirEst();
                }

                if (randWall == 2)
                {
                    cases[ligneCourante, colonneCourante].GetComponent<Chemin>().OuvrirNord();
                    cases[ligneCourante, colonneCourante + 1].GetComponent<Chemin>().OuvrirNord();
                }

                if (randWall == 3)
                {

                    cases[ligneCourante, ligneCourante].GetComponent<Chemin>().OuvrirOuest();
                    cases[ligneCourante - 1, colonneCourante].GetComponent<Chemin>().OuvrirOuest();
                }
            }
            
            StartCoroutine(OuvreToiSesame());

                ligneCourante++;
                colonneCourante++;
            
        }
        

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
