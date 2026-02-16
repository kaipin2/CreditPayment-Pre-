using Const;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Transactions;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.U2D;

public class AWSDBScript : MonoBehaviour
{

    //public ItemList list;
    public GoodsData GoodsDataList;

    public IEnumerator GetGoodsList()
    {

        string apiUrl = "https://creditpayment-api.duckdns.org/index.php";// PHPスクリプトのURL

        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        //3秒後に完了していなければタイムアウト
        request.timeout = 3;

        yield return request.SendWebRequest();


        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
            //ローカル環境からデータを取得する
            LocalDataGet();
        }
        else
        {
            Debug.Log("Received: " + request.downloadHandler.text);
            //取得したJsonデータを日本語に修正
            string json = request.downloadHandler.text;

            // JSON配列をラップする
            json = "{ \"items\": " + json + "}";
            GoodsDataList.GoodsList = JsonConvert.DeserializeObject<ItemList>(json);

            //ImageURLからスプライトを作成

            foreach (Item item in GoodsDataList.GoodsList.items)
            {
                yield return GetGoodsImage(item, item.ImageURL);
                item.ImageSlace = new Vector2(100, 100);
            }
        }

        
    }



    //DBから画像を取得する
    public IEnumerator GetGoodsImage(Item item, string ImageURL)
    {
        Sprite sprite;
        UnityWebRequest imagerequest = UnityWebRequestTexture.GetTexture(ImageURL);
        yield return imagerequest.SendWebRequest();

        if (imagerequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(imagerequest.error);
        }
        else
        {
            Texture2D texture = DownloadHandlerTexture.GetContent(imagerequest);


            sprite = Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f)
            );

            item.ImageSprite = sprite;
        }

    }
    //AWSのDBに接続できなかった場合、ゲーム内に格納しているdataのみを使用
    private void LocalDataGet()
    {
        Sprite[] GoodsImageList = Resources.LoadAll<Sprite>(Const.CO.GoodsImageListPass + Const.CO.ImageListName);
        ItemList ItemList = new ItemList();
        ItemList.items = new Item[Const.CO.GoodsList.Count];

        int loopindex = 0;

        foreach (Goods goods in Const.CO.GoodsList) { 
            Item newitem = new Item();
            newitem.Id = loopindex;
            newitem.Name = goods.strName;
            newitem.Price = goods.intPrice;
            newitem.EffectType = goods.strEffectType;
            newitem.EffectSize = goods.intEffectSize;
            newitem.ImageSprite = GoodsImageList.FirstOrDefault(s => s.name == goods.strImageName);
            newitem.ImageSlace = goods.vec2ImageSize;

            ItemList.items[loopindex] = newitem;

            loopindex++;
        }
        GoodsDataList.GoodsList = ItemList;

    }
}