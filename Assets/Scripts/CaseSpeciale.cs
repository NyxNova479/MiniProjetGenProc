using UnityEngine;

public class CaseSpeciale : MonoBehaviour
{

    [SerializeField] GameObject player;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "CaseTP")
        {
            gameObject.transform.position = new Vector3(Random.Range(0, 100), 3, Random.Range(0, 100)); ;
             
            
            
        }

    }



}
