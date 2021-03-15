using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSaver : MonoBehaviour
{
    public bool IsNewSaving = false;
    private GameManager gm;

    public enum SaveSlot{
        First, Second, Third,
    }

    public SaveSlot currentSaveSlot = SaveSlot.First;

    void Awake()
    {
        if(IsNewSaving)
            return;
        Load(currentSaveSlot);
    }
    void OnApplicationQuit() {
        Save(currentSaveSlot);
    }
    public void Load(SaveSlot saveSlot){
        FileStream fs = null;
        string path = Application.persistentDataPath + "/" + saveSlot.ToString() + ".tgb";
        if (File.Exists(path))
        {
            try
            {
                fs = new FileStream(path, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                var littleSuns = (Dictionary<int, bool>)bf.Deserialize(fs);
                LittleSunData.LittleSuns = littleSuns;
                var ability = (D_PlayerAbility.PlayerAbility)bf.Deserialize(fs);
                Player player = GameObject.Find("Player").GetComponent<Player>();
                player.playerAbilityData.SetPlayerAbility(ability);
                var runtimeData = (PlayerRuntimeData.PlayerRuntimeSaveData)bf.Deserialize(fs);
                player.playerRuntimeData.SetPlayerRuntimeSaveData(runtimeData);
                var enemyAliveRevivable = (Dictionary<int, bool>)bf.Deserialize(fs);
                EnemySaveData.EnemyAliveRevivable = enemyAliveRevivable;
                var enemyAliveUnrevivable = (Dictionary<int, bool>)bf.Deserialize(fs);
                EnemySaveData.EnemyAliveUnrevivable = enemyAliveUnrevivable;
            }
            catch (FileNotFoundException ex)
            {
                Debug.Log("On GameSaver::OnEnable, " + ex.StackTrace);
            }
            finally
            {
                fs?.Close();
            }
        }
    }
    public void Save(SaveSlot saveSlot){
        try
        {
            using(FileStream fs = new FileStream(Application.persistentDataPath + "/" + saveSlot.ToString() + ".tgb", FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(fs, LittleSunData.LittleSuns);
                Player player = GameObject.Find("/Player").transform.Find("Player").GetComponent<Player>();
                //Player player = GameObject.Find("Player").GetComponent<Player>();
                bf.Serialize(fs, player.playerAbilityData.GetPlayerAbility());
                bf.Serialize(fs, player.playerRuntimeData.GetPlayerRuntimeSaveData());
                bf.Serialize(fs, EnemySaveData.EnemyAliveRevivable);
                bf.Serialize(fs, EnemySaveData.EnemyAliveUnrevivable);

                fs.Close();
            }
        }
        catch(FileNotFoundException ex)
        {
            Debug.Log("On GameSaver::OnApplicationQuit, " + ex.StackTrace);
        }
    }
}
