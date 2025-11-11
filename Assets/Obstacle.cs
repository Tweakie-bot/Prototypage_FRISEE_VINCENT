using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private GameObject position;

    private bool isBurning;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 

    }

    public void SetBurning()
    {
        isBurning = true;
        InvokeRepeating("DiminishHealth", 1, 2);
        transform.Rotate(new Vector3 (90, 0, 0));
        transform.position = position.transform.position;

        gameObject.GetComponent<Renderer>().material.color = Color.red;
    }

    public bool GetIsBurning()
    {
        return isBurning;
    }
}
