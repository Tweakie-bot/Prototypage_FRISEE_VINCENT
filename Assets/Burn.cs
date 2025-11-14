using Unity.VisualScripting;
using UnityEngine;

public class Burn : MonoBehaviour
{
    [SerializeField] private GameObject position;
    [SerializeField] private Material mat;
    [SerializeField] private PlayerInteraction playerInteraction;

    private Vector3 Position;
    private Quaternion Rotation;

    private Material m;
    private int health = 20;

    private bool isBurning;
    void Start()
    {
        m = gameObject.GetComponent<Renderer>().material;
        Position = transform.position;
        Rotation = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    { 
        if (health < 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void GiveHealthBack()
    {
        CancelInvoke("DiminishHealth");

        isBurning = false;

        gameObject.SetActive(true);
        transform.position = Position;
        transform.rotation = Rotation;
        gameObject.GetComponent<Renderer>().material = m;
        health = 20;
    }
    public void DiminishHealth()
    {
        if (isBurning)
        {
            health -= 1;
        }
    }
    public void SetBurning()
    {
        if (isBurning)
            return;

        isBurning = true;
        playerInteraction.AddDestroyedObject(gameObject);
        InvokeRepeating("DiminishHealth", 1, 1);

        if (gameObject.CompareTag("Obstacle"))
        {

            transform.Rotate(new Vector3(90, 0, 0));
            transform.position = position.transform.position;
        }

        gameObject.GetComponent<Renderer>().material = mat;
    }

    public bool GetIsBurning()
    {
        return isBurning;
    }
}
