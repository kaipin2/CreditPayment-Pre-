//Mainゲーム画面で商品をクリックした際に行われるスクリプト

using TMPro; //名前空間を追加
using UnityEngine;
using UnityEngine.Audio;

public class GoodsClickActionScript : MonoBehaviour
{
    [SerializeField]
    private GameControllerScript gcsMainGameScripe;//メインゲームのスクリプトを持っているオブジェクト

    private GameObject ParentObj; //親オブジェクト
    private string strPriceText; //値段
    private string strEffectText; //効果数
    private string strEffectType; //効果種類
    private bool blAudioPlaying; //SEが再生させているか
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //商品達の親Objectを取得
        ParentObj = this.gameObject.transform.parent.gameObject;
        blAudioPlaying = false;//SEが再生されているかの関数を初期化


    }

    // Update is called once per frame
    void Update()
    {
        //SEが再生終了している場合
        if (blAudioPlaying && !gcsMainGameScripe.GetComponent<AudioSource>().isPlaying)
        {
            //残金、体力、精神力を更新
            gcsMainGameScripe.UpdateStatus(int.Parse(strPriceText), strEffectType, strEffectText);
            blAudioPlaying = false;//SEが再生されているかの関数を初期化
        }
    }

    public void ClickGoods()
    {
        //アニメーション中でない場合、実行
        if (!ParentObj.GetComponent<AnimationEndScript>().blAnimation)
        {
            //クリックした商品の値段、効果の種類、効果値を取得
            strPriceText = this.gameObject.transform.Find(Const.CO.GoodsPriceTextPass).gameObject.GetComponent<TextMeshProUGUI>().text;
            strEffectType = this.gameObject.transform.Find(Const.CO.GoodsEffectTypeImagePass).gameObject.GetComponent<SpriteRenderer>().sprite.name;
            strEffectText = this.gameObject.transform.Find(Const.CO.GoodsEffectSizeTextPass).gameObject.GetComponent<TextMeshProUGUI>().text;
            
            //Heartの文字列が入っている場合は精神力、Dumbbellの文字列が入っている場合は体力、Moneyの文字列が入っている場合は残金を増減
            if (strEffectType.Contains(Const.CO.MentalImageName))
            {
                strEffectType = Const.CO.PlayerMentalName;
            }
            else if(strEffectType.Contains(Const.CO.PhysicalImageName)){
                strEffectType = Const.CO.PlayerPhysicalName;
            }else if (strEffectType.Contains(Const.CO.MoneyImageName)){
                strEffectType = Const.CO.PlayerMoneyName;
            }

                //デバッグ用
                //print($"Goods:{name},Price:{strPriceText},Effect:{strEffectType}→{strEffectText}");

                //アニメーション中を示す変数をTrueにして、商品を移動させるアニメーションを実行
                ParentObj.GetComponent<AnimationEndScript>().blAnimation = true;
                ParentObj.GetComponent<Animator>().Play(Const.CO.OutGoodsAnime);

            //現在再生している音声を停止して商品を選んだ時のSEを再生
            gcsMainGameScripe.StopAndPlayAudio(gcsMainGameScripe.GetSelectGoodsSE());
            blAudioPlaying = true; //SE再生中に設定
            

        }
        
    }


}
