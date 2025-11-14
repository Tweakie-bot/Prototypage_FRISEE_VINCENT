using UnityEngine;

public class Bougie : MonoBehaviour
{
    [SerializeField] GameObject bougieHead;
    [SerializeField] GameObject effect;
    [SerializeField] Material _material;

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
        bougieHead.GetComponent<Renderer>().material = _material;
        effect.SetActive(true);
    }

    public bool GetIsLighted()
    {
        return IsLighted;
    }
}
