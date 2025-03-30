using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject p1;
    public GameObject p2;
    public GameObject p1_UI;
    public TMP_Text p1_Score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {/*
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else { //If copy already exists, new copy is destroyed
            Destroy(this.gameObject);
            return;
        }*/
    }

    public void ScoreUp(int playerNum) {

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
