using Const;  //定数を定義している
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ClickButtonScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip aucClick; //クリック音
    [SerializeField]
    private AudioClip aucInsert; //カードを挿入する音


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
        //Startの文字列を非表示
        this.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
        //クリック音を再生
        this.GetComponent<AudioSource>().PlayOneShot(aucClick);
        //クレジットカードを挿入するアニメーションを再生
        this.GetComponent<Animator>().Play(Const.CO.TitleCreditAnime);
        //アニメーションが終わった後のMainシーンに移動するようにアニメーターで設定
        
    }

    //アニメーション途中でATMにカードを挿入する時の音を再生
    public void InsertAudio()
    {
        this.GetComponent<AudioSource>().PlayOneShot(aucInsert);
    }
    //シーンジャンプする関数
    public void JumpScene(string Scene)
    {
        Debug.Log("Click");
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
            case "HowToPlay":
                SceneManager.LoadScene(Const.CO.HowToPlayScene);
                break;
        }
        
    }
}
