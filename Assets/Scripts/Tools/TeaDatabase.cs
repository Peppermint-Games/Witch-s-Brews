public class TeaDatabase{
  public static Tea newTea(int id){
    Tea newTea = new Tea();
    newTea.id = id;
    newTea.growTime = 5;
      switch(id){
        case 0:
          newTea.teaName = "English Breakfast";
          newTea.ingredients[0] = 2;
          newTea.ingredients[1] = 3;
          newTea.ingredients[2] = 4;
          newTea.ingredients[3] = 5;
          newTea.teaVibe = vibe.energetic;
            break;
      }
  }
}
