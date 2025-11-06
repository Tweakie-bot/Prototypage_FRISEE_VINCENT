using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    [SerializeField] private float MinSize;
    [SerializeField] private float ShrinkRate;

    private Vector3 OriginalSize;

    private bool isShrinking = true;

    private PlayerInteraction playerInteraction;
    void Start()
    {
        playerInteraction = gameObject.GetComponent<PlayerInteraction>();

        OriginalSize = transform.localScale;

        InvokeRepeating("Shrink", 5, 2);
    }

    void Update()
    {
        if(transform.localScale.y < MinSize)
        {
            playerInteraction.Respawn();
            Debug.Log("Respawn at " + playerInteraction.GetRespawn());
        }
    }

    public void Shrink()
    {
        if (isShrinking == true)
        {
            Vector3 shrink = new Vector3(ShrinkRate, ShrinkRate, ShrinkRate);
            transform.localScale -= shrink;
        }
    }

    public void ShrinkFast()
    {
        if (isShrinking == true)
        {
            Vector3 shrink = new Vector3(ShrinkRate * 2, ShrinkRate * 2, ShrinkRate * 2);
            transform.localScale -= shrink;
        }
    }

    public void GrowBack()
    {
        if (transform.localScale.y < OriginalSize.y)
        {
            transform.localScale += new Vector3(ShrinkRate, ShrinkRate, ShrinkRate);
        }
    }

    public void SetIsShrinking(bool boolean)
    {
        isShrinking = boolean;
    }

    public void ResetSize()
    {
        transform.localScale = OriginalSize;
    }
}
