using Const;  //定数を定義している
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class ClickButtonScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip aucClick; //クリック音
    [SerializeField]
    private AudioClip aucInsert; //カードを挿入する音

    private string strJumpScene; //ジャンプするシーン
    private bool blCanJump; //シーンジャンプできるか判定する関数
    private bool blDataLoad; //データ読み込み中か判定

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blCanJump = false; //初期化
        blDataLoad = false; //初期化
    }

    // Update is called once per frame
    void Update()
    {

        //ジャンプ可能で音声が再生終了している場合
        if (blCanJump && !blDataLoad&&!this.GetComponent<AudioSource>().isPlaying)
        {
            //Resources.UnloadUnusedAssets(); //メモリ開放
            switch (strJumpScene )
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

    //タイトルからMain画面に遷移する処理
    public void MainJumpFromTitle()
    {
        //Startの文字列を非表示
        this.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().enabled = false;
        //クリック音を再生
        this.GetComponent<AudioSource>().PlayOneShot(aucClick);
        //クレジットカードを挿入するアニメーションを再生
        this.GetComponent<Animator>().Play(Const.CO.TitleCreditAnime);

        blCanJump = true; //ジャンプ可能に変更
        //アニメーションが終わった後のMainシーンに移動するようにアニメーターで設定

    }

    //アニメーション途中でATMにカードを挿入する時の音を再生
    public IEnumerator InsertAudio()
    {
        blDataLoad = true; //データ読み込み中

        this.GetComponent<AudioSource>().PlayOneShot(aucInsert);

        //商品リストをAWSから取得
        yield return StartCoroutine(this.GetComponent<AWSDBScript>().GetGoodsList());
        blDataLoad = false; //データ読み込み完了
    }

    //シーンジャンプする関数
    public void JumpScene(string Scene)
    {
        //他でジャンプ判定していない場合クリック音を再生
        if(!blCanJump) this.GetComponent<AudioSource>().PlayOneShot(aucClick);
        
        strJumpScene = Scene; //シーンを設定
        blCanJump = true; //ジャンプ可能に変更
        
    }
}
