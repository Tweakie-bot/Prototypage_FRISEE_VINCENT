using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerSize : MonoBehaviour
{
    [SerializeField] private GameObject capsule;
    [SerializeField] private float MinSize;
    private Vector3 OriginalSize;
    private bool isShrinking = true;

    private bool GameOver;

    private PlayerInteraction playerInteraction;
    void Start()
    {
        playerInteraction = GetComponent<PlayerInteraction>();

        OriginalSize = capsule.transform.localScale;

        InvokeRepeating("Shrink", 2, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(capsule.transform.localScale.y < MinSize)
        {
            GameOver = true;
            playerInteraction.Respawn();
            GameOver = false;
        }
    }

    public void Shrink()
    {
        if (isShrinking == true)
        {
            Vector3 shrink = new Vector3(0.1f, 0.1f, 0.1f);
            capsule.transform.localScale -= shrink;
        }
    }

    public void GrowBack()
    {
        if (capsule.transform.localScale != OriginalSize)
        {
            capsule.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
        }
    }

    public void SetGameOver(bool gameOver)
    {
        GameOver = gameOver;
    }

    public bool GetGameOver()
    {
        return GameOver;
    }

    public void SetIsShrinking(bool boolean)
    {
        isShrinking = boolean;
    }

    public void ResetSize()
    {
        capsule.transform.localScale = OriginalSize;
    }
}
