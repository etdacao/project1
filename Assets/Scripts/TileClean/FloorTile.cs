using System.Collections;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [Header("Materials")]
    public Renderer tileRenderer;
    public Material dirtyMaterial;
    public Material cleanMaterial;

    [Header("Particles")]
    public GameObject dirtyParticles;
    public GameObject cleanParticles;

    [Header("Cleaning Settings")]
    public float holdTimeToClean = 2f; //浮点数 - 小数
    public float dirtyAgainTime = 5f;



    private bool playerOnTile = false; //布尔类型 - 是否，0/1
    private bool isClean = false;
    private float holdTimer = 0f;

    void Start()
    {
        SetDirtyState();
    }

    void Update()
    {
        if (playerOnTile && !isClean)
        {
            Debug.Log("Player is on a dirty tile. Hold space to clean.");
            if (Input.GetKey(KeyCode.Space))
            {
                holdTimer += Time.deltaTime;

                if (holdTimer >= holdTimeToClean)
                {
                    SetCleanState();

                    StartCoroutine(BeDirtyAgainAfterDelay());
                }
            }
            else
            {
                holdTimer = 0f;
            }
        }
    }
    

/// <summary>
/// custom function
/// </summary>
    //把地面设置成脏的，一次性
    void SetDirtyState()
    {
        isClean = false;
        holdTimer = 0f;

        if (tileRenderer != null && dirtyMaterial != null)
            tileRenderer.material = dirtyMaterial;

        if (dirtyParticles != null)
            dirtyParticles.SetActive(true);

        if (cleanParticles != null)
            cleanParticles.SetActive(false);
    }

    void SetCleanState()
    {
        isClean = true;
        holdTimer = 0f;

        if (tileRenderer != null && cleanMaterial != null)
            tileRenderer.material = cleanMaterial;

        if (dirtyParticles != null)
            dirtyParticles.SetActive(false);

        if (cleanParticles != null)
            cleanParticles.SetActive(true);

        Debug.Log(gameObject.name + "is cleaned!");
        Debug.Log(gameObject.name + "is cleaned!");
    }

    IEnumerator BeDirtyAgainAfterDelay()
    {
        yield return new WaitForSeconds(dirtyAgainTime);
        SetDirtyState();
        Debug.Log(gameObject.name + "is dirty again!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTile = true;
            Debug.Log("Player is on the" + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnTile = false;
            holdTimer = 0f;
        }
    }
}