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

    GameObject cage;



    // Il me faut au démarrage une génération aléatoire d'un trésor dans une zone remplie de case
    
    //2) Faire de certaines de ces cases des cases spéciales
    // 3) Placer le trésor aléatoirement dans le labyrinthe






    void Start()
    {
        cage = Instantiate(cagePrefab, Vector3.zero, Quaternion.identity);
        // 1) Générations des cases  aléatoirement
        cases = new GameObject[100, 100];

        for (int i = 0; i <= 100; i++)
        {
            for (int j = 0; j <= 100; j++)
            {


                cases[i, j] = Instantiate(casePrefab, new Vector3(i *5, 0, j*5 ), Quaternion.identity);
                
            }
        }
        StartCoroutine(Clignotage());
        StartCoroutine(Libère());
    }


    //Permet de lancer la réapparition des murs après un certain temps donné
    IEnumerator FermeToiSesame()
    {
        yield return new WaitForSeconds(3f);

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {


                cases[i, j].GetComponent<Chemin>().FermerTousLesMurs();

            }
        }
        
       
    }

    float tempsDerniereExecution = 0.0f;
    float delai = 1f;


    // Update is called once per frame
    void Update()
    {
        // Je veux qu'à chaque Frame  2 des murs de chaque case s'ouvrent puis se referme après un instant

       
        {
            // 1) Il faut ouvrir les 2 Murs aléatoirement sur chaque case


            tempsDerniereExecution += Time.fixedDeltaTime;  // ajoute a chaque update le temps écoulé depuis le dernier Update		
            if (tempsDerniereExecution > delai)
            {
                OpenPathRandom();
                tempsDerniereExecution = 0;
                StartCoroutine(FermeToiSesame());
            }


            // 2) Attendre un temps puis faire la manoeuvre inverse
            

            

            
        }
        

    }

    
    

     private void OpenPathRandom()
    {
        for (int n = 0; n <= 1; n++)
        {

            for (int i = 0; i <= 100; i++)
            {

                for (int j = 0; j <= 100; j++)
                {
                    int randWall = Random.Range(0, 4);
                    if (randWall == 0)
                    {

                        cases[i, j].GetComponent<Chemin>().OuvrirSud();
                        cases[i, j].GetComponent<Chemin>().OuvrirNord();
                    }



                    if (randWall == 1)
                    {

                        cases[i, j].GetComponent<Chemin>().OuvrirEst();
                        cases[i, j].GetComponent<Chemin>().OuvrirOuest();
                    }

                    if (randWall == 2)
                    {
                        cases[i, j].GetComponent<Chemin>().OuvrirNord();
                        cases[i, j].GetComponent<Chemin>().OuvrirSud();
                    }

                    if (randWall == 3)
                    {

                        cases[i, j].GetComponent<Chemin>().OuvrirOuest();
                        cases[i, j].GetComponent<Chemin>().OuvrirEst();
                    }
                }
            }
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
            for (int i = 0; i <= 100; i++)
            {
                for (int j = 0; j <= 100; j++)
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
