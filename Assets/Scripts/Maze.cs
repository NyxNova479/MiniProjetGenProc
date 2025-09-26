using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Maze : MonoBehaviour
{
    GameObject[,] cases;

    [SerializeField]
    GameObject casetresorPrefab;

    GameObject[,] caseTresor;

    [SerializeField]
    GameObject caseBlinkPrefab;

    GameObject[,] caseBlink;

    [SerializeField]
    GameObject caseTPPrefab;

    GameObject[,] caseTP;



    [SerializeField]
    GameObject casePrefab;
    [SerializeField]
    GameObject cagePrefab;

    

    GameObject cage;



    // Il me faut au d�marrage une g�n�ration al�atoire d'un tr�sor dans une zone remplie de case
    // J'aimerais aussi que certaines cases aient des effets commes un clignotage ou un t�l�porteur
    // Je veux que quand le joueur rentre: 1. dans une cases clignotage, cela lance le clignotage des case; 2. dans une case t�l�porteur il soit t�l�porter al�atoirement dans la map 
    






    void Start()
    {
        cage = Instantiate(cagePrefab, Vector3.zero, Quaternion.identity);
        // 1) G�n�rations des cases  al�atoirement
        cases = new GameObject[100, 100];
        caseTresor = new GameObject[100, 100];
        caseBlink = new GameObject[100, 100];
        caseTP = new GameObject[100, 100];
        //Cr�� les coordonn�es en x et z des cases sp�ciales
        int tresorRandX = Random.Range(30, 40);
        int tresorRandZ = Random.Range(30, 40);

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {


                cases[i, j] = Instantiate(casePrefab, new Vector3(i * 5, 0, j * 5), Quaternion.identity);
                // G�n�re des cases tr�sor pour qu'elles existent pour plus tard
                caseTresor[i, j] = Instantiate(casetresorPrefab, new Vector3(i * 5, 500000000, j * 5), Quaternion.identity);
                // G�n�re des cases clignotage pour qu'elles existent pour plus tard
                caseBlink[i, j] = Instantiate(caseBlinkPrefab, new Vector3(i * 5, 500000000000, j * 5), Quaternion.identity);
                // G�n�re des cases tp pour qu'elles existent pour plus tard
                caseTP[i, j] = Instantiate(caseTPPrefab, new Vector3(i * 5, 5000000000000, j * 5), Quaternion.identity);
            }
        }

        // G�n�ration al�atoires de 350 cases , elles seront des cases de clignotage
        for (int x = 0; x < 350; x++)
        {
            int cligneRandX = Random.Range(30, 100);
            int cligneRandZ = Random.Range(30, 100);
            G�n�reCaseCligne(cligneRandX, cligneRandZ);
        }

        // G�n�ration al�atoire de 350 autres cases , elles seront des cases de t�l�porteuses
        for (int x = 0; x < 350; x++)
        {
            int tpRandX = Random.Range(30, 100);
            int tpRandZ = Random.Range(30, 100);
            G�n�reCaseCligne(tpRandX, tpRandZ);
        }

        // 2) D�sactiver une case al�atoirement, elle deviendra une case tr�sor
        cases[tresorRandX, tresorRandZ].GetComponent<Chemin>().OuvrirTousLesMurs();
        cases[tresorRandX, tresorRandZ].GetComponent<Chemin>().OuvrirSol();
        // 3) Instantier un tr�sor � cette emplacement dans la map
        caseTresor[tresorRandX, tresorRandZ] = Instantiate(casetresorPrefab, new Vector3(tresorRandX * 5, 0, tresorRandZ * 5), Quaternion.identity);

        // Lance le clignotage des cases
        StartCoroutine(Clignotage());

        // Lib�re le joueur de sa case
        StartCoroutine(Lib�re());
    }

 
    

    

    private void G�n�reCaseTP(int tpRandX, int tpRandZ)
    {
        cases[tpRandX, tpRandZ].GetComponent<Chemin>().OuvrirTousLesMurs();
        cases[tpRandX, tpRandZ].GetComponent<Chemin>().OuvrirSol();
        cases[tpRandX,tpRandZ] = Instantiate(caseTPPrefab,new Vector3(tpRandX*5,0,tpRandZ*5),Quaternion.identity);
     
    }



    private void G�n�reCaseCligne(int cligneRandX, int cligneRandZ)
    {
        cases[cligneRandX, cligneRandZ].GetComponent<Chemin>().OuvrirTousLesMurs();
        cases[cligneRandX, cligneRandZ].GetComponent<Chemin>().OuvrirSol();
        cases[cligneRandX,cligneRandZ] = Instantiate(caseBlinkPrefab, new Vector3(cligneRandX * 5,0,cligneRandZ*5),Quaternion.identity);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Clignotage());
        }
    }

    //Permet de lancer la r�apparition des murs apr�s un certain temps donn�
    IEnumerator FermeToiSesame()
    {
        yield return new WaitForSeconds(2f);

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {


                cases[i, j].GetComponent<Chemin>().FermerTousLesMurs();

            }
        }
        
       
    }

    float tempsDerniereExecution = 0.0f;
    float delai = 2f;


    // Update is called once per frame
    void Update()
    {
       // Je veux qu'� chaque Frame  2 des murs de chaque case s'ouvrent puis se referme apr�s un instant

       
        {



            tempsDerniereExecution += Time.fixedDeltaTime;  // ajoute a chaque update le temps �coul� depuis le dernier Update		
            if (tempsDerniereExecution > delai)
            {
                // 1) Il faut ouvrir les 2 Murs al�atoirement sur chaque case
                OpenPathRandom();
                tempsDerniereExecution = 0;
                // 2) Attendre un temps puis faire la manoeuvre inverse
                StartCoroutine(FermeToiSesame());
            }


            
        }
        

    }

    
    

     private void OpenPathRandom()
     {
        for (int n = 0; n < 2; n++)
        {

            for (int i = 0; i < 100; i++)
            {

                for (int j = 0; j < 100; j++)
                {
                    int randWall = Random.Range(0, 4);
                    if (randWall == 0)
                    {

                        cases[i, j].GetComponent<Chemin>().OuvrirSud();
                        cases[i, j].GetComponent<Chemin>().OuvrirNord();
                        caseBlink[i, j].GetComponent<Chemin>().OuvrirSud();
                        caseBlink[i, j].GetComponent<Chemin>().OuvrirNord();
                        caseTP[i, j].GetComponent<Chemin>().OuvrirSud();
                        caseTP[i, j].GetComponent<Chemin>().OuvrirNord();
                    }



                    if (randWall == 1)
                    {

                        cases[i, j].GetComponent<Chemin>().OuvrirEst();
                        cases[i, j].GetComponent<Chemin>().OuvrirOuest();
                        caseBlink[i, j].GetComponent<Chemin>().OuvrirEst();
                        caseBlink[i, j].GetComponent<Chemin>().OuvrirOuest();
                        caseTP[i, j].GetComponent<Chemin>().OuvrirEst();
                        caseTP[i, j].GetComponent<Chemin>().OuvrirOuest();
                    }

                    if (randWall == 2)
                    {
                        cases[i, j].GetComponent<Chemin>().OuvrirNord();
                        cases[i, j].GetComponent<Chemin>().OuvrirSud();
                        caseBlink[i, j].GetComponent<Chemin>().OuvrirNord();
                        caseBlink[i, j].GetComponent<Chemin>().OuvrirSud();
                        caseTP[i, j].GetComponent<Chemin>().OuvrirNord();
                        caseTP[i, j].GetComponent<Chemin>().OuvrirSud();
                    }

                    if (randWall == 3)
                    {

                        cases[i, j].GetComponent<Chemin>().OuvrirOuest();
                        cases[i, j].GetComponent<Chemin>().OuvrirEst();
                        caseBlink[i, j].GetComponent<Chemin>().OuvrirOuest();
                        caseBlink[i, j].GetComponent<Chemin>().OuvrirEst();
                        caseTP[i, j].GetComponent<Chemin>().OuvrirOuest();
                        caseTP[i, j].GetComponent<Chemin>().OuvrirEst();
                    }
                }
            }
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
                    caseBlink[i, j].GetComponent<Chemin>().OuvrirTousLesMurs();
                    caseTP[i, j].GetComponent<Chemin>().OuvrirTousLesMurs();
                }
            }

            // 2) R�activer les murs 
            yield return new WaitForSeconds(0.45f);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    cases[i, j].GetComponent<Chemin>().FermerTousLesMurs();
                    caseBlink[i, j].GetComponent<Chemin>().FermerTousLesMurs();
                    caseTP[i, j].GetComponent<Chemin>().FermerTousLesMurs();
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
