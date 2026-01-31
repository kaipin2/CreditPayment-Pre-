using UnityEngine;
using UnityEngine.SceneManagement;
using Const;  //定数を定義している

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

    
    //シーンジャンプする関数
    public void JumpScene(string Scene)
    {
        switch (Scene)
        {
            case "Main":
                SceneManager.LoadScene(Const.CO.MainGameScene);
                break;
            case "Title":
                SceneManager.LoadScene(Const.CO.TitleScene);
                break;
        }
        
    }
}
