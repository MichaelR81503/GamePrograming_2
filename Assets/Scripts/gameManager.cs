using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    [Header("Global vars")]
    public GameObject myPlayer;
    public float timer;
    public float timeLimt;
    public int score;

    [Header("NPC vars")]
    public GameObject collectible1;
    public float spawnInterval;
    public float spawnTimer;
    public Vector2 spawnXBounds;
    public Vector2 spawnYBounds;

    [Header("UI/UX VARS")]
    public TextMeshProUGUI TitleText;


    public enum GameState
    {
        GAMESTART, PLAYING, GAMEOVER
    };

    public GameState mygameState;
    // Start is called before the first frame update
    void Start()
    {
        mygameState = GameState.GAMESTART;
        myPlayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        switch (mygameState)
        {
            case GameState.GAMESTART:
                if (Input.GetKey(KeyCode.Space))
                {
                    EnterPlaying();
                }
                break;

            case GameState.PLAYING:

                //timer is global, spawnTimer tracks collectibles
                timer += Time.deltaTime;
                spawnTimer += Time.deltaTime;

                if (timer > timeLimt)
                {
                    EnterFinal();
                }

                //this is the world position where our collectible spawns
                float x = Random.Range(spawnXBounds.x, spawnXBounds.y);
                float y = Random.Range(spawnYBounds.x, spawnYBounds.y);
                Vector3 targetPos = new Vector3(x, y, 0);

                //instantiate and reset timer when condition is met
                if (spawnTimer > spawnInterval)
                {
                    Instantiate(collectible1, targetPos, Quaternion.identity);
                    spawnTimer = 0;
                }
                break;

            case GameState.GAMEOVER:

                if (Input.GetKey(KeyCode.Space))
                {
                    EnterPlaying();
                }
                break;
        }
    }



    void EnterPlaying()
    {
        timer = 0;
        mygameState = GameState.PLAYING;
        myPlayer.SetActive(true);
        TitleText.enabled = false;
    }

    void EnterFinal()
    {
        mygameState = GameState.GAMEOVER;
        myPlayer.SetActive(false);
        TitleText.enabled = true;
        TitleText.text = "CONGRATS, You Survived. Press [SPACE] to restart";
    }
}
