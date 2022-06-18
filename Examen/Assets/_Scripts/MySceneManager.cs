using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public GameObject SceneButtonPanel;
    public GameObject BackButton;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        BackButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int i)
    {
        SceneButtonPanel.SetActive(false);
        BackButton.SetActive(true);
        SceneManager.LoadScene(i);
    }

    public void LoadStartScene()
    {
        SceneButtonPanel.SetActive(true);
        BackButton.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
