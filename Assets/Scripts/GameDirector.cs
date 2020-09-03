using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;


public class GameDirector : MonoBehaviour
{
    [HeaderAttribute("TextLabel")]
    public Text gameOverText;
    public Text gameClearText;
    public Text scoreText;

    [HeaderAttribute("TargetObject")]
    public GameObject boundTop;
    public GameObject boundLeft;
    public GameObject boundRight;
    public GameObject boundBottom;

    public GameObject startStage;

    private List<GameObject> bounds = new List<GameObject>();

    private GameObject[] blocks;

    // Start is called before the first frame update
    void Start()
    {
        this.gameOverText.gameObject.SetActive(false);
        this.gameClearText.gameObject.SetActive(false);
        this.scoreText.gameObject.SetActive(false);

        this.bounds.Add(boundTop);
        this.bounds.Add(boundLeft);
        this.bounds.Add(boundRight);
        this.bounds.Add(boundBottom);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < bounds.Count; i++)
        {
            var bound = this.bounds[i].GetComponent<BoundController>();
            if (bound.hasBlockHit == true)
            {
                this.gameOverText.gameObject.SetActive(true);
                bound.hasBlockHit = false;

                StartCoroutine(WaitReload());
            }
        }

        if (startStage.GetComponent<StartStageController>().stayBlockCount == 0 && this.blocks == null)
        {
            StartCoroutine(CheckObjectsHaveStopped());
        }
    }

    IEnumerator CheckObjectsHaveStopped()
    {
        print("checking... ");
        Rigidbody2D[] GOS = FindObjectsOfType(typeof(Rigidbody2D)) as Rigidbody2D[];
        bool allSleeping = false;

        while (!allSleeping)
        {
            allSleeping = true;

            foreach (Rigidbody2D GO in GOS)
            {
                if (!GO.IsSleeping())
                {
                    allSleeping = false;
                    yield return null;
                    break;
                }
            }
        }
        print("All objects sleeping");
        // Do something else

        if (startStage.GetComponent<StartStageController>().stayBlockCount == 0 && Input.GetMouseButton(0) == false)
        {
            if (this.blocks == null)
                this.blocks = GameObject.FindGameObjectsWithTag("Block");

            // List<float> scores = new List<float>();
            float score = 1.0f;

            foreach (GameObject block in blocks)
            {
                // scores.Add(Screen.height + block.transform.position.y);
                score *= Screen.height + block.transform.position.y;
            }
            // var score = scores.Sum();
            score /= 100000000000.0f;
            score -= 230000.0f;

            this.scoreText.text = score.ToString("0") + "点";

            this.gameClearText.gameObject.SetActive(true);
            this.scoreText.gameObject.SetActive(true);

            GameObject dragger = GameObject.Find("Dragger");
            Destroy(dragger);
        }
    }

    IEnumerator WaitReload()
    {
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
