using Const;  //定数を定義している
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//説明に使用している情報をまとめたクラスを作成
public class Explanation
{
    private string ExplanationText; //説明文
    private Vector2 ExplanationPos; //説明文場所
    private Vector2 ArrowHandPos; //矢印場所
    private Vector2 ArrowHandScale; //矢印大きさ
    private Quaternion ArrowHandRotation; //矢印の角度
    private Vector2 FramePos; //強調表示の枠線場所
    private Vector2 FrameScale; //強調表示の大きさ


    //説明のステータスを設定する
    public Explanation SetStatus(string argExplanationText, Vector2 argExplanationPos,
        Vector2 argArrowHandPos, Vector2 argArrowHandScale, Quaternion argArrowHandRotation,
        Vector2 argFramePos, Vector2 argFrameScale)
    {
        ExplanationText = argExplanationText;
        ExplanationPos = argExplanationPos;
        ArrowHandPos = argArrowHandPos;
        ArrowHandScale = argArrowHandScale;
        ArrowHandRotation = argArrowHandRotation;
        FramePos = argFramePos;
        FrameScale = argFrameScale;

        return this;
    }

    //説明のステータスを取得する
    public string GetExplanationText() { return this.ExplanationText; }
    public Vector2 GetExplanationPos() { return this.ExplanationPos; }
    public Vector2 GetArrowHandPos() { return this.ArrowHandPos; }
    public Vector2 GetArrowHandScale() { return this.ArrowHandScale; }
    public Quaternion GetArrowHandRotation() { return this.ArrowHandRotation; }
    public Vector2 GetFramePos() { return this.FramePos; }
    public Vector2 GetFrameScale() { return this.FrameScale; }
}
public class HowToPlayScript : MonoBehaviour
{
    [SerializeField]
    private GameObject objExplanation; //説明文言が表示されているオブジェクト
    [SerializeField]
    private GameObject objPageText; //説明文言のページ数が表示されているオブジェクト
    [SerializeField]
    private GameObject objRedFrame; //赤色のフレームオブジェクト
    [SerializeField]
    private GameObject objArrowHand; //指さしを表示するオブジェクト
    [SerializeField]
    private GameObject objBackColor; //背景色を設定しているオブジェクト

    private int intClickNumber; //クリックした回数
    private GameObject objExplanationText; //説明文を表示しているオブジェクト

    //ゲーム画面に表示する説明(順番に表示)
    private Explanation[] ExplanationList = new Explanation[]
    {
        new Explanation().SetStatus("ゲーム説明",new Vector2(0,3.5f),
            new Vector2(0,0),new Vector2(0,0),Quaternion.Euler(0f, 0f, 0f),
            new Vector2(0,0),new Vector2(0,0)
            ),
        new Explanation().SetStatus("3つの商品の中から\n好きなものをクリックして購入",new Vector2(0,3.5f),
            new Vector2(-2f,1.3f),new Vector2(0.375f,0.375f),Quaternion.Euler(0f,0f,-60f),
            new Vector2(0,-0.7f),new Vector2(1.95f,1f)
            ),
        new Explanation().SetStatus("1つ商品を購入すると\n1日経過します",new Vector2(0,0.89f),
            new Vector2(-1.83f,2.95f),new Vector2(0.375f,0.375f),Quaternion.Euler(0f,-180f,45f),
            new Vector2(-2.34f,4.27f),new Vector2(0.4f,0.5f)
            ),
        new Explanation().SetStatus("商品を購入すると金額分\nクレジット使用量がたまります",new Vector2(0,-0.5f),
            new Vector2(-1f,2.35f),new Vector2(0.375f,0.375f),Quaternion.Euler(0f,0f,-60f),
            new Vector2(0,1.05f),new Vector2(2f,0.5f)
            ),
        new Explanation().SetStatus("購入すると記載されている\nステータス分上昇します",new Vector2(0,1.25f),
            new Vector2(-0.93f,-0.5f),new Vector2(0.375f,0.375f),Quaternion.Euler(0f,0f,-60f),
            new Vector2(0,-1.51f),new Vector2(1.95f,0.3f)
            ),
        new Explanation().SetStatus("?は数値がランダムで\n減少する可能性もあります",new Vector2(0,1.25f),
            new Vector2(1.14f,-0.62f),new Vector2(0.375f,0.375f),Quaternion.Euler(0f,0f,-60f),
            new Vector2(2f,-1.57f),new Vector2(0.55f,0.2f)
            ),
        new Explanation().SetStatus("購入金額以外は購入時に\n即時反映されます",new Vector2(0,-0.5f),
            new Vector2(0.29f,1.6f),new Vector2(0.375f,0.375f),Quaternion.Euler(0f,0f,45f),
            new Vector2(1.14f,3.66f),new Vector2(1.2f,1.2f)
            ),
        new Explanation().SetStatus("体力は時間経過で、精神力は\n経過日付で減少していきます",new Vector2(0,-0.5f),
            new Vector2(0.29f,1.6f),new Vector2(0.375f,0.375f),Quaternion.Euler(0f,0f,45f),
            new Vector2(1.14f,3.29f),new Vector2(1.2f,0.8f)
            ),
        new Explanation().SetStatus("購入金額は引き落とし日に\nまとめて反映されます",new Vector2(0,-0.98f),
            new Vector2(0.67f,-2.76f),new Vector2(0.375f,0.375f),Quaternion.Euler(0f,-180f,-60f),
            new Vector2(-1.05f,-3.84f),new Vector2(1.3f,0.5f)
            ),
        new Explanation().SetStatus("給料日になると\n残高が増加します",new Vector2(0,-0.98f),
            new Vector2(-0.07f,-4.39f),new Vector2(0.375f,0.375f),Quaternion.Euler(0f,-180f,45f),
            new Vector2(-1.05f,-2.73f),new Vector2(1.3f,0.5f)
            ),
        new Explanation().SetStatus("ステータスをマイナスにせず\n生き残り続けましょう",new Vector2(0,0f),
            new Vector2(0,0),new Vector2(0,0),Quaternion.Euler(0f, 0f, 0f),
            new Vector2(0,0),new Vector2(0,0)
            ),
    }; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        intClickNumber = 0; // クリック回数を初期化
        //説明文を表示しているオブジェクトを取得
        objExplanationText = objExplanation.transform.Find(Const.CO.ExplanationTextPass).gameObject;
        objPageText.GetComponent<TextMeshProUGUI>().text = "0/"+ (ExplanationList.Length - 1).ToString(); //ページ数のテキストを初期化

        //背景色を設定して表示
        objBackColor.GetComponent<Image>().color = Const.CO.BlackBackColor;
        //説明に使用しているオブジェクトを更新
        ChangeExplanation(ExplanationList[intClickNumber]);
    }

    public void ClickNext()
    {
        intClickNumber++; //クリックした回数を増加
        
        //説明文が1周している場合最初に戻す
        if (intClickNumber >= ExplanationList.Length) intClickNumber = 1;

        //最初の説明文を表示する際、背景色を透明に変更
        if (intClickNumber == 1) objBackColor.GetComponent<Image>().color = Const.CO.ClearBackColor;
        //最後の説明文を表示する際、背景色を黒に変更
        else if (intClickNumber == ExplanationList.Length - 1) objBackColor.GetComponent<Image>().color = Const.CO.BlackBackColor;

        //説明に使用しているオブジェクトを更新
        ChangeExplanation(ExplanationList[intClickNumber]);
        


    }

    public void ChangeExplanation(Explanation explanation)
    {
        //テキスト値を更新
        objExplanationText.GetComponent<TextMeshProUGUI>().text = explanation.GetExplanationText();
        //テキスト場所を更新
        objExplanation.transform.localPosition = explanation.GetExplanationPos();
        //矢印場所を更新
        objArrowHand.transform.localPosition = explanation.GetArrowHandPos();
        //矢印の大きさを更新
        objArrowHand.transform.localScale = explanation.GetArrowHandScale();
        //矢印の大きさを更新
        objArrowHand.transform.localRotation = explanation.GetArrowHandRotation();
        //強調表示場所を更新
        objRedFrame.transform.localPosition = explanation.GetFramePos();
        //強調表示の大きさを更新
        objRedFrame.transform.localScale = explanation.GetFrameScale();
        
        //ページ数のテキストを更新
        objPageText.GetComponent<TextMeshProUGUI>().text = intClickNumber+"/" + (ExplanationList.Length - 1).ToString(); 
    }
}
