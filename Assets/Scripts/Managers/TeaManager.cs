using System.Collections.Generic;
using UnityEngine;

public class TeaManager : MonoBehaviour{
  public static TeaManager I;
  void Awake(){
    I = this;
    BuildDatabase();
  }
  public List<Tea> teaDatabase = new List<Tea>();
  void BuildDatabase(){
  }
}
public class Tea{
  public string teaName;
  [TextArea]
  public string description;
  public int id, growTime;
  public int[] ingredients = new int[4];
  public vibe teaVibe;
  public timeScale scale = timeScale.minute;
}
