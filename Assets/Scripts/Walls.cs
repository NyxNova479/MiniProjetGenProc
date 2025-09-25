using UnityEngine;

public class Walls : MonoBehaviour
{

    [SerializeField]
    string nom;



    public void Open()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    public void Close()
    {
        gameObject.GetComponent<BoxCollider>().enabled = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }

    // Je veux pouvoir placer une porte à un endroit donné


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
