using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;
    private Transform canvasTrans;
    private UIManager() 
    {
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        GameObject.DontDestroyOnLoad(canvas);
    }

    /// <summary>
    /// �洢�������������
    /// </summary>
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();
    
    public T ShowPanel<T> () where T : BasePanel
    {
        string panelName = typeof(T).Name;
        
        if (panelDic.ContainsKey(panelName) == false)
        {
            GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
            panelObj.transform.SetParent(canvasTrans, false);
            T panel = panelObj.GetComponent<T>();
            panelDic.Add(panelName, panel);
        }
        
        panelDic[panelName].ShowMe();
        return panelDic[panelName] as T;
    }

    public void HidePanel<T> (bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
        {
            if(isFade) 
            {
                panelDic[panelName].HideMe(() => 
                {
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(panelDic[panelName].gameObject);
                panelDic.Remove(panelName);
            }
            
        }

    }

    public T GetPanel<T> () where T : BasePanel
    {
        string panelName = typeof(T).Name;
        return panelDic.ContainsKey(panelName) ? panelDic[panelName] as T : null;
    }

    public BasePanel this[string panelName]
    {
        get 
        {
            return panelDic.ContainsKey(panelName) ? panelDic[panelName] : null;
        }
    }
}
