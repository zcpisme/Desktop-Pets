using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;
using System.IO;

public class TablePetBack : MonoBehaviour
{
    public string strProduct;//项目名称
    private int windowWidth = 500;//窗口宽度
    private int windowHeight = 700;//窗口高度
    private int currentX;
    private int currentY;
    #region Win函数常量
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
    [DllImport("user32.dll")]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [DllImport("user32.dll")]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
    [DllImport("user32.dll")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);
    [DllImport("user32.dll")]
    static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);
    [DllImport("user32.dll")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, int bAlpha, int dwFlags);
    [DllImport("Dwmapi.dll")]
    static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();
    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);




    private const int GWL_EXSTYLE = -20;
    private const int GWL_STYLE = -16;
    private const int WS_EX_LAYERED = 0x00080000;
    private const int WS_BORDER = 0x00800000;
    private const int WS_CAPTION = 0x00C00000;
    private const int SWP_SHOWWINDOW = 0x0040;
    private const int SWP_NOREDRAW = 0x0008;
    private const int SWP_NOMOVE = 0x0002;
    private const int LWA_COLORKEY = 0x00000001;
    private const int LWA_ALPHA = 0x00000002;
    private const int WS_EX_TRANSPARENT = 0x20;
    private const int ULW_COLORKEY = 0x00000001;
    private const int ULW_ALPHA = 0x00000002;
    private const int ULW_OPAQUE = 0x00000004;
    private const int ULW_EX_NORESIZE = 0x00000008;
    #endregion
    IntPtr hwnd;
    float xx;
    bool bx;
    // Use this for initialization
    void Awake()
    {
        Screen.fullScreen = false;
#if UNITY_EDITOR
        print("编辑模式不更改窗体");
#else
        hwnd = FindWindow(null, strProduct);
        //去边框并且透明
        SetWindowLong(hwnd, GWL_EXSTYLE, WS_EX_LAYERED);
        int intExTemp = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_BORDER & ~WS_CAPTION);

        //保持右下角间位置
        currentX = Screen.currentResolution.width - windowWidth-200;
        currentY = Screen.currentResolution.height - windowHeight -100;
        SetWindowPos(hwnd, -1, currentX, currentY, windowWidth, windowHeight, SWP_SHOWWINDOW);
        var margins = new MARGINS() { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hwnd, ref margins);
#endif
    }

    /* public int ChangeSpeed = 5;
     private int maximum = 40;
     private int minmum = 7; */

    //bool temp=true;
    //private int windowWidth2;

    void Update()
    {
        //鼠标滚轴缩放大小(轴值取值范围-1~1)
        //大于0时
     /*         if (Input.GetAxis("Mouse ScrollWheel") > 0)
              {
                  //照相机角度大于30时
                  if (Camera.main.fieldOfView > 10f)
                  {
                      //角度每次减少5
                      Camera.main.fieldOfView -= 5f;
                  }

              }
              //小于0时
              if (Input.GetAxis("Mouse ScrollWheel") < 0)
              {
                  //照相机角度小于90是
                  if (Camera.main.fieldOfView < 90f)
                  {
                      //角度每次加5
                      Camera.main.fieldOfView += 5f;
                  }
              }   */
          if (Input.GetAxis("Mouse ScrollWheel") > 0) //滚轴向上
          {
              windowWidth = windowWidth+10;
              windowHeight = (int)(windowWidth * 1.4);
            SetWindowPos(hwnd, -1, currentX, currentY, windowWidth, windowHeight, SWP_NOMOVE | SWP_NOREDRAW);
         //     var margins = new MARGINS() { cxLeftWidth = -1 };
         //     DwmExtendFrameIntoClientArea(hwnd, ref margins);
          }
          if (Input.GetAxis("Mouse ScrollWheel") < 0)  //滚轴向下
        {
              windowWidth = windowWidth - 10;
              windowHeight = (int)(windowWidth *1.4);
              SetWindowPos(hwnd, -1, currentX, currentY, windowWidth, windowHeight, SWP_NOMOVE | SWP_NOREDRAW);
         //     var margins = new MARGINS() { cxLeftWidth = -1 };
        //      DwmExtendFrameIntoClientArea(hwnd, ref margins);
          }  // */


          //拖动窗口
        if (Input.GetMouseButtonDown(2))
        {
            xx = 0f;
            bx = true;
        }
        if (bx && xx >= 0.1f)
        if (bx)
        { //这样做为了区分界面上面其它需要滑动的操作
            ReleaseCapture();
            SendMessage(hwnd, 0xA1, 0x02, 0);
            SendMessage(hwnd, 0x0202, 0, 0);
        }
        if (bx)
            xx += Time.deltaTime;  
        if (Input.GetMouseButtonUp(2))
        {
            xx = 0f;
            bx = false;
        }

        //更改窗口大小 以便显示菜单
        //if (Input.GetMouseButtonDown(1))
        //{
        //    if (temp)
        //    {
        //        windowWidth2 = windowWidth * 2;
        //        //windowHeight = (int)(windowWidth * 2.2);
        //        SetWindowPos(hwnd, -1, currentX, currentY, windowWidth2, windowHeight, SWP_NOMOVE | SWP_NOREDRAW);
        //        temp = false;
        //    }
        //    else 
        //    {
        //        windowWidth2 = windowWidth2/2;
        //        //windowHeight = (int)(windowWidth * 2.2);
        //        SetWindowPos(hwnd, -1, currentX, currentY, windowWidth2, windowHeight, SWP_NOMOVE | SWP_NOREDRAW);
        //        temp = true;
        //    }
            
        //}

    }


    void OnApplicationQuit()
    {
        SetWindowPos(hwnd, -1, currentX, currentY, 0, 0, SWP_SHOWWINDOW);
    }
}
