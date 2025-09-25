using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Maze : MonoBehaviour
{
    GameObject[,] cases;
    [SerializeField]
    GameObject casePrefab;
    [SerializeField]
    GameObject cagePrefab;
    [SerializeField]
    GameObject portePrefab;
    [SerializeField]
    GameObject joueur;

    
    GameObject cage;



    // Il me faut au démarrage une génération aléatoire d'un trésor dans une zone remplie de case
    
    //2) Faire de certaines de ces cases des cases spéciales
    // 3) Placer le trésor aléatoirement dans le labyrinthe






    void Start()
    {
        cage = Instantiate(cagePrefab, Vector3.zero, Quaternion.identity);
        // 1) Générations des cases  aléatoirement
        cases = new GameObject[100, 100];

        for (int n = 0; n < 100; n++)
        {
            for (int i = 0; i < 100; i++)
            {


                cases[n, i] = Instantiate(casePrefab, new Vector3(n *5, 0, i*5 ), Quaternion.identity);
                
            }
        }
        StartCoroutine(Clignotage());
        StartCoroutine(Libère());
    }

    int indexLigneCourante = 0;
    int indexColonneCourante = 0;

    //Permet de lancer la réapparition des murs après un certain temps donné
    IEnumerator OuvreToiSesame(int ligneCourante, int colonneCourante)
    {
        yield return new WaitForSeconds(3f);
        cases[ligneCourante, colonneCourante].GetComponent<Chemin>().FermerTousLesMurs();
        cases[ligneCourante, colonneCourante] = Instantiate(casePrefab, new Vector3(cases[ligneCourante, colonneCourante].transform.position.x, 0, cases[ligneCourante, colonneCourante].transform.position.z), Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        // Je veux qu'à l'appuie sur O, 2 portes apparaissent sur 2 des murs de la case où se trouve le joueur puis se referme après un instant

        int ligneCourante = indexLigneCourante;
        int colonneCourante = indexLigneCourante;


        if (Input.GetKeyDown(KeyCode.O))
        {
            // 1) Il faut ouvrir les 2 Murs aléatoirement selon la case où est le joueur
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
                if (cases[ligneCourante, colonneCourante] == portePrefab)
                {
                    if (ligneCourante == 0)
                    {
                        randWall = Random.Range(0, 2);
                    }
                    if (colonneCourante == 0)
                    {
                        randWall = Random.Range(1, 3);
                    }
                    if (ligneCourante == 0 && colonneCourante == 0)
                    {
                        randWall = Random.Range(1, 2);
                    }
                }
                if (randWall == 0)
                {

                    cases[ligneCourante, colonneCourante].GetComponent<Chemin>().OuvrirSud();
                    // 2) Remplacer les Murs par des portes
                    cases[ligneCourante, colonneCourante] = Instantiate(portePrefab, new Vector3(cases[ligneCourante, colonneCourante].GetComponent<Walls>().transform.position.x, 0, cases[ligneCourante, colonneCourante].GetComponent<Walls>().transform.position.z), Quaternion.identity);
                    cases[colonneCourante, colonneCourante - 1].GetComponent<Chemin>().OuvrirNord();

                }


                if (randWall == 1)
                {

                    cases[ligneCourante, colonneCourante].GetComponent<Chemin>().OuvrirEst();
                    cases[ligneCourante, colonneCourante] = Instantiate(portePrefab, new Vector3(cases[ligneCourante, colonneCourante].transform.position.x*3, 0, cases[ligneCourante, colonneCourante].transform.position.z*3), Quaternion.identity);
                    cases[ligneCourante + 1, colonneCourante].GetComponent<Chemin>().OuvrirOuest();
                }

                if (randWall == 2)
                {
                    cases[ligneCourante, colonneCourante].GetComponent<Chemin>().OuvrirNord();
                    cases[ligneCourante, colonneCourante] = Instantiate(portePrefab, new Vector3(cases[ligneCourante, colonneCourante].transform.position.x*3, 0, cases[ligneCourante, colonneCourante].transform.position.z*3), Quaternion.identity);
                    cases[ligneCourante, colonneCourante + 1].GetComponent<Chemin>().OuvrirSud();
                }

                if (randWall == 3)
                {

                    cases[ligneCourante, ligneCourante].GetComponent<Chemin>().OuvrirOuest();
                    cases[ligneCourante, colonneCourante] = Instantiate(portePrefab, new Vector3(cases[ligneCourante, colonneCourante].transform.position.x*3, 0, cases[ligneCourante, colonneCourante].transform.position.z*3), Quaternion.identity);
                    cases[ligneCourante - 1, colonneCourante].GetComponent<Chemin>().OuvrirEst();
                }
            }
            // 3) Attendre un temps puis faire la manoeuvre inverse
            StartCoroutine(OuvreToiSesame(ligneCourante, ligneCourante));

            

            
        }
        

    }




    // Je veux au démarrage que les murs clignotes 3 fois pour laisser entrevoir la position du trésor



    IEnumerator Clignotage()
    {
        
        yield return new WaitForSeconds(1);
        // 3) Recommencer l'itération
        for (int n = 0; n < 3; n++)
        {
            // 1) Désactiver les murs un instant 
            yield return new WaitForSeconds(0.45f);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    cases[i, j].GetComponent<Chemin>().OuvrirTousLesMurs();
                }
            }
            // 2) Réactiver les murs 
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
    // Au lancement j'aimerais que le player soit incapable de bouger pendant 3 seconds
    // 1) Je créé une prefab cage constituée de collider
    // 2) Je fais un ienumerator pour attendre 3 secondes avant de désactiver les colliders

    IEnumerator Libère()
    {
        yield return new WaitForSeconds(4);
        cage.GetComponent<Cage>().EnleverCage();
    }


}
