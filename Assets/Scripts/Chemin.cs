using UnityEngine;

public class Chemin : MonoBehaviour
{

    [SerializeField]
    GameObject[] murs;



    GameObject mur;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    //Desactive les collider et mesh renderer de tous les murs
    public void OuvrirTousLesMurs()
    {
        for (int i = 0; i < murs.Length; i++)
        {
            murs[i].GetComponent<Walls>().Open();
        }
    }


    public void OuvrirNord()
    {
        murs[0].GetComponent<Walls>().Open();
    }

    public void OuvrirSud()
    {
        murs[1].GetComponent<Walls>().Open();
    }

    public void OuvrirEst()
    {
        murs[2].GetComponent<Walls>().Open();
    }

    public void OuvrirOuest()
    {
        murs[3].GetComponent<Walls>().Open();
    }

    public void OuvrirSol()
    {
        murs[4].GetComponent<Walls>().Open();
    }

    //Réactive les collider et mesh renderer de tous les murs
    public void FermerTousLesMurs()
    {
        for (int i = 0; i < murs.length; i++)
        {
            murs[i].GetComponent<Walls>().Close();
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
