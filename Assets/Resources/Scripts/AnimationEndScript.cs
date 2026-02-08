//アニメーションが終了しているかどうかを判定するスクリプト
using UnityEngine;
using TMPro; //名前空間を追加
using Const;  //定数を定義している

public class AnimationEndScript : MonoBehaviour
{
    public bool blAnimation; //アニメーション中か判定※他スクリプトでTrueにする
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //商品が画面内に入ってくるアニメーション終了時に実行
    public void InGoodsAnimationEnd()
    {
        blAnimation = false;
    }

    //商品が画面外に出ていくアニメーション終了時に実行
    public void OutGoodsAnimationEnd()
    {
        Resources.UnloadUnusedAssets(); //メモリ開放

        foreach (Transform child in this.gameObject.transform)
        {
            //商品のステータスを更新
            this.gameObject.GetComponent<GoodsStateDisplayUpdateScript>().StateUpdate_ALL(
                child,
                Const.CO.GoodsList[UnityEngine.Random.Range(0, Const.CO.GoodsList.Count)]);
        }
        //商品
        this.gameObject.GetComponent<Animator>().Play(Const.CO.InGoodsAnime);
    }
}
