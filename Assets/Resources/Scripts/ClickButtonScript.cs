using UnityEngine;
using UnityEngine.SceneManagement;
using Const;  //’è”‚ğ’è‹`‚µ‚Ä‚¢‚é

public class ClickButtonScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartClick()
    {
        SceneManager.LoadScene(Const.CO.MainGameScene);
    }
}
