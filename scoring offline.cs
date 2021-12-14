 public class GameData
 {
     public int score;
     public string name;
     
 
     public GameData(int scoreInt, string nameStr)
     {
         score = scoreInt;
         name = nameStr;
        
     }
 }

 using System.IO;
 using System.Runtime.Serialization.Formatters.Binary;
 
 public class save : MonoBehaviour 
 {
     int currentScore = 0;
     string currentName = "name";
     
 
    
 
     public void SaveFile()
     {
         string destination = Application.persistentDataPath + "/save.dat";
         FileStream file;
 
         if(File.Exists(destination)) file = File.OpenWrite(destination);
         else file = File.Create(destination);
 
         GameData data = new GameData(currentScore, currentName, current);
         BinaryFormatter bf = new BinaryFormatter();
         bf.Serialize(file, data);
         file.Close();
     }
 
     public void LoadFile()
     {
         string destination = Application.persistentDataPath + "/save.dat";
         FileStream file;
 
         if(File.Exists(destination)) file = File.OpenRead(destination);
         else
         {
             Debug.LogError("File not found");
             return;
         }
 
         BinaryFormatter bf = new BinaryFormatter();
         GameData data = (GameData) bf.Deserialize(file);
         file.Close();
 
         currentScore = data.score;
         currentName = data.name;
        
 
         Debug.Log(data.name);
         Debug.Log(data.score);
         
     }
 
 }