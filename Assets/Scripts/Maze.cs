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



    // Il me faut au d�marrage une g�n�ration al�atoire d'un tr�sor dans une zone remplie de case
    
    //2) Faire de certaines de ces cases des cases sp�ciales
    // 3) Placer le tr�sor al�atoirement dans le labyrinthe






    void Start()
    {
        cage = Instantiate(cagePrefab, Vector3.zero, Quaternion.identity);
        // 1) G�n�rations des cases  al�atoirement
        cases = new GameObject[100, 100];

        for (int n = 0; n < 100; n++)
        {
            for (int i = 0; i < 100; i++)
            {


                cases[n, i] = Instantiate(casePrefab, new Vector3(n *5, 0, i*5 ), Quaternion.identity);
                
            }
        }
        StartCoroutine(Clignotage());
        StartCoroutine(Lib�re());
    }

    int indexLigneCourante = 0;
    int indexColonneCourante = 0;

    //Permet de lancer la r�apparition des murs apr�s un certain temps donn�
    IEnumerator OuvreToiSesame(int ligneCourante, int colonneCourante)
    {
        yield return new WaitForSeconds(3f);
        cases[ligneCourante, colonneCourante].GetComponent<Chemin>().FermerTousLesMurs();
        cases[ligneCourante, colonneCourante] = Instantiate(casePrefab, new Vector3(cases[ligneCourante, colonneCourante].transform.position.x, 0, cases[ligneCourante, colonneCourante].transform.position.z), Quaternion.identity);
    }


    // Update is called once per frame
    void Update()
    {
        // Je veux qu'� l'appuie sur O, 2 portes apparaissent sur 2 des murs de la case o� se trouve le joueur puis se referme apr�s un instant

        int ligneCourante = indexLigneCourante;
        int colonneCourante = indexLigneCourante;


        if (Input.GetKeyDown(KeyCode.O))
        {
            // 1) Il faut ouvrir les 2 Murs al�atoirement selon la case o� est le joueur
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




    // Je veux au d�marrage que les murs clignotes 3 fois pour laisser entrevoir la position du tr�sor



    IEnumerator Clignotage()
    {
        
        yield return new WaitForSeconds(1);
        // 3) Recommencer l'it�ration
        for (int n = 0; n < 3; n++)
        {
            // 1) D�sactiver les murs un instant 
            yield return new WaitForSeconds(0.45f);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    cases[i, j].GetComponent<Chemin>().OuvrirTousLesMurs();
                }
            }
            // 2) R�activer les murs 
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
    // 1) Je cr�� une prefab cage constitu�e de collider
    // 2) Je fais un ienumerator pour attendre 3 secondes avant de d�sactiver les colliders

    IEnumerator Lib�re()
    {
        yield return new WaitForSeconds(4);
        cage.GetComponent<Cage>().EnleverCage();
    }


}
