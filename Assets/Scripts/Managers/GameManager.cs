using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public int tickRate = 20;
    public const int ticksPerDay = 2400;
    public float TicksPerMinute { get { return ticksPerDay / (24f * 60f); } }
    public saveData save;
    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(gameObject);
            return;
        }
        I = this;
        DontDestroyOnLoad(gameObject);
        save = GetDefaultSave();
    }
    saveData GetDefaultSave()
    {
        saveData dat = new saveData();
        dat.data = new gameData();
        dat.gold = 100;
        dat.currentDay = 0;
        dat.tickCount = 0;
        for (int i = 0; i < 20; i++) dat.data.tea.inventory.Add(new TeaInv { teaID = i, count = 3 });
        for (int i = 0; i < 40; i++) dat.data.garden.inventory.Add(new plantInv { plantID = i, count = 2 });
        return dat;
    }
    public void LoadGame()
    {
        Load();
        InvokeRepeating(nameof(Tick), 0, (1f / tickRate));
    }
    void Tick()
    {
        save.tickCount++;
        TeaSimulation.UpdateThis(save);
        int currentDay = (int)(save.tickCount / (float)ticksPerDay);
        if (currentDay > save.currentDay)
        {
            save.currentDay = currentDay;
            GardenSimulation.UpdateThis(save);
            Save();
        }
    }
    public long GetTicks(
   float amount,
   timeScale scale)
    {
        float ticks = 0f;

        switch (scale)
        {
            case timeScale.minute:
                ticks =
                    TicksPerMinute *
                    amount;
                break;

            case timeScale.day:
                ticks =
                    ticksPerDay *
                    amount;
                break;
        }

        return (long)Mathf.RoundToInt(
            ticks
        );
    }
    public void Load() => save = SaveManager.LoadData(save);
    public void Save() => SaveManager.SaveData(save);
}