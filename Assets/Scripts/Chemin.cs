using UnityEngine;

public class Chemin : MonoBehaviour
{

    [SerializeField]
    GameObject[] murs;

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

    
    //Réactive les collider et mesh renderer de tous les murs
    public void FermerTousLesMurs()
    {
        for (int i = 0; i < murs.Length; i++)
        {
            murs[i].GetComponent<Walls>().Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
