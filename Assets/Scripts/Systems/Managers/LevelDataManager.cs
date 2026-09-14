using System.IO;
using UnityEngine;

public class LevelDataManager : MonoBehaviour
{
    public static LevelDataManager Instance;
    string savePath;
    private void Awake()
    {
        Instance = this;
        // Windows'ta: %userprofile%\AppData\LocalLow\CompanyName\ProductName
        // Android'de: /storage/emulated/0/Android/data/package_name/files
        savePath = Application.persistentDataPath;
        Debug.Log($"Level Data Path: {savePath}");
    }
    public LevelData LoadLevelData(int levelID) 
    {
        string resourcePath = $"Level_{levelID}";
        TextAsset jsonFile = Resources.Load<TextAsset>(resourcePath);

        if (jsonFile != null)
        {
            try
            {
                // TextAsset içindeki metni alýyoruz
                string json = jsonFile.text;

                // JSON'u Class'a çeviriyoruz
                LevelData data = JsonUtility.FromJson<LevelData>(json);
                return data;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"JSON Parse Hatasý: {e.Message}");
                return null;
            }
        }
        else
        {
            Debug.LogError($"Resources klasöründe dosya bulunamadý! Aranan yol: Resources/{resourcePath}");
            return null;
        }
    }
    public void SaveLevelData(LevelData data)
    {
        // Poco veri yapýsý json'a çevirilecek.
        // Json veri text'e çevrilecek.
        // Dosyaya yazilacak.

        // 1. POCO veri yapýsý JSON string'e çevriliyor (Serialization)
        // 'true' parametresi, JSON'ýn okunabilir (Pretty Print) olmasýný saðlar.
        string json = JsonUtility.ToJson(data, true);

        // 2. Dosya yolu ve ismi belirleniyor
        // Path.Combine, iþletim sistemine göre doðru slash (/) iþaretini koyar.
        string fileName = $"Level_{data.levelID}.json";
        string fullPath = Path.Combine(savePath, fileName);

        // 3. Dosyaya yazýlýyor (Varsa üzerine yazar)
        try
        {
            File.WriteAllText(fullPath, json);
            Debug.Log($"Level {data.levelID} baþarýyla kaydedildi: {fullPath}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Kaydetme hatasý: {e.Message}");
        }
    }
    public int GetLevelCount()
    {
        int currentID = 1;
        int result = 0;
        while (true)
        {
            string resourcePath = $"Level_{currentID}";
            TextAsset jsonFile = Resources.Load<TextAsset>(resourcePath);
            if (jsonFile != null)
            {
                currentID++;
                result++;
            }
            else break;
        }
        return result;
    }
}
