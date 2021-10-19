using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public string GameStatus = "Play";

    [SerializeField] Text Score;
    [SerializeField] Text HighScore;

    public float LeftBoundX;
    public float RightBoundX;

    public int playerScore = 0;

    public bool newArangementSpawnable = true;

    public float rowTriggerY;

    [SerializeField] private GameObject[] arangementprefs;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("highscore", 0);
        LeftBoundX = -2.5f;
        RightBoundX = 2.5f;
        InvokeRepeating(nameof(DespawnArangement), 0, 0.5f);
        InvokeRepeating(nameof(SpawnArangement), 0, 0.5f);
    }
    void LateUpdate()
    {
        transform.position = new Vector3((0.0f + LeftBoundX + RightBoundX) / 2, 0, -10);
        Score.text = "Score: " + playerScore.ToString();
        if(PlayerPrefs.GetInt("highscore") < playerScore)
        {
            PlayerPrefs.SetInt("highscore", playerScore);
        }
        HighScore.text = "High_Score: " + PlayerPrefs.GetInt("highscore").ToString();
        rowTriggerY = 100;
    }
    void SpawnArangement()
    {
        if (newArangementSpawnable)
        {
            int randominteger = Random.Range(0, 20);
            int randinteger = Random.Range(0, 6);




            GameObject instance1 = Instantiate(arangementprefs[randominteger], new Vector3(0, 5, 0), Quaternion.identity);
            SpriteRenderer[] isprr1 = instance1.GetComponentsInChildren<SpriteRenderer>();

            Color32[] availableColors = new Color32[7];
            availableColors[0] = new Color32(237, 25, 179, 255);
            availableColors[1] = new Color32(11, 241, 250, 255);
            availableColors[2] = new Color32(251, 242, 42, 255);
            availableColors[3] = new Color32(44, 220, 8, 255);
            availableColors[4] = new Color32(255, 151, 23, 255);
            availableColors[5] = new Color32(143, 23, 255, 255);
            availableColors[6] = new Color32(0, 89, 243, 255);

            foreach (SpriteRenderer sprr in isprr1)
            {
                sprr.color = availableColors[randinteger];
            }
            newArangementSpawnable = false;
        }
    }
    void DespawnArangement()
    {
        GameObject[] arangements = GameObject.FindGameObjectsWithTag("Arangement");
        foreach(GameObject arngmnt in arangements)
        {
            if (arngmnt.transform.childCount == 0) { Destroy(arngmnt); }
        }
    }
    public void PauseGame()
    {
        if (GameStatus == "Play")
        {
            GameStatus = "Pause";
            GameObject[] arangements = GameObject.FindGameObjectsWithTag("Arangement");
            foreach(GameObject arngmnt in arangements)
            {
                arngmnt.GetComponent<Arangement>().enabled = false;
            }
        }

        else if (GameStatus == "Pause")
        {
            GameStatus = "Play";
            GameObject[] arangements = GameObject.FindGameObjectsWithTag("Arangement");
            foreach (GameObject arngmnt in arangements)
            {
                arngmnt.GetComponent<Arangement>().enabled = true;
            }
        }
    }
}