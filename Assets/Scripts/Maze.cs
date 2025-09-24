using System.Collections;
using UnityEngine;

public class Maze : MonoBehaviour
{
    GameObject[,] cases;
    [SerializeField]
    GameObject casePrefab;
    
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

    int indexDeLaCaseCourante = 0;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {


            if (indexDeLaCaseCourante >= 1000) return;
            cases[indexDeLaCaseCourante, 0].GetComponent<Case>().OuvrirEst();
            cases[indexDeLaCaseCourante + 1, 0].GetComponent<Case>().OuvrirOuest();
            indexDeLaCaseCourante++;





        }
    }

    IEnumerator Clignotage()
    {
        yield return new WaitForSeconds(2f);
        for (int n = 0; n < 5; n++)
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    cases[i, j].GetComponent<Case>().OuvrirTousLesMurs();
                }
            }
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    cases[i, j].GetComponent<Case>().FermerTousLesMurs();
                }

            }
        }
    }
}
