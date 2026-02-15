using Const;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
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
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + request.error);
            }
            else
            {
                Debug.Log("Received: " + request.downloadHandler.text);
            }

            //取得したJsonデータを日本語に修正
            string json = request.downloadHandler.text;

            // JSON配列をラップする
            json = "{ \"items\": " + json + "}";
            GoodsDataList.GoodsList = JsonConvert.DeserializeObject<ItemList>(json);

            //ImageURLからスプライトを作成

            foreach (Item item in GoodsDataList.GoodsList.items)
            {
                yield return GetGoodsImage(item, item.ImageURL);
            }
    }



    //DBから画像を取得する
    public IEnumerator GetGoodsImage(Item item,string ImageURL)
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
                new Vector2(0.5f,0.5f)
            );

            item.ImageSprite = sprite;
        }

        
    }
}