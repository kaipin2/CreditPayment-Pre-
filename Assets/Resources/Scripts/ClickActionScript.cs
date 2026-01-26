using UnityEngine;
using TMPro; //名前空間を追加

public class ClickActionScript : MonoBehaviour
{
    private GameObject ParentObj; //親オブジェクト
    private string strPriceText; //値段
    private string strEffectText; //効果数
    private string strEffectType; //効果種類
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ParentObj = this.gameObject.transform.parent.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickGoods()
    {
        if (!ParentObj.GetComponent<AnimationEndScript>().blAnimation)
        {
            strPriceText = this.gameObject.transform.Find("GoodsPrice/PriceText").gameObject.GetComponent<TextMeshProUGUI>().text;
            strEffectType = this.gameObject.transform.Find("GoodsDetail/Image").gameObject.GetComponent<SpriteRenderer>().sprite.name;
            strEffectText = this.gameObject.transform.Find("GoodsDetail/GoodsDetailText").gameObject.GetComponent<TextMeshProUGUI>().text;
            //Heartの文字列が入っている場合は精神力、Dumbbellの文字列が入っている場合は体力を増減
            if (strEffectType.Contains("Heart"))
            {
                strEffectType = "精神力";
            }
            else if(strEffectType.Contains("Dumbbell")){
                strEffectType = "体力";
            }
                print($"Goods:{name},Price:{strPriceText},Effect:{strEffectType}→{strEffectText}");
            ParentObj.GetComponent<AnimationEndScript>().blAnimation = true;
            ParentObj.GetComponent<Animator>().Play("OutGoods");
        }
        
    }


}
