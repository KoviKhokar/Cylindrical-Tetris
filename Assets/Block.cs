using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Vector3 initPos;
    public GameObject blockCollidedWith;
    private Manager manager;
    // Start is called before the first frame update
    private void Start()
    {
        initPos = transform.localPosition;
        manager = Camera.main.GetComponent<Manager>();
    }
    private void Update()
    {
        Fall();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == tag)
        {
            blockCollidedWith = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == blockCollidedWith)
        {
            blockCollidedWith = null;
        }
    }
    private void Fall()
    {
        if (gameObject.GetComponentInParent<Arangement>().enabled == false && manager.rowTriggerY < transform.position.y)
        {
            GameObject instance = Instantiate(this.gameObject, transform.position, transform.rotation);
            instance.transform.Translate(new Vector3(0, -0.5f, 0), Space.World);
            instance.GetComponent<SpriteRenderer>().enabled = false;
            if (instance.GetComponent<Block>().blockCollidedWith == null && instance.transform.position.y >= -4.75f)
            {
                Destroy(instance);
                transform.Translate(new Vector3(0, -0.5f, 0), Space.World);
            }
            else
            {
                Destroy(instance);
            }
        }
        if(gameObject.GetComponentInParent<Arangement>().enabled==false && transform.position.y >= 5.25) { manager.GameStatus = "Lose"; } 
    }
}