using UnityEngine;

public class EndScene : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");}
    }
}
