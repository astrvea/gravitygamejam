using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;
    GameObject p1;
    GameObject p2;
    TMP_Text p1s_Text;
    string currScene;
    int p1_Score;
    //int p2_Score;
    //int playCount = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else { //If copy already exists, new copy is destroyed
            Destroy(this.gameObject);
            return;
        }
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        currScene = SceneManager.GetActiveScene().name;
        if (currScene == "MainMenu") {
            MainUpdate();
        } else if (currScene == "BlackHole" && p1 == null) {
                SetUp();
        } else if (currScene == "Lose" && p1s_Text == null) {
            EndGame();
        }
    }

    void MainUpdate() {
        if (Input.GetButtonDown("Jump")) {
            PlayerTest.score = 0;
            SceneManager.LoadScene("BlackHole");
        } 
    }

    void EndGame(){
        p1s_Text = GameObject.Find("Player1_Score").GetComponent<TMP_Text>();
        p1s_Text.text = "Score: " + PlayerTest.score.ToString();
    }

    void SetUp() {
        if (currScene == "BlackHole") {
        p1 = GameObject.Find("Player1");
        if (p1 == null) {
            return;
        }} else if (currScene == "Lose") {

        }
        /*
        p2 = GameObject.Find("Player2");
        if (playCount == 1){
                var kill = GameObject.Find("P2_UI");
                Destroy(p2); Destroy(kill);
                var cam = GameObject.Find("Cam 1");
                var cam2 = GameObject.Find("Cam 1_2");
                cam.SetActive(false);
                cam2.SetActive(true);
        } else {
                var cam2 = GameObject.Find("Cam 1_2");
                cam2.SetActive(true);
        } */
    }
 }
