using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        Debug.Log("StartNewGame");
        SceneManager.LoadScene(1);
    }

    public void OnClickExit()
    {
        Application.Quit();
        Debug.Log("OnClickExit");
    }
}
