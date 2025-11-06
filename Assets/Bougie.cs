using UnityEngine;

public class Bougie : MonoBehaviour
{
    [SerializeField] GameObject bougieHead;
    [SerializeField] GameObject effect;
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
        bougieHead.GetComponent<Renderer>().material.color = Color.red;
        effect.SetActive(true);
    }

    public bool GetIsLighted()
    {
        return IsLighted;
    }
}
