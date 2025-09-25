using UnityEngine;

public class Chemin : MonoBehaviour
{

    [SerializeField]
    GameObject[] murs;

    [SerializeField]
    GameObject murOuestPrefab;

    [SerializeField]
    GameObject murSudPrefab;

    [SerializeField]
    GameObject murNordPrefab;

    [SerializeField]
    GameObject murEstPrefab;

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

    //Réactive les collider et mesh renderer de tous les murs
    public void FermerTousLesMurs()
    {
        for (int i = 0; i < murs.Length; i++)
        {
            murs[i].GetComponent<Walls>().Close();
        }
    }

    public void PlaceNord()
    {
        mur = Instantiate(murNordPrefab, new Vector3(murs[0].transform.position.x, 0, murs[0].transform.position.z), new Quaternion(0,0,0,0));
    }

    public void PlaceSud()
    {
        mur = Instantiate(murSudPrefab, new Vector3(murs[1].transform.position.x, 0, murs[1].transform.position.z), new Quaternion(0, 0, 0, 0));
    }

    public void PlaceOuest()
    {
        mur = Instantiate(murOuestPrefab, new Vector3(murs[2].transform.position.x, 0, murs[2].transform.position.z), Quaternion.identity);
    }

    public void PlaceEst()
    {
        mur = Instantiate(murEstPrefab, new Vector3(murs[3].transform.position.x, 0, murs[3].transform.position.z), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
