using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class textGameManager : MonoBehaviour
{

    public TMP_InputField myInput;
    public string playerName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName()
    {
        playerName = myInput.text;
    }
}
