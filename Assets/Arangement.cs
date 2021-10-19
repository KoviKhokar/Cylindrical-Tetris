using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arangement : MonoBehaviour
{
    public Block[] childBlocks;
    private float hinput;
    private float vinput;
    private Manager manager;
    private float countDownToHalf;
    private float countDownToHalfHalf;

    void Start()
    {
        manager = Camera.main.GetComponent<Manager>();
        childBlocks = GetComponentsInChildren<Block>();
    }
    void Update()
    {
        countDownToHalf -= Time.deltaTime;
        if(countDownToHalf <= 0)
        {
            Fall();
            countDownToHalf = 0.5f;
        }
        countDownToHalfHalf -= Time.deltaTime;
        if (countDownToHalfHalf <= 0)
        {
            Move();
            countDownToHalfHalf = 0.25f;
        }

        hinput = Input.GetAxisRaw("Horizontal");
        vinput = Input.GetAxisRaw("Vertical");
        if (vinput == 1) { vinput = 0; }
        
        if (Input.GetMouseButtonUp(0))
        {
            bool allowed = true;


            GameObject instance = Instantiate(gameObject, transform.position, transform.rotation);
            Block[] ichildBlocks = instance.GetComponentsInChildren<Block>();


            foreach (Block block in childBlocks)
            {
                block.GetComponent<BoxCollider2D>().enabled = false;
            }


            foreach (Block iblock in ichildBlocks)
            {
                iblock.GetComponent<SpriteRenderer>().enabled = false;
                iblock.transform.localPosition = iblock.initPos;
                if (iblock.blockCollidedWith != null) { allowed = false; }
            }
            instance.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 90);
            foreach (Block iblock in ichildBlocks)
            {
                if (iblock.blockCollidedWith != null) { allowed = false; }
            }


            foreach (Block block in childBlocks)
            {
                block.GetComponent<BoxCollider2D>().enabled = true;
            }
            Destroy(instance);
            if (allowed) 
            {
                foreach(Block block in childBlocks)
                {
                    block.transform.localPosition = block.initPos;
                }
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + 90); 
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            bool allowed = true;


            GameObject instance = Instantiate(gameObject, transform.position, transform.rotation);
            Block[] ichildBlocks = instance.GetComponentsInChildren<Block>();


            foreach (Block block in childBlocks)
            {
                block.GetComponent<BoxCollider2D>().enabled = false;
            }


            foreach (Block iblock in ichildBlocks)
            {
                iblock.GetComponent<SpriteRenderer>().enabled = false;
                iblock.transform.localPosition = iblock.initPos;
                if (iblock.blockCollidedWith != null) { allowed = false; }
            }
            instance.transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90);
            foreach (Block iblock in ichildBlocks)
            {
                if (iblock.blockCollidedWith != null) { allowed = false; }
            }


            foreach (Block block in childBlocks)
            {
                block.GetComponent<BoxCollider2D>().enabled = true;
            }
            Destroy(instance);
            if (allowed)
            {
                foreach (Block block in childBlocks)
                {
                    block.transform.localPosition = block.initPos;
                }
                transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z - 90);
            }
        }
    }
    public void Move()
    {
        bool allowed = true;

        foreach(Block block in childBlocks)
        {
            if (block != null)
            {
                block.GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        GameObject instance = Instantiate(this.gameObject, transform.position, transform.rotation);
        instance.transform.Translate(new Vector2(hinput / 2, vinput / 2), Space.World);
        Block[] iblocks = instance.GetComponentsInChildren<Block>();
        foreach(Block iblock in iblocks)
        {
            if (iblock != null)
            {
                iblock.GetComponent<SpriteRenderer>().enabled = false;

                if (iblock.transform.position.x < manager.LeftBoundX)
                {
                    iblock.transform.position += new Vector3(5, 0, 0);
                }
                if (iblock.transform.position.x > manager.RightBoundX)
                {
                    iblock.transform.position -= new Vector3(5, 0, 0);
                }
                if (iblock.blockCollidedWith != null || iblock.transform.position.y < -4.75) { allowed = false; }
            }
        }

        if (allowed)
        {
            Destroy(instance);
            transform.Translate(new Vector2(hinput / 2, vinput / 2), Space.World);
            if (vinput == -1) { manager.playerScore++; }
            foreach (Block block in childBlocks)
            {
                if (block != null)
                {
                    block.GetComponent<BoxCollider2D>().enabled = true;
                    if (block.transform.position.x < manager.LeftBoundX)
                    {
                        block.transform.position += new Vector3(5, 0, 0);
                    }
                    if (block.transform.position.x > manager.RightBoundX)
                    {
                        block.transform.position -= new Vector3(5, 0, 0);
                    }
                }
            }
        }
        else
        {
            Destroy(instance);
            foreach (Block block in childBlocks)
            {
                if (block != null)
                {
                    block.GetComponent<BoxCollider2D>().enabled = true;
                }
            }
        }
    }
    public void Fall()
    {
        bool allowed = true;

        foreach (Block block in childBlocks)
        {
            if (block != null)
            {
                block.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        
        GameObject instance = Instantiate(this.gameObject, transform.position, transform.rotation);
        instance.transform.Translate(new Vector2(0, - 0.5f), Space.World);
        Block[] iblocks = instance.GetComponentsInChildren<Block>();
        foreach (Block iblock in iblocks)
        {
            if (iblock != null)
            {
                iblock.GetComponent<SpriteRenderer>().enabled = false;
                if (iblock.blockCollidedWith != null || iblock.transform.position.y < -4.75f) { allowed = false; }
            }
        }

        if (allowed)
        {
            Destroy(instance);
            transform.Translate(new Vector2(0, - 0.5f), Space.World);
        }
        foreach (Block block in childBlocks)
        {
            if (block != null)
            {
                block.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        if(!allowed)
        {
            Destroy(instance);
            manager.newArangementSpawnable = true;
            enabled = false;
        }
    }
}