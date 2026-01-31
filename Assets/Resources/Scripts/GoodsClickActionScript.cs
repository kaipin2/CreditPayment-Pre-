//Mainゲーム画面で商品をクリックした際に行われるスクリプト

using UnityEngine;
using TMPro; //名前空間を追加

public class GoodsClickActionScript : MonoBehaviour
{
    [SerializeField]
    private GameControllerScript gcsMainGameScripe;

    private GameObject ParentObj; //親オブジェクト
    private string strPriceText; //値段
    private string strEffectText; //効果数
    private string strEffectType; //効果種類
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //商品達の親Objectを取得
        ParentObj = this.gameObject.transform.parent.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
            
            //Heartの文字列が入っている場合は精神力、Dumbbellの文字列が入っている場合は体力を増減
            if (strEffectType.Contains(Const.CO.MentalImageName))
            {
                strEffectType = Const.CO.PlayerMentalName;
            }
            else if(strEffectType.Contains(Const.CO.PhysicalImageName)){
                strEffectType = Const.CO.PlayerPhysicalName;
            }
            //デバッグ用
            print($"Goods:{name},Price:{strPriceText},Effect:{strEffectType}→{strEffectText}");
            
            //アニメーション中を示す変数をTrueにして、商品を移動させるアニメーションを実行
            ParentObj.GetComponent<AnimationEndScript>().blAnimation = true;
            ParentObj.GetComponent<Animator>().Play(Const.CO.OutGoodsAnime);

            //残金、体力、精神力を更新
            gcsMainGameScripe.UpdateStatus(int.Parse(strPriceText),strEffectType, int.Parse(strEffectText));

        }
        
    }


}
