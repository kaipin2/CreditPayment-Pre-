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

    //タイトルからMain画面に遷移する処理
    public void MainJumpFromTitle()
    {
        this.GetComponent<Animator>().Play(Const.CO.TitleCreditAnime);
        //JumpScene("Main");
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
            case "Demo":
                SceneManager.LoadScene("DemoScene");
                break;
        }
        
    }
}
