public class TeaDatabase{
  public static Tea newTea(int id){
    Tea newTea = new Tea();
    newTea.id = id;
    newTea.growTime = 5;
      switch(id){
        case 0:
          newTea.teaName = "English Breakfast";
          newTea.ingredients[0] = 3;
          newTea.ingredients[1] = 4;
          newTea.ingredients[2] = 5;
          newTea.ingredients[3] = 6;
          newTea.teaVibe = vibe.cozy;
            break;
      }
  }
}
