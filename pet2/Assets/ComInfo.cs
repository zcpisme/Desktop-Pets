using UnityEngine;
using System.Collections;
using System.Text;
using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;

public class ComInfo : MonoBehaviour
{

    private long memoryUsage;
    private Text myMemory;
    private double mycons = System.Math.Pow(1024,3);
    private double avaliableMb;
    private double TotMb;
    // Start is called before the first frame update
    void Start()
    {
        myMemory = GameObject.Find("Text").GetComponent<Text>();

    }

    // Update is called once per frame



    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        //系统内存总量
        public ulong ullTotalPhys;
        //系统可用内存
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;
    }

    //extern  调用 其他平台 dll 关键字
    [DllImport("kernel32.dll")]
    protected static extern void GlobalMemoryStatus(ref MEMORYSTATUSEX lpBuff);

    void Update()
    {
        MEMORYSTATUSEX ms = new MEMORYSTATUSEX();
        ms.dwLength = 64;
        GlobalMemoryStatus(ref ms);
        avaliableMb = ms.ullAvailPhys / mycons;
        TotMb=ms.ullTotalPhys/mycons;

        myMemory.text = "可用:"+avaliableMb.ToString("f1")+"GB";
        

    }

}