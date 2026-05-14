using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Zenject;

public class CloudDataManager : MonoBehaviour
{
    [Header("Google Drive Settings")]
    [SerializeField] private string fileId = "ТВОЙ_ID_ФАЙЛА_ЗДЕСЬ";

    private string BaseUrl => $"https://drive.google.com/uc?export=download&id={fileId}";

    private PlayerConfig _zenjectConfig;

    [Inject]
    public void Construct(PlayerConfig zenjectConfig)
    {
        _zenjectConfig = zenjectConfig;
    }

    private async void Start()
    {
        await DownloadAndApplyConfig();
    }

    private async Task DownloadAndApplyConfig()
    {
        Debug.Log("Начинаю асинхронную загрузку конфигурации из облака...");

        if (_zenjectConfig == null)
        {
            Debug.LogWarning("CloudDataManager: Ссылка на PlayerConfig в Zenject равна null! Проверь GameInstaller.");
        }

        using (UnityWebRequest webRequest = UnityWebRequest.Get(BaseUrl))
        {
            var operation = webRequest.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

#if UNITY_2020_3_OR_NEWER
            if (webRequest.result == UnityWebRequest.Result.Success)
#else
            if (!webRequest.isHttpError && !webRequest.isNetworkError)
#endif
            {
                string json = webRequest.downloadHandler.text;

                if (_zenjectConfig != null)
                {
                    JsonUtility.FromJsonOverwrite(json, _zenjectConfig);

                    Debug.Log($"<color=green>CloudDataManager: Данные из JSON успешно применены! MaxHealth = {_zenjectConfig.MaxHealth}, BulletSpeed = {_zenjectConfig.BulletSpeed}</color>");
                }
            }
            else
            {
                Debug.LogError($"CloudDataManager: Ошибка загрузки конфигурации из облака: {webRequest.error}");
            }
        }
    }
}