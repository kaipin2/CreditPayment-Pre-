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

    //スコアのクラスを生成
    public class Score
    {
        private string ScoreTextPass;//スコアを表示するObjectPass
        private string ScoreText;//表示するスコアのテキスト※変数は<変数>で設定

        //Scoreのステータスを設定
        public Score SetStatus(string argScoreTextPass, string argScoreText)
        {
            ScoreTextPass = argScoreTextPass;
            ScoreText = argScoreText;
            return this;
        }

        //Scoreのステータスを取得する
        public string GetScoreTextPass() {return this.ScoreTextPass;}
        public string GetScoreText(string varScore = "") {
            return this.ScoreText.Replace("<変数>", varScore);
        }
    }
    //BGMやSEの名前と音量を設定するクラスを生成
    public class BGM_SE
    {
        private string AudioName;
        private float AudioVolume;

        //BGMやSEのステータスを設定
        public BGM_SE SetStatus(string argAudioName, float argAudioVolume)
        {
            AudioName = argAudioName;
            AudioVolume = argAudioVolume;
            return this;
        }

        //BGMやSEのステータスを取得する
        public string GetAudioName() {return AudioName;}
        public float GetAudioVolume() { return AudioVolume; }
    }

    public static class CO
    {
        public static string GameVersion = "Ver 1.0.0"; //ゲームのバージョン情報

        #region シーン名
        public static string TitleScene = "TitleScene"; //タイトルシーンの名称
        public static string MainGameScene = "MainGameScene"; //メインゲーム画面のシーン名称
        #endregion

        #region BGM名
        public static BGM_SE MainBGM = new BGM_SE().SetStatus("MainBGM",1); //メインのBGM
        public static BGM_SE EmergencyBGM = new BGM_SE().SetStatus("EmergencyBGM", 0.5f); //ステータスが危険な状態の時のBGM

        #endregion
        #region 色の設定
        //数字がプラスの時のテキストカラー
        public static Color PlusColor = new Color(0, 224, 255, 255);
        //数字がマイナスの時のテキストカラー
        public static Color MinusColor = new Color(255, 0, 0, 255);
        //数字がゼロの時のテキストカラー
        public static Color ZeroColor = new Color(255, 255, 255, 255);
        //精神力に作用する商品の背景色
        public static Color MentalBackColor = new Color(1f, 0.56f, 0.56f, 1f);
        //体力に作用する商品の背景色
        public static Color PhysicalBackColor = new Color(0.54f, 0.54f, 0.54f, 1f);
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
        public static string GoodsImageListPass = "Images/Goods/"; //商品画像を格納しているパス、Resourcesからの相対パス
        #endregion

        #region Audio保存場所
        public static string BMGListPass = "Audio/BGM/"; //BGMを格納しているパス、Resourcesからの相対パス
        #endregion
        #region ランダム効果商品
        public static string RandomEffectSizeText = "?"; //増加ステータスが決まっていない商品の表示
        #endregion

        #region 商品のリスト(ランダム効果は0で表示)
        public static List<Goods> GoodsList = new List<Goods>() {
            new Goods().SetStatus("おにぎり", 300, PlayerPhysicalName, 4, "Onigiri", new Vector2(80,66)),
            new Goods().SetStatus("サンドイッチ", 180, PlayerPhysicalName, 2, "Sandwich", new Vector2(80, 66)),
            new Goods().SetStatus("ゲーム機", 3000, PlayerMentalName, 8, "NintendoSwitch", new Vector2(80, 66)),
            new Goods().SetStatus("本", 100, PlayerMentalName, 1, "Book", new Vector2(40, 33)),
            new Goods().SetStatus("ステーキ", 1000, PlayerPhysicalName, 8, "Steak", new Vector2(100, 83)),
            new Goods().SetStatus("音楽", 250, PlayerMentalName, 2, "Music", new Vector2(100, 100)),
            new Goods().SetStatus("寝具", 500, PlayerMentalName, 3, "Bedding", new Vector2(100, 100)),
            new Goods().SetStatus("水晶", 0, PlayerMentalName, 0, "Crystal", new Vector2(60, 50)),
            new Goods().SetStatus("食べ放題", 1600, PlayerPhysicalName, 0, "All-you-can-eat", new Vector2(110, 91)),
            };//商品を格納しているリスト
        #endregion

        #region スコアのリスト
        public static List<Score> ScoreList = new List<Score> {
            new Score().SetStatus(ScoreDayTextPass,"経過日数：<変数>日"),
            new Score().SetStatus(ScoreGetSalaryTextPass,"取得金額：<変数>円"),
            new Score().SetStatus(ScoreUseSalaryTextPass,"購入金額：<変数>円"),
            new Score().SetStatus(ScoreGetMentalTextPass,"取得精神力：<変数>"),
            new Score().SetStatus(ScoreUseMentalTextPass,"消費精神力：<変数>"),
            new Score().SetStatus(ScoreGetPhysicalTextPass,"取得体力：<変数>"),
            new Score().SetStatus(ScoreUsePhysicalTextPass, "消費体力：<変数>"),
            new Score().SetStatus(ScoreNumberTextPass, "<変数>点"),
            new Score().SetStatus(ScoreRankTextPass, "<変数>")
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
            public static string TitleCreditAnime = "TitleCredit"; //タイトル画面からMain画面に移動するアニメーション名
            public static string OutGoodsAnime = "OutGoods"; //商品が画面外に移動するアニメーション名
            public static string InGoodsAnime = "InGoods"; //商品が画面内に移動するアニメーション名
            public static string PlusCahngeStatusAnime = "PlusCahngeStatus"; //プレイヤーのステータスが増加したときに表示するアニメーション名
            public static string MinusCahngeStatusAnime = "MinusCahngeStatus"; //プレイヤーのステータスが減少したときに表示するアニメーション名
        #endregion
    }

}
