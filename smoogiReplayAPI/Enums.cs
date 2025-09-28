using System;

namespace ReplayAPI
{
    [Flags]
    public enum Keys
    {
        None = 0,
        M1 = (1 << 0),
        M2 = (1 << 1),
        K1 = (1 << 2) | (1 << 0),
        K2 = (1 << 3) | (1 << 1),
    }

    [Flags]
    public enum Mods
    {
        None = 0,
        NF = (1 << 0), //NoFail
        EZ = (1 << 1), //Easy
        TD = (1 << 2), //TouchDevice
        HD = (1 << 3), //Hidden
        HR = (1 << 4), //HardRock
        SD = (1 << 5), //SuddenDeath
        DT = (1 << 6), //DoubleTime
        RX = (1 << 7), //Relax
        HT = (1 << 8), //HalfTime
        NC = (1 << 9), //NightCore
        FL = (1 << 10), //FlashLight
        Auto = (1 << 11),
        SO = (1 << 12), //SpunOut
        AP = (1 << 13), //AutoPilot
        PF = (1 << 14), //Perfect
        K4 = (1 << 15), //Key4
        K5 = (1 << 16), //Key5
        K6 = (1 << 17), //Key6
        K7 = (1 << 18), //Key7
        K8 = (1 << 19), //Key8
        FI = (1 << 20), //FadeIn
        RD = (1 << 21), //Random
        CM = (1 << 22), //Cinema
        TP = (1 << 23), //TargetPractice
        K9 =  (1 << 24), //Key9
        CO = (1 << 25), //Coop
        K1 = (1 << 26), //Key1
        K3 = (1 << 27), //Key3
        K2 = (1 << 28), //Key2
        V2 = (1 << 29), //ScoreV2
        MR = (1 << 30) //Mirror
    }

    public enum GameModes
    {
        osu = 0,
        Taiko = 1,
        CtB = 2,
        Mania = 3
    }
}