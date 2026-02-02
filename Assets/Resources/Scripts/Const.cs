using UnityEngine;
using System.Collections.Generic;

namespace Const
{ 
    //商品のクラスを生成
    public  class Goods
    {
        private string strName; //商品名
        private int intPrice; //商品の金額
        private string strEffectType; //商品の効果種類
        private int intEffectSize; //商品の効果
        private string strImageName; //商品画像名
        private Vector2 vec2ImageSize; //商品画像サイズ

        //商品のステータスを設定する
        public Goods SetStatus(string argstrName,int argintPrize, string argstrEffectType, int argintEffectSize,string argstrImageName,Vector2 argvec2ImageSize)
        {
            strName = argstrName;
            strImageName = argstrImageName;
            vec2ImageSize = argvec2ImageSize;
            intPrice =argintPrize;
            strEffectType=argstrEffectType;
            intEffectSize=argintEffectSize;
            return this;
        }
        //商品のステータスを取得する
        public string GetName()
        {
            return this.strName;
        }
        public string GetImageName()
        {
            return this.strImageName;
        }
        public Vector2 GetImageSize()
        {
            return this.vec2ImageSize;
        }
        public int GetPrice()
        {
            return this.intPrice;
        }
        public string GetEffectType()
        {
            return this.strEffectType;
        }
        public int GetEffectSize()
        {
            return this.intEffectSize;
        }
    }
    public static class CO
    {
        public static string GameVersion = "Ver 1.0.0"; //ゲームのバージョン情報

        #region シーン名
        public static string TitleScene = "TitleScene"; //タイトルシーンの名称
        public static string MainGameScene = "MainGameScene"; //メインゲーム画面のシーン名称
        #endregion
        #region テキストの色
        //数字がプラスの時のテキストカラー
        public static Color PlusColor = new Color(0, 224, 255, 255);
        //数字がマイナスの時のテキストカラー
        public static Color MinusColor = new Color(255, 0, 0, 255);
        //数字がゼロの時のテキストカラー
        public static Color ZeroColor = new Color(255, 255, 255, 255);
        #endregion

        #region object場所
        public static string GoodsControllObjectPass = "GameCanvas/BackGround/GoodsControllObject"; //全商品の親オブジェクトのGameControllerとの相対位置
        public static string GoodsNameTextPass = "GoodsName"; //商品名を表示しているテキストGoodsControllObjectとの相対位置    
        public static string GoodsImagePass = "GoodsImage"; //商品画像GoodsControllObjectとの相対位置    
        public static string GoodsPriceTextPass = "GoodsPrice/PriceText"; //商品の値段を表示しているテキストGoodsControllObjectとの相対位置
        public static string GoodsEffectTypeImagePass = "GoodsDetail/Image"; //商品の効果種類を表示している画像GoodsControllObjectとの相対位置
        public static string GoodsEffectSizeTextPass = "GoodsDetail/GoodsDetailText"; //商品の効果量を表示しているテキストGoodsControllObjectとの相対位置
        public static string PlayerStatusChangeTextPass = "ChangeText"; //プレイヤーのステータス変化量を表示しているテキスト、ステータスPanelとの相対位置
        public static string PlayerStatusRestTextPass = "RestText"; //プレイヤーのステータス残量を表示しているテキスト、ステータスPanelとの相対位置
        public static string YearTextPass = "YearText"; //現在の日付(年)テキスト、日付Objectとの相対位置
        public static string MonthTextPass = "MonthText"; //現在の日付(月)テキスト、日付Objectとの相対位置
        public static string DayTextPass = "DayText"; //現在の日付(日)テキスト、日付Objectとの相対位置
        public static string SalaryTextPass = "SalaryDateText"; //給料日テキスト、SettingObjectとの相対位置
        public static string WithdrawalTextPass = "WithdrawalDateText"; //引き落とし日テキスト、SettingObjectとの相対位置
        public static string ScoreDayTextPass = "DayText"; //最終スコア(経過日数)テキスト、FinishPanelとの相対位置
        public static string ScoreGetSalaryTextPass = "GetSalaryText"; //最終スコア(取得給与)テキスト、FinishPanelとの相対位置
        public static string ScoreUseSalaryTextPass = "UseSalaryText"; //最終スコア(消費給与)テキスト、FinishPanelとの相対位置
        public static string ScoreGetMentalTextPass = "GetMentalText"; //最終スコア(取得精神力)テキスト、FinishPanelとの相対位置
        public static string ScoreUseMentalTextPass = "UseMentalText"; //最終スコア(消費精神力)テキスト、FinishPanelとの相対位置
        public static string ScoreGetPhysicalTextPass = "GetPhysicalText"; //最終スコア(取得体力)テキスト、FinishPanelとの相対位置
        public static string ScoreUsePhysicalTextPass = "UsePhysicalText"; //最終スコア(消費体力)テキスト、FinishPanelとの相対位置
        public static string ScoreNumberTextPass = "ScoreNumberText"; //最終スコア(点数)テキスト、FinishPanelとの相対位置
        public static string ScoreRankTextPass = "ScoreRankText"; //最終スコア(ランク)テキスト、FinishPanelとの相対位置
        #endregion

        #region ゲーム上のステータス名
        public static string PlayerMoneyName = "残金"; //プレイヤーの残高に関するステータス名
        public static string PlayerMentalName = "精神力"; //プレイヤーの精神力に関するステータス名
        public static string PlayerPhysicalName = "体力"; //プレイヤーの体力に関するステータス名
        #endregion

        #region イメージ画像名
        public static string MoneyImageName = "JCB_CreditCard"; //プレイヤーの残高を表示するための画像名
        public static string MentalImageName = "Heart"; //プレイヤーの精神力を表示するための画像名
        public static string PhysicalImageName = "Dumbbell"; //プレイヤーの体力力を表示するための画像名
        public static string GoodsImageListPass = "Images/Goods/"; //商品画像を格納しているパス
        #endregion

        #region 商品の種類
        public static Goods Goods_A = new Goods().SetStatus("おにぎり", 300, PlayerPhysicalName, 5, "onigiri", new Vector2(80,66)); //商品A
        public static Goods Goods_B = new Goods().SetStatus("サンドイッチ", 180, PlayerPhysicalName, 2, "sandwich", new Vector2(80, 66)); //商品B
        public static Goods Goods_C = new Goods().SetStatus("ゲーム機", 3000, PlayerMentalName, 4, "NintendoSwitch", new Vector2(80, 66)); //商品C
        #endregion

        #region 商品のリスト
        public static List<Goods> GoodsList = new List<Goods>() {
            Goods_A,
            Goods_B,
            Goods_C,
            };//商品を格納しているリスト
        #endregion

        #region ランク表示
            public static string[,] RankList = new string[8, 2] {
                {"SS","100000" },
                {"S","800000" },
                {"A","500000" },
                {"B","30000" },
                {"C","10000" },
                {"D","8000" },
                {"E","5000" },
                {"F","0" },
            }; //ランク表示を格納しているリスト
        #endregion

        #region アニメーション名
            public static string OutGoodsAnime = "OutGoods"; //商品が画面外に移動するアニメーション名
            public static string InGoodsAnime = "InGoods"; //商品が画面内に移動するアニメーション名
            public static string PlusCahngeStatusAnime = "PlusCahngeStatus"; //プレイヤーのステータスが増加したときに表示するアニメーション名
            public static string MinusCahngeStatusAnime = "MinusCahngeStatus"; //プレイヤーのステータスが減少したときに表示するアニメーション名
        #endregion
    }

}
