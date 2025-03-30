using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 instance;
    GameObject p1;
    GameObject p2;
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
        } else {
            if (currScene == "BlackHole" && p1 == null) {
                SetUp();
            }
        }
    }

    void MainUpdate() {
        if (Input.GetButtonDown("Jump")) {
            SceneManager.LoadScene("BlackHole");
            
        } else if (Input.GetButtonDown("Jump2")){
            //playCount = 2;
            SceneManager.LoadScene("BlackHole");
            }
    }

    public void EndGame(){
            SceneManager.LoadScene("BlackHole");

    }

    void SetUp() {
        p1 = GameObject.Find("Player1");
        if (p1 == null) {
            return;
        } /*
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
