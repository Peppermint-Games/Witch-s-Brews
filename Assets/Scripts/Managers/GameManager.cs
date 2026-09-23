using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public int tickRate = 20;
    public const int ticksPerDay = 12000;
    public float TicksPerHour => ticksPerDay / 24;
    public float TicksPerMinute => TicksPerHour / 60f;
    public float TicksPerSecond => TicksPerMinute / 60;
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
    public long GetTicks(float amount, timeScale scale)
    {
        float ticks = 0f;
        switch (scale)
        {
            case timeScale.second:
                ticks = TicksPerSecond * amount;
                break;
            case timeScale.minute:
                ticks = TicksPerMinute * amount;
                break;
            case timeScale.hour:
                ticks = TicksPerHour * amount;
                break;
            case timeScale.day:
                ticks = ticksPerDay * amount;
                break;
        }
        return (long)Mathf.RoundToInt(ticks);
    }
    public void Load() => save = SaveManager.LoadData(save);
    public void Save() => SaveManager.SaveData(save);
}