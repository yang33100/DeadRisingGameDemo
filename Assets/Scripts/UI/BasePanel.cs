using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float fadeSpeed = 10;
    private bool isShow = false;
    private UnityAction hideCallback;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null )
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        Init();
    }
    /// <summary>
    /// 必须实现的初始化
    /// </summary>
    public abstract void Init();

    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;
        isShow = true;
    }
    public virtual void HideMe(UnityAction callBack)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        hideCallback = callBack;
    }
    void Update()
    {
        if( isShow && canvasGroup.alpha != 1)
        {
            if (canvasGroup.alpha >= 1)
                canvasGroup.alpha = 1;
            else
                canvasGroup.alpha += fadeSpeed * Time.deltaTime;
        }
        else if( !isShow)
        {
            if (canvasGroup.alpha <= 0)
            {
                hideCallback?.Invoke(); 
            }
            
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
        }
    }
}
