using System.Collections;
using UnityEngine;

public class Cage : MonoBehaviour
{
    [SerializeField]
    GameObject[] murs;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }


    public void EnleverCage()
    {
        for (int i = 0; i < murs.Length; i++)
        {
            murs[i].GetComponent<CageWalls>().Ouvre();
        }
            
    }
    void Update()
    {

    }

}



// Update is called once per frame

        
    

