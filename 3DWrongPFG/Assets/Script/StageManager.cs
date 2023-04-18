using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public int saveStage = 3;

    // Start is called before the first frame update
    void Start()
    {
        ReLoadStage();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReLoadStage()
    {
        Debug.Log("ReLoadGame");
        //saveStage = PlayerPrefs.GetInt("saveStage"); // 불려올때
        // PlayerPrefs.SetInt("saveStage", 1); 저장할때 
        for (int num = 1; num <= saveStage; num++)
        {
            string stageName = "Stage" + num;
            Debug.Log(stageName);
            GameObject.Find(stageName).GetComponent<Button>().interactable = true;
        }
    }

    public void GetBtn()
    {
        GameObject tempBtn = EventSystem.current.currentSelectedGameObject;
        string stageNum = tempBtn.name;
        PlayerPrefs.SetString("nowStage", stageNum);
        SceneManager.LoadScene(2);
    }
}
