using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static bool IsQuitting = false;

    public static T Instance
    {
        get
        {
            if (IsQuitting)
            {
                _instance = null;
            }

            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();

                if (_instance == null)
                {
                    Debug.LogError($"{typeof(T).Name} is not exits");
                }
                else
                {
                    IsQuitting = false;
                }

            }
            return _instance;

        }
    }

    private void OnDisable()
    {
        IsQuitting = true;
        _instance = null;
    }
}