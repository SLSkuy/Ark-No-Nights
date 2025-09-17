using UnityEngine;

public class BasePanel<T> : MonoBehaviour where T : class
{
    public static T Instance { get; private set; }

    void Awake()
    {
        Instance = this as T;
    }

    public virtual void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }

    public virtual void HidePanel()
    {
        this.gameObject.SetActive(false);
    }
}
