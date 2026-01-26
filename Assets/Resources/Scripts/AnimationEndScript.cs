using UnityEngine;

public class AnimationEndScript : MonoBehaviour
{
    public bool blAnimation; //アニメーション中か判定
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        blAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationEnd()
    {
        blAnimation = false;
    }
}
