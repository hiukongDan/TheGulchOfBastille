using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameSaver : MonoBehaviour
{
    private GameManager gm;
    void Awake()
    {
        FileStream fs = null;
        if (File.Exists(Application.persistentDataPath + "/default.tgb"))
        {
            try
            {
                fs = new FileStream(Application.persistentDataPath + "/default.tgb", FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();

                var littleSuns = (Dictionary<int, bool>)bf.Deserialize(fs);
                LittleSunData.LittleSuns = littleSuns;
                var ability = (D_PlayerAbility.PlayerAbility)bf.Deserialize(fs);
                GameObject.Find("Player").GetComponent<Player>().playerAbilityData.SetPlayerAbility(ability);
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

    void OnApplicationQuit()
    {
        try
        {
            using(FileStream fs = new FileStream(Application.persistentDataPath + "/default.tgb", FileMode.OpenOrCreate))
            {
                BinaryFormatter bf = new BinaryFormatter();

                bf.Serialize(fs, LittleSunData.LittleSuns);

                bf.Serialize(fs, GameObject.Find("Player").GetComponent<Player>().playerAbilityData.GetPlayerAbility());

                fs.Close();
            }
        }
        catch(FileNotFoundException ex)
        {
            Debug.Log("On GameSaver::OnApplicationQuit, " + ex.StackTrace);
        }
    }
}
