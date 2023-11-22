using UnityEngine;

/// <summary>
/// Uma instância estática é semelhante a um singleton, mas em vez de destruir novas instâncias,
/// ele substitui a instância atual. Isso é útil para redefinir o estado e economiza fazendo
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
/// Isso transforma a instância estática em um singleton básico.
/// Isso destruirá qualquer nova versão criada, deixando o original intacto.
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
/// Perfeito para classes de sistema que exigem dados persistentes e com estado ou fontes de áudio
/// onde a música toca através de telas de carregamento, etc.
/// </summary>
public abstract class PersistentSingleton<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}

