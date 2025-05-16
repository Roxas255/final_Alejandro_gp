using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
    
{
    public string sceneToLoad = "MainScene"; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
    
}
