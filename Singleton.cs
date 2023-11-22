using UnityEngine;

/// <summary>
/// Uma inst�ncia est�tica � semelhante a um singleton, mas em vez de destruir novas inst�ncias,
/// ele substitui a inst�ncia atual. Isso � �til para redefinir o estado e economiza fazendo
/// isso manualmente.
/// </summary>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T instance { get; private set; }
    protected virtual void Awake() => instance = this as T;

    protected virtual void OnApplicationQuit()
    {
        instance = null;
        Destroy(gameObject);
    }
}

/// <summary>
/// Isso transforma a inst�ncia est�tica em um singleton b�sico.
/// Isso destruir� qualquer nova vers�o criada, deixando o original intacto.
/// </summary>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else base.Awake();
    }
}

/// <summary>
/// Persistente do singleton. Isso vai manter ao carregar uma scene
/// Perfeito para classes de sistema que exigem dados persistentes e com estado ou fontes de �udio
/// onde a m�sica toca atrav�s de telas de carregamento, etc.
/// </summary>
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}

