using UnityEngine;

public class Path : MonoBehaviour
{

    [SerializeField]
    GameObject[] murs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OuvrirTousLesMurs()
    {
        for (int i = 0; i < murs.Length; i++)
        {
            murs[i].GetComponent<Mur>().Ouvrir();
        }
    }

    public void OuvrirNord()
    {
        murs[0].GetComponent<Mur>().Ouvrir();
    }

    public void OuvrirSud()
    {
        murs[1].GetComponent<Mur>().Ouvrir();
    }

    public void OuvrirEst()
    {
        murs[2].GetComponent<Mur>().Ouvrir();
    }

    public void OuvrirOuest()
    {
        murs[3].GetComponent<Mur>().Ouvrir();
    }

    public void FermerTousLesMurs()
    {
        for (int i = 0; i < murs.Length; i++)
        {
            murs[i].GetComponent<Mur>().Fermer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
