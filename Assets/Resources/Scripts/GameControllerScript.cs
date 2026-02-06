//ゲームで使用する変数やゲーム状況を監視するスクリプト
using Const;//定数を定義している
using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class GameControllerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject FinishPanel; //ゲーム結果を表示するobject
    [SerializeField]
    private GameObject MoneyPanel; //残金を表示するobject
    [SerializeField]
    private GameObject MentalPanel; //精神力を表示するobject
    [SerializeField]
    private GameObject PhysicalPanel; //体力を表示するobject
    [SerializeField]
    private GameObject DayPanel; //体力を表示するobject
    [SerializeField]
    private GameObject SettingPanel; //ゲーム設定を表示するobject
    [SerializeField]
    private AudioClip aucStatusTimer;　//タイマー音
    [SerializeField]
    private AudioClip aucStatusDec;　//ステータス減少音
    [SerializeField]
    private AudioClip aucStatusInc;　//ステータス増加音
    [SerializeField]
    private AudioClip aucMoneyDec;　//お金減少音
    [SerializeField]
    private AudioClip aucMoneyInc;　//お金増加音
    [SerializeField]
    private AudioClip aucSelectGoods; //商品の選択音
    [SerializeField]
    private AudioClip aucScoreDisplay; //スコアの表示音
    [SerializeField]
    private AudioSource ausBGMAudioSource;　//BGMのAudioSource

    private AudioClip[] aucBGMList;　//BGMリスト
    private int IntSalaryDay; // 給料日
    private int IntCreditDay; //クレジットカードの引き落とし日
    private int IntCreditMoney; //クレジットカード使用額
    private int IntMonthlyIncome; // 月収
    private int startMoney;  //初期残金
    private int oldMoney;  //計算前の残金
    private int Money;  //残金
    private int startMental;  //初期精神力
    private int oldMental;  //計算前の精神力
    private int Mental; //精神力
    private int startPhysical;  //初期体力
    private int oldPhysical;  //計算前の体力
    private int Physical; //体力
    private int CountDay; //経過日数
    private int IntGetMoney; //取得金額
    private int IntUseMoney; //消費金額
    private int IntGetMental; //取得精神力
    private int IntUseMental; //消費精神力
    private int IntGetPhysical; //取得体力
    private int IntUsePhysical; //消費体力
    private int LevelChangeTime;//消費量を変化させるタイミング
    private DateTime StartDay;　//初期の日付
    private DateTime CurrentDay;　//現在の日付
    private GameObject ObjGoodsControll; //全商品の親object
    private GameObject ObjChangeMoneyText; //残金の増減を表示するテキスト
    private GameObject ObjRestMoneyText; //残金の残りを表示するテキスト
    private GameObject ObjChangeMentalText; //精神力の増減を表示するテキスト
    private GameObject ObjRestMentalText; //精神力の残りを表示するテキスト
    private GameObject ObjChangePhysicalText; //体力の増減を表示するテキスト
    private GameObject ObjRestPhysicalText; //体力の残りを表示するテキスト
    private GameObject ObjMonthText; //現在の月を表示するテキスト
    private GameObject ObjDayText; //現在の日を表示するテキスト
    private GameObject ObjSalaryText; //給料日を表示するテキスト
    private GameObject ObjWithdrawalText; //引き落とし日を表示するテキスト
    private float CurrentTime = 0f; //現在の経過時間(spanごとにリセット)
    private float Span = 1f; // リセット間隔
    private bool blGameFinish = false; //ゲームが終了しているか格納する関数

    #region Public関数
    //商品選択音を返す関数
    public AudioClip GetSelectGoodsSE()
    {
        return aucSelectGoods;
    }

    
    //ステータスの更新処理
    public void UpdateStatus(int intPrice, string strEffectType, string strEffectNumber)
    {
        int EffectSize = 0; 
        IntCreditMoney += intPrice;　//クレジット使用額に購入商品分を足す

        //効果量が0の場合、ランダム設定
        if (strEffectNumber == Const.CO.RandomEffectSizeText) {
            //増減させるのが残金の場合
            if (strEffectType == Const.CO.PlayerMoneyName) {
                EffectSize = UnityEngine.Random.Range(-10000, 10000) * ((int)(CountDay / LevelChangeTime) + 1);
            }
            //増減させるのが残金以外の場合
            else
            {
                EffectSize = UnityEngine.Random.Range(-10, 10) *  ((int)(CountDay / LevelChangeTime) + 1);
            }
        }
        else EffectSize = int.Parse(strEffectNumber);//効果量を格納

        //商品の効果種類によって精神力か体力を更新
        if (strEffectType == Const.CO.PlayerMentalName)
        {
            Mental += EffectSize; //精神力を更新
            //精神力が減少する場合は消費変数を、増加する場合は取得変数を更新
            if (EffectSize < 0) IntUseMental += Mathf.Abs(EffectSize);
            else if (EffectSize > 0) IntGetMental += Mathf.Abs(EffectSize);
        }
        else if (strEffectType == Const.CO.PlayerPhysicalName)
        {
            Physical += EffectSize; //体力を更新
            //体力が減少する場合は消費変数を、増加する場合は取得変数を更新
            if (EffectSize < 0) IntUsePhysical += Mathf.Abs(EffectSize);
            else if (EffectSize > 0) IntGetPhysical += Mathf.Abs(EffectSize);
        }else if (strEffectType == Const.CO.PlayerMoneyName)
        {
            Money += EffectSize; //残金を更新
            //残金が減少する場合は消費変数を、増加する場合は取得変数を更新
            if (EffectSize < 0) IntUseMoney += Mathf.Abs(EffectSize);
            else if (EffectSize > 0) IntGetMoney += Mathf.Abs(EffectSize);
        }

            //日付を更新する際の処理
            DayCountUp();


    }

    //現在再生している音声を停止して、新しい音声を再生させる
    public void StopAndPlayAudio(AudioClip argausPlayAudio)
    {
        //現在再生されている音声を停止
        this.GetComponent<AudioSource>().Stop();
        //選択した音声を再生
        this.GetComponent<AudioSource>().PlayOneShot(argausPlayAudio);
    }
    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        #region TextObject取得
        ObjChangeMoneyText = MoneyPanel.transform.Find(Const.CO.PlayerStatusChangeTextPass).gameObject;
        ObjRestMoneyText = MoneyPanel.transform.Find(Const.CO.PlayerStatusRestTextPass).gameObject;
        ObjChangeMentalText = MentalPanel.transform.Find(Const.CO.PlayerStatusChangeTextPass).gameObject;
        ObjRestMentalText = MentalPanel.transform.Find(Const.CO.PlayerStatusRestTextPass).gameObject;
        ObjChangePhysicalText = PhysicalPanel.transform.Find(Const.CO.PlayerStatusChangeTextPass).gameObject;
        ObjRestPhysicalText = PhysicalPanel.transform.Find(Const.CO.PlayerStatusRestTextPass).gameObject;
        ObjMonthText = DayPanel.transform.Find(Const.CO.MonthTextPass).gameObject;
        ObjDayText = DayPanel.transform.Find(Const.CO.DayTextPass).gameObject;
        ObjSalaryText = SettingPanel.transform.Find(Const.CO.SalaryTextPass).gameObject;
        ObjWithdrawalText = SettingPanel.transform.Find(Const.CO.WithdrawalTextPass).gameObject;
        #endregion

        #region 変数設定
        StartDay = DateTime.Now; // 初期の日付を設定
        CurrentDay = StartDay; //現在の日付を初期化
        IntSalaryDay = 25; //給料日を設定
        IntCreditDay = 15;//クレジットカードの引き落とし日を設定
        IntMonthlyIncome = 10000; //月収を設定
        IntCreditMoney = 0; //クレジット使用額を初期化

        Money = IntMonthlyIncome; //初期残金を設定
        oldMoney = Money; //計算前残高を初期化
        startMoney = Money; //初期金額を格納

        Mental = 10; //初期精神力を設定
        oldMental = Mental; //計算前精神力を設定
        startMental = Mental; //初期精神力を格納

        Physical = 20; //初期体力を設定
        oldPhysical = Physical; // 計算前体力を設定
        startPhysical = Physical; // 初期体力を格納

        LevelChangeTime = 10; //消費量アップタイミングを初期化

        //スコア関係の値を初期化
        CountDay = 0; //経過日数を初期化
        IntGetMoney = 0; //取得金額を初期化
        IntUseMoney = 0; //消費金額を初期化
        IntGetMental = 0; //取得精神力を初期化
        IntUseMental = 0; //消費精神力を初期化
        IntGetPhysical = 0; //取得体力を初期化
        IntUsePhysical = 0; //消費体力を初期化
        #endregion

        #region 設定値を画面に表示
        ObjSalaryText.GetComponent<TextMeshProUGUI>().text = "毎月" + IntSalaryDay + "日";
        ObjWithdrawalText.GetComponent<TextMeshProUGUI>().text = "毎月" + IntCreditDay + "日";
        #endregion

        #region ステータスの背景色を設定
        MoneyPanel.GetComponent<Image>().color = Const.CO.MoneyBackColor;
        MentalPanel.GetComponent<Image>().color = Const.CO.MentalBackColor;
        PhysicalPanel.GetComponent<Image>().color = Const.CO.PhysicalBackColor;
        #endregion

        aucBGMList = Resources.LoadAll<AudioClip>(Const.CO.BMGListPass); //BGMのリストを取得

        FinishPanel.GetComponent<Canvas>().enabled = false; // 終了画面を非表示

        //商品の配置を行う

        ObjGoodsControll = this.transform.Find(Const.CO.GoodsControllObjectPass).gameObject;
       
        foreach (Transform child in ObjGoodsControll.transform)
        {
            ObjGoodsControll.GetComponent<GoodsStateDisplayUpdateScript>().StateUpdate_ALL(
                child,
                Const.CO.GoodsList[UnityEngine.Random.Range(0, UnityEngine.Random.Range(0, Const.CO.GoodsList.Count))]
            );
        }

        //初期ステータスを画面に表示
        DisPlayStatus();

        //MainのBGMを設定してBGMを再生
        ChengeBGMClip(Const.CO.MainBGM);
        
    }
    public void Update()
    {

        
        //アニメーション中でない場合、体力を1秒ごとに「経過日数に応じた値 + 1」減少
        if (!ObjGoodsControll.GetComponent<AnimationEndScript>().blAnimation && !blGameFinish)
        {
            CurrentTime += Time.deltaTime;//時間を計測
            if(CurrentTime > Span)
            {
                Physical-= (int)(CountDay / LevelChangeTime) + 1; //体力を経過日数に応じた値+1減少
                CurrentTime = 0f; //経過時間をリセット
                DisPlayStatus(aucStatusTimer); //画面に表示
                
            }
            
        }

    }
    
    //BGMリストから特定のBGMを再生する関数
    private void ChengeBGMClip(BGM_SE Audio)
    {
        //引数が設定されない場合、BGMを停止
        if (Audio == null) ausBGMAudioSource.Stop();
        else
        {
            string filename = Audio.GetAudioName(); //ファイル名を取得
            float Volume = Audio.GetAudioVolume(); //音量を取得

            ausBGMAudioSource.volume = Volume; //音量を設定

            //現在設定されているBGMが再生しようとしているBGMでない場合
            if (ausBGMAudioSource.clip == null || ausBGMAudioSource.clip.name != filename)
            {
                //Clipを修正してBGM再生
                ausBGMAudioSource.clip = aucBGMList.FirstOrDefault(clip => clip.name == filename);
                ausBGMAudioSource.Play();
            }
            //BGMは変わらないがすでに再生されていない場合、再度再生
            else if (!ausBGMAudioSource.isPlaying)
            {
                ausBGMAudioSource.Play();
            }
        }
        
    }
    //画面上ステータスを反映する関数
    private void DisPlayStatus(AudioClip aucSE = null)
    {
        //ステータス残量を画面に表示
        ObjRestMoneyText.GetComponent<TextMeshProUGUI>().text = Const.CO.PlayerMoneyName+"：" + Money;
        ObjRestMentalText.GetComponent<TextMeshProUGUI>().text = Const.CO.PlayerMentalName+"：" + Mental;
        ObjRestPhysicalText.GetComponent<TextMeshProUGUI>().text = Const.CO.PlayerPhysicalName+"：" + Physical;
        
        //更新した日付を画面に表示
        //ObjYearText.GetComponent<TextMeshProUGUI>().text = CurrentDay.Year.ToString()+"年";
        ObjMonthText.GetComponent<TextMeshProUGUI>().text = CurrentDay.Month.ToString() + "月";
        ObjDayText.GetComponent<TextMeshProUGUI>().text = CurrentDay.Day.ToString() + "日";

        //金額変化を表示
        StatusChange(Money - oldMoney, ObjChangeMoneyText, aucSE);
        oldMoney = Money; //計算前残高を更新

        //精神力変化を表示
        StatusChange(Mental - oldMental, ObjChangeMentalText, aucSE);
        oldMental = Mental; //計算前精神力を更新
        
        //体力変化を表示
        StatusChange(Physical - oldPhysical, ObjChangePhysicalText, aucSE);
        oldPhysical = Physical; //計算前体力を更新

        //残金、精神力、体力のいずれかが負の場合ゲーム終了
        if (Money < 0 || Mental < 0 || Physical < 0)
        {
            ChengeBGMClip(null); //BGMを停止する
            GameFinishCheck(); //ゲーム終了処理を実施
        }
        //精神力か体力が一割以下になった場合、BGMを危機的状況BGMに変更
        else if (startMental / 10 >= Mental || startPhysical / 10 >= Physical)
        {
            //危機的状況のBGMを設定してBGMを再生
            ChengeBGMClip(Const.CO.EmergencyBGM);
        }
        else
        {
            //MainのBGMを設定してBGMを再生
            ChengeBGMClip(Const.CO.MainBGM);
        }
    }

    //日付を更新する際に処理する関数
    private void DayCountUp()
    {
        AudioClip aucSE = null; //再生するSE

        //経過日数に+1
        CountDay++;
        //現在の日付を更新
        CurrentDay = StartDay.AddDays(CountDay);

        //一日たつごとに仕事でメンタル減少
        int intDecMental = UnityEngine.Random.Range(1, 3) + (int)(CountDay / LevelChangeTime);
        IntUseMental = intDecMental;
        Mental -= intDecMental;
        DisPlayStatus();

        //給料日の場合、給料を加算
        if (CurrentDay.Day == IntSalaryDay)
        {
            Debug.Log($"給料日");
            Money += IntMonthlyIncome;
            IntGetMoney += IntMonthlyIncome; //取得金額に給料を足す
            aucSE = aucMoneyInc; //お金増加音を設定
        }
        //クレジットカード引き落とし日の場合、残金を更新
        if (CurrentDay.Day == IntCreditDay)
        {
            Debug.Log($"引き落とし日");
            Money -= IntCreditMoney;
            IntUseMoney += IntCreditMoney; //消費金額に現在のクレジット使用量を足す
            IntCreditMoney = 0; //クレジット使用量を初期化
            aucSE = aucMoneyDec; //お金増加音を設定
        }

        //画面上のステータスを更新
        DisPlayStatus(aucSE);

        

    }

    //ステータスが変化したときに行う処理
    private void StatusChange(int Diff,GameObject StatusText, AudioClip aucSE)
    {
        if (Diff > 0)
        {
            StatusText.GetComponent<TextMeshProUGUI>().text = "+" + Diff.ToString();
            StatusText.GetComponent<Animator>().Play(Const.CO.PlusCahngeStatusAnime);
            if(!aucSE) aucSE = aucStatusInc;//増加音を設定
        }
        else if (Diff < 0)
        {
            StatusText.GetComponent<TextMeshProUGUI>().text = Diff.ToString();
            StatusText.GetComponent<Animator>().Play(Const.CO.MinusCahngeStatusAnime);
            if (!aucSE) aucSE = aucStatusDec;//減少音を設定
        }

        //ステータスに変化があった場合SEを再生
        if(Diff != 0) this.GetComponent<AudioSource>().PlayOneShot(aucSE);
    }

    //ゲームが終了している場合の処理
    private void GameFinishCheck()
    {
        blGameFinish = true; //ゲーム終了変数に値を格納
        FinishPanel.GetComponent<Canvas>().enabled = true; // 終了画面を表示

        //スコア点数
        int intScore = ScoreNumberCal();

        //表示するスコアの変数部分
        List<string> ScoreList = new List<string>
        {
            CountDay.ToString(),
            IntGetMoney.ToString(),
            IntUseMoney.ToString(),
            IntGetMental.ToString(),
            IntUseMental.ToString(),
            IntGetPhysical.ToString(),
            IntUsePhysical.ToString(),
            intScore.ToString(),
            ScoreRankCal(intScore)
        };

        //スコア表示
        int loopindex = 0; //ループ回数を設定
        foreach (Score score in Const.CO.ScoreList)
        {
            //テキストを画面に表示
            ScoreDisplay(
            FinishPanel.transform.Find(score.GetScoreTextPass()).gameObject.GetComponent<TextMeshProUGUI>(),
            score.GetScoreText(ScoreList[loopindex])
            );

            loopindex++;//インクリメント
        }

    }

    //スコア表示する関数
    private void ScoreDisplay(TextMeshProUGUI TextObject , string Score)
    {
        TextObject.enabled = true; //画面に表示する
        TextObject.text = Score; //テキストを設定
        //表示音を再生
        this.GetComponent<AudioSource>().volume = 0.3f;
        this.GetComponent<AudioSource>().PlayOneShot(aucScoreDisplay);
        
    }
    //スコア計算する関数
    private int ScoreNumberCal()
    {
        //出力用変数
        int intScore = 0;

        //経過日数×400
        intScore += 400 * CountDay;
        //購入金額×1%(少数点以下切り捨て)
        intScore += (int)(0.01f * IntUseMoney);
        //取得精神力×10%(少数点以下切り捨て)
        intScore += (int)(0.1f * IntGetMental);
        //取得体力×10%(少数点以下切り捨て)
        intScore += (int)(0.1f * IntGetPhysical);

        return intScore;
    }

    //スコアランクを計算する関数
    private string ScoreRankCal(int intScore)
    {
        string strScoreRank = "";
        for (int Rank = 0;Rank <  Const.CO.RankList.GetLength(0);Rank++){
            
            if(intScore >= int.Parse(Const.CO.RankList[Rank, 1]))
            {
                strScoreRank = Const.CO.RankList[Rank, 0];
                break;
            }
        }
        return strScoreRank;
    }
}
