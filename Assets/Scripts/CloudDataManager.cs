using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using Unity.Entities;

[System.Serializable]
public class PlayerConfig
{
    public float MaxHealth;
    public float BulletSpeed;
}

public class CloudDataManager : MonoBehaviour
{
    [Header("Настройки Google Drive")]
    [Tooltip("Вставь сюда только ID файла из ссылки Google Drive")]
    public string fileId = "ТВОЙ_ID_ЗДЕСЬ";

    private string fileName = "GameConfig.json";
    private string BaseUrl => $"https://drive.google.com/uc?export=download&id={fileId}";

    void Start()
    {
        StartCoroutine(DownloadAndApplyConfig());
    }

    IEnumerator DownloadAndApplyConfig()
    {
        Debug.Log("Начинаю загрузку конфигурации...");

        using (UnityWebRequest webRequest = UnityWebRequest.Get(BaseUrl))
        {
            yield return webRequest.SendWebRequest();

            bool isError = false;
#if UNITY_2020_2_OR_NEWER
            if (webRequest.result != UnityWebRequest.Result.Success) isError = true;
#else
            if (webRequest.isNetworkError || webRequest.isHttpError) isError = true;
#endif

            if (isError)
            {
                Debug.LogWarning($"Не удалось скачать из облака ({webRequest.error}). Пробую загрузить локальную копию...");
            }
            else
            {
                string savePath = Path.Combine(Application.persistentDataPath, fileName);
                File.WriteAllText(savePath, webRequest.downloadHandler.text);
                Debug.Log($"<color=green>Конфиг обновлен и сохранен:</color> {savePath}");
            }
        }

        LoadLocalConfigAndApply();
    }

    void LoadLocalConfigAndApply()
    {
        string loadPath = Path.Combine(Application.persistentDataPath, fileName);

        if (File.Exists(loadPath))
        {
            string jsonContent = File.ReadAllText(loadPath);
            PlayerConfig config = JsonUtility.FromJson<PlayerConfig>(jsonContent);

            Debug.Log($"<color=cyan>Данные из JSON применены:</color> MaxHealth = {config.MaxHealth}");

            ApplyToECS(config);
        }
        else
        {
            Debug.LogError("Файл конфигурации не найден ни в облаке, ни на диске!");
        }
    }

    void ApplyToECS(PlayerConfig config)
    {
        if (World.DefaultGameObjectInjectionWorld == null) return;

        var em = World.DefaultGameObjectInjectionWorld.EntityManager;
        var query = em.CreateEntityQuery(typeof(Health), typeof(PlayerTag));

        using (var entities = query.ToEntityArray(Unity.Collections.Allocator.TempJob))
        {
            if (entities.Length > 0)
            {
                var h = em.GetComponentData<Health>(entities[0]);
                h.Max = config.MaxHealth;
                h.Current = config.MaxHealth;
                em.SetComponentData(entities[0], h);
            }
        }
    }
}
