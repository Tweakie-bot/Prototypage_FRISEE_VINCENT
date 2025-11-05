using UnityEngine;

public class Bougie : MonoBehaviour
{
    [SerializeField] private GameObject bougie;
    private bool IsLighted;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLighted()
    {
        IsLighted = true;
        bougie.GetComponent<Renderer>().material.color = Color.red;
    }

    public bool GetIsLighted()
    {
        return IsLighted;
    }
}
