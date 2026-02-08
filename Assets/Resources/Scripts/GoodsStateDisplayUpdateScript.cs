//表示されている商品の状態を更新するスクリプト

using Const;  //定数を定義している
using System.ComponentModel;
using TMPro; //名前空間を追加
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GoodsStateDisplayUpdateScript : MonoBehaviour
{
    [SerializeField]
    private Sprite sprMental; //精神力を表示する画像
    [SerializeField]
    private Sprite sprPhysical; //体力を表示する画像
    [SerializeField]
    private Sprite sprMoney; //お金を表示する画像

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //すべてのステータスを更新する
    public void StateUpdate_ALL(Transform TargetGoods, Goods goods)
    {
        StateUpdate_Name(TargetGoods, goods.GetName());
        StateUpdate_Image(TargetGoods, goods.GetImageName(), goods.GetImageSize());
        StateUpdate_Price(TargetGoods, goods.GetPrice());
        StateUpdate_EffectType(TargetGoods, goods.GetEffectType());
        StateUpdate_EffectSize(TargetGoods, goods.GetEffectSize());
    }
    //商品名を更新する
    public void StateUpdate_Name(Transform TargetGoods, string stringName)
    {
        //金額を画面上に反映
        TargetGoods.transform.Find(Const.CO.GoodsNameTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = stringName;
    }
    //商品画像を更新
    public void StateUpdate_Image(Transform TargetGoods, string stringImageName,Vector2 vec2ImageSize)
    {
        //商品画像を取得
        Sprite spImage = Resources.LoadAll<Sprite>(Const.CO.GoodsImageListPass + Const.CO.ImageListName)[int.Parse(stringImageName)];
        //商品画像を表示するオブジェクトを取得
        GameObject objGoodsImage = TargetGoods.transform.Find(Const.CO.GoodsImagePass).gameObject;
        objGoodsImage.GetComponent<SpriteRenderer>().sprite = spImage; //商品画像更新
        Debug.Log($"spImage:{spImage}、stringImageName：{stringImageName}");
        objGoodsImage.transform.localScale = vec2ImageSize; //商品サイズ更新
        
    }

    //金額のステータスを更新する
    public void StateUpdate_Price(Transform TargetGoods,int intPrice)
    {
        //金額を設定
        int intRandomPrice = (int)(intPrice * UnityEngine.Random.Range(0.85f,1.25f));
        //金額を画面上に反映
        TargetGoods.transform.Find(Const.CO.GoodsPriceTextPass).gameObject.GetComponent<TextMeshProUGUI>().text = intRandomPrice.ToString();
        
    }
    //効果タイプのステータスを更新する
    public void StateUpdate_EffectType(Transform TargetGoods,string strEffectType)
    {
        //画像を設定するオブジェクトを取得
        GameObject objEffectImage = TargetGoods.transform.Find(Const.CO.GoodsEffectTypeImagePass).gameObject;
        //画像のコンポーネントを取得
        SpriteRenderer sprEffectType = objEffectImage.GetComponent<SpriteRenderer>();
        
        
        //体力か精神力かで表示する画像を変更
        if (strEffectType == Const.CO.PlayerMentalName)
        {
            //精神力を設定
            sprEffectType.sprite = sprMental;
            GoodsBackColor(TargetGoods,Const.CO.MentalBackColor);
            //画像大きさを調整
            objEffectImage.transform.localScale = Const.CO.PlayerMentalSize;
        }
        else if(strEffectType == Const.CO.PlayerPhysicalName)
        {
            //体力を設定
            sprEffectType.sprite = sprPhysical;
            GoodsBackColor(TargetGoods,Const.CO.PhysicalBackColor);
            //画像大きさを調整
            objEffectImage.transform.localScale = Const.CO.PlayerPhysicalSize;
        }
        else if (strEffectType == Const.CO.PlayerMoneyName)
        {
            //体力を設定
            sprEffectType.sprite = sprMoney;
            GoodsBackColor(TargetGoods, Const.CO.MoneyBackColor);
            //画像大きさを調整
            objEffectImage.transform.localScale = Const.CO.PlayerMoneySize;
        }
    }
    //効果量のステータスを更新する
    public void StateUpdate_EffectSize(Transform TargetGoods,int intEffectSize)
    {
        //効果量をテキスト型に変更
        string strEffectSize = intEffectSize.ToString();
        //効果量を画面上に示すテキストを取得
        TextMeshProUGUI guiEffextSizeText = TargetGoods.transform.Find(Const.CO.GoodsEffectSizeTextPass).gameObject.GetComponent<TextMeshProUGUI>();

        //効果量が０の場合?に変更
        if (strEffectSize == "0") strEffectSize = Const.CO.RandomEffectSizeText;
        //効果量が正の数値である場合、先頭に+を追加
        else if (!strEffectSize.StartsWith("-")) strEffectSize = "+" + strEffectSize;

        //効果量を示すテキスト値を更新
        guiEffextSizeText.text = strEffectSize;

        //効果量が正の場合は青色、負の場合は赤色、0の場合は白色に変更
        if (guiEffextSizeText.text.StartsWith("-"))
        {
            guiEffextSizeText.color = Const.CO.MinusColor;
        }
        else if (guiEffextSizeText.text.StartsWith("+"))
        {
            guiEffextSizeText.color = Const.CO.PlusColor;
        }
        else
        {
            guiEffextSizeText.color = Const.CO.ZeroColor;
        }
    }

    public void GoodsBackColor(Transform TargetGoods,Color BackColor)
    {
        TargetGoods.gameObject.GetComponent<Image>().color = BackColor;
        
    }
}
