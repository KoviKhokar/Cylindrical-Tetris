using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    [SerializeField] private GameObject[] heldBlocks = new GameObject[10];
    private Manager manager;
    private void Start()
    {
        manager = Camera.main.GetComponent<Manager>();
    }
    private void Update()
    {
        if (heldBlocks[9] != null)
        {
            foreach(GameObject block in heldBlocks)
            {
                Destroy(block);
            }
            manager.rowTriggerY = transform.position.y;
            manager.playerScore += 10;
            float boundUpdater = (float) Random.Range(-5, 5) / 2;
            manager.LeftBoundX += boundUpdater;
            manager.RightBoundX += boundUpdater;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            for (int x = 0; x <= heldBlocks.Length; x++)
            {
                if (heldBlocks[x] == null)
                {
                    heldBlocks[x] = collision.gameObject;
                    break;
                }
            }
        }   
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
        {
            for (int x = 0; x <= heldBlocks.Length; x++)
            {
                if (heldBlocks[x] == collision.gameObject)
                {
                    heldBlocks[x] = null;
                    break;
                }
            }
        }
    }
}
