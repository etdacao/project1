using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishWashingSystem : MonoBehaviour
{
    public List<GameObject> dirtyPlates = new List<GameObject>();

    public float spawnInterval = 5f;
    private bool playerInRange = false;

    void Start()
    {
        StartCoroutine(SpawnPlates());
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            WashPlate();
        }
    }

    void WashPlate()
    {
        if (dirtyPlates.Count > 0)
        {
            GameObject plate = dirtyPlates[dirtyPlates.Count - 1];
            dirtyPlates.RemoveAt(dirtyPlates.Count - 1);
            Destroy(plate);

            Debug.Log("ϴ��һ���룡");
        }
    }

    IEnumerator SpawnPlates()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // �����ȼ򵥴�ӡ��������Ըĳ�������
            Debug.Log("����һ�����룡");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("����ϴ������");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("�뿪ϴ������");
        }
    }
}