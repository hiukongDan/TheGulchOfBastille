using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;

public class GameSaver : MonoBehaviour
{
    private GameManager gm;
    public enum SaveSlot{
        First, Second, Third, SlotNum
    }

    private static string saveDirectory;

    Dictionary<int, SaveSlotMeta> saveSlotMetas;
    Dictionary<int, SaveSlotMeta> SaveSlotMetas{
        get{
            if(saveSlotMetas == null){
                LoadMeta();
            }
            return saveSlotMetas;
        }
    }
    [Serializable]
    public struct SaveSlotMeta{
        public int SceneCode;
        public float elapsedSeconds;
    };

    public SaveSlot currentSaveSlot = SaveSlot.First;
    public float autoSaveInterval = 600f;

    public bool isNewGame = true;

    void Awake(){
        gm = GetComponent<GameManager>();
        GameSaver.saveDirectory = Application.persistentDataPath;
    }

    private void LoadMeta(){
        string path = saveDirectory + "/meta.tgb";
        FileStream fs = null;
        if(File.Exists(path)){
            try{
                fs = new FileStream(path, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                var metaDict = (Dictionary<int, SaveSlotMeta>)bf.Deserialize(fs);
                saveSlotMetas = metaDict;
            }
            catch(FileNotFoundException ex){
                Debug.Log("On GameSaver::LoadMeta, " + ex.StackTrace);
            }
            finally{
                fs?.Close();
            }
        }
        else{
            InitMeta();
        }
    }

    private void InitMeta(){
        saveSlotMetas = new Dictionary<int, SaveSlotMeta>();
        for(int i = 0; i < (int)SaveSlot.SlotNum; ++i){
            SaveSlotMeta meta = new SaveSlotMeta();
            meta.SceneCode = (int)gm.currentSceneCode;
            meta.elapsedSeconds = 0f;

            saveSlotMetas.Add(i, meta);
        }
    }

    public void UpdateMeta(SaveSlot saveSlot, SaveSlotMeta meta){
        SaveSlotMetas[(int)saveSlot] = meta;
    }

    /// <summary>
    /// Use this to save meta infomation
    /// </summary>
    private void SaveMeta(){
        SaveSlotMeta meta = new SaveSlotMeta();
        meta.SceneCode = (int)gm.currentSceneCode;
        meta.elapsedSeconds = gm.elapsedSeconds;
        UpdateMeta(currentSaveSlot, meta);
        try{
            using(FileStream fs = new FileStream(saveDirectory + "/meta.tgb", FileMode.OpenOrCreate)){
                var bf = new BinaryFormatter();
                bf.Serialize(fs, saveSlotMetas);

                fs.Close();
            }
        }
        catch(FileNotFoundException ex){
            Debug.Log("On GameSaver::SaveMeta, " + ex.StackTrace);
        }
    }

    public void LoadAll(){
        Load(currentSaveSlot);
        LoadMeta();
    }
    
    public void Load(SaveSlot saveSlot){
        FileStream fs = null;
        string path = saveDirectory + "/" + saveSlot.ToString() + ".tgb";
        if (File.Exists(path))
        {
            try
            {
                fs = new FileStream(path, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                var littleSuns = (Dictionary<int, bool>)bf.Deserialize(fs);
                LittleSunData.LittleSuns = littleSuns;
                var playerSaveData = (D_PlayerStateMachine.PlayerSaveData)bf.Deserialize(fs);
                var ability = (D_PlayerAbility.PlayerAbility)bf.Deserialize(fs);
                Player player = GameObject.Find("Player").GetComponent<Player>();
                player.playerData.SetPlayerSaveData(playerSaveData);
                player.playerAbilityData.SetPlayerAbility(ability);
                var runtimeData = (PlayerRuntimeData.PlayerRuntimeSaveData)bf.Deserialize(fs);
                player.playerRuntimeData.SetPlayerRuntimeSaveData(runtimeData);
                var miscData = (MiscData)bf.Deserialize(fs);
                player.miscData = miscData;
                var enemyAliveRevivable = (Dictionary<int, bool>)bf.Deserialize(fs);
                EnemySaveData.EnemyAliveRevivable = enemyAliveRevivable;
                var enemyAliveUnrevivable = (Dictionary<int, bool>)bf.Deserialize(fs);
                EnemySaveData.EnemyAliveUnrevivable = enemyAliveUnrevivable;
            }
            catch (FileNotFoundException ex)
            {
                Debug.Log("On GameSaver::Load, " + ex.StackTrace);
            }
            finally
            {
                fs?.Close();
            }
        }
    }

    /// <summary>
    /// Use this to save game status
    /// </summary>
    public void Save(){
        Save(currentSaveSlot);
    }

    public void Save(SaveSlot saveSlot){
        try
        {
            using(FileStream fs = new FileStream(saveDirectory + "/" + saveSlot.ToString() + ".tgb", FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(fs, LittleSunData.LittleSuns);
                Player player = GameObject.Find("/Player").transform.Find("Player").GetComponent<Player>();
                //Player player = GameObject.Find("Player").GetComponent<Player>();
                bf.Serialize(fs, player.playerData.GetPlayerSaveData());
                bf.Serialize(fs, player.playerAbilityData.GetPlayerAbility());
                bf.Serialize(fs, player.playerRuntimeData.GetPlayerRuntimeSaveData());
                bf.Serialize(fs, player.miscData);
                bf.Serialize(fs, EnemySaveData.EnemyAliveRevivable);
                bf.Serialize(fs, EnemySaveData.EnemyAliveUnrevivable);

                fs.Close();
            }
        }
        catch(FileNotFoundException ex)
        {
            Debug.Log("On GameSaver::Save, " + ex.StackTrace);
        }
    }

    public void SaveAll(){
        Save();
        SaveMeta();
    }

    public bool HasValidSaving(SaveSlot slot){
        string path = saveDirectory + "/" + slot.ToString() + ".tgb";
        return File.Exists(path);
    }

    public bool HasValidSaving(){
        return HasValidSaving(SaveSlot.First) || HasValidSaving(SaveSlot.Second) || HasValidSaving(SaveSlot.Third);
    }

    public void AutoSave(){
        Invoke("AutoSave", autoSaveInterval);
        SaveAll();
    }

    public SaveSlotMeta GetSaveSlotMeta(SaveSlot saveSlot){
        return SaveSlotMetas[(int)saveSlot];
    }
}
