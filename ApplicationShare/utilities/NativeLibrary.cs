using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ApplicationShare.utilities
{
    public static class NativeLibrary
    {
        public static List<RECT> SubtractRect(List<RECT> recs, RECT rect)
        {
            //RECT rn = new RECT { Left = rect.Left, Top = rect.Top, Right = rect.Right, Bottom = rect.Bottom };
            List<RECT> rnList = new List<RECT>();
            rnList.Add(rect);

            foreach (RECT r in recs)
            {
                List<RECT> newList = new List<RECT>();
                for (int idx = 0; idx < rnList.Count; idx++)
                {
                    RECT rn = rnList[idx];
                    //outside
                    if (r.Left >= rn.Right || r.Top >= rn.Bottom || r.Right <= rn.Left || r.Bottom <= rn.Top)
                    {
                        continue;
                    }

                    //within
                    if (r.Left > rn.Left && r.Top > rn.Top && r.Right < rn.Right && r.Bottom < rn.Bottom)
                    {
                        RECT r1 = new RECT();
                        r1.Left = rn.Left;
                        r1.Top = rn.Top;
                        r1.Right = r.Left;
                        r1.Bottom = rn.Bottom;
                        rnList[idx] = r1;

                        RECT r2 = new RECT();
                        r2.Left = r.Left;
                        r2.Top = rn.Top;
                        r2.Right = r.Right;
                        r2.Bottom = r.Top;
                        rnList.Insert(++idx, r2);

                        RECT r3 = new RECT();
                        r3.Left = r.Right;
                        r3.Top = rn.Top;
                        r3.Right = rn.Right;
                        r3.Bottom = rn.Bottom;
                        rnList.Insert(++idx, r3);

                        rn.Left = r.Left;
                        rn.Top = r.Bottom;
                        rn.Right = r.Right;
                        rnList.Insert(++idx, rn);
                        continue;
                    }

                    //full overlap
                    if (r.Left <= rn.Left && r.Top <= rn.Top && r.Right >= rn.Right && r.Bottom >= rn.Bottom)
                    {
                        rn.Left = 0;
                        rn.Top = 0;
                        rn.Right = 0;
                        rn.Bottom = 0;
                        rnList[idx] = rn;
                        continue;
                    }

                    //clip left side
                    if (r.Left <= rn.Left && r.Right < rn.Right && r.Top <= rn.Top && r.Bottom >= rn.Bottom)
                    {
                        int width = rn.Right - r.Right;
                        rn.Left = r.Right;
                        //rn.width = width;
                        rnList[idx] = rn;
                        continue;
                    }

                    //clip top side
                    if (r.Top <= rn.Top && r.Bottom < rn.Bottom && r.Left <= rn.Left && r.Right >= rn.Right)
                    {
                        int height = rn.Bottom - r.Bottom;
                        rn.Top = r.Bottom;
                        //rn.height = height;
                        rnList[idx] = rn;
                        continue;
                    }

                    //clip right side
                    if (r.Right >= rn.Right && r.Left > rn.Left && r.Top <= rn.Top && r.Bottom >= rn.Bottom)
                    {
                        //rn.width = r.Left - rn.Left;
                        //int width = r.Left - rn.Left;
                        rn.Right = r.Left;
                        rnList[idx] = rn;
                        continue;
                    }

                    //clip bottom side
                    if (r.Bottom >= rn.Bottom && r.Top > rn.Top && r.Left <= rn.Left && r.Right >= rn.Right)
                    {
                        //rn.height = r.Top - rn.Top;
                        //int height = r.Top - rn.Top;
                        rn.Bottom = r.Top;
                        rnList[idx] = rn;
                        continue;
                    }

                    //clip left top side
                    if (r.Right > rn.Left && r.Bottom > rn.Top && r.Left <= rn.Left && r.Top <= rn.Top)
                    {
                        RECT r1 = new RECT();
                        r1.Left = rn.Left;
                        r1.Top = r.Bottom;
                        r1.Right = r.Right;
                        r1.Bottom = rn.Bottom;
                        rnList[idx] = r1;
                        rn.Left = r.Right;
                        rnList.Insert(++idx, rn);
                        continue;
                    }

                    //clip right top side
                    if (r.Left < rn.Right && r.Bottom > rn.Top && r.Right >= rn.Right && r.Top <= rn.Top)
                    {
                        RECT r1 = new RECT();
                        r1.Left = r.Left;
                        r1.Top = r.Bottom;
                        r1.Right = rn.Right;
                        r1.Bottom = rn.Bottom;
                        rnList[idx] = r1;
                        rn.Right = r.Left;
                        rnList.Insert(++idx, rn);
                        continue;
                    }

                    //clip right buttom side
                    if (r.Left < rn.Right && r.Top < rn.Bottom && r.Right >= rn.Right && r.Bottom >= rn.Bottom)
                    {
                        RECT r1 = new RECT();
                        r1.Left = r.Left;
                        r1.Top = rn.Top;
                        r1.Right = rn.Right;
                        r1.Bottom = r.Top;
                        rnList[idx] = r1;
                        rn.Right = r.Left;
                        rnList.Insert(++idx, rn);
                        continue;
                    }

                    //clip left buttom side
                    if (r.Right > rn.Left && r.Top < rn.Bottom && r.Left <= rn.Left && r.Bottom >= rn.Bottom)
                    {
                        RECT r1 = new RECT();
                        r1.Left = rn.Left;
                        r1.Top = rn.Top;
                        r1.Right = r.Right;
                        r1.Bottom = r.Top;
                        rnList[idx] = r1;
                        rn.Left = r.Right;
                        rnList.Insert(++idx, rn);
                        continue;
                    }

                    //clip left middle side
                    if (r.Right > rn.Left && r.Top > rn.Top && r.Bottom < rn.Bottom && r.Left <= rn.Left)
                    {
                        RECT r1 = new RECT();
                        r1.Left = rn.Left;
                        r1.Top = rn.Top;
                        r1.Right = rn.Right;
                        r1.Bottom = r.Top;
                        rnList[idx] = r1;

                        if (r.Right < rn.Right)
                        {
                            RECT r2 = new RECT();
                            r2.Left = r.Right;
                            r2.Top = r.Top;
                            r2.Right = rn.Right;
                            r2.Bottom = r.Bottom;
                            rnList.Insert(++idx, r2);
                        }

                        rn.Top = r.Bottom;
                        rnList.Insert(++idx, rn);
                        continue;
                    }

                    //clip top middle side
                    if (r.Bottom > rn.Top && r.Left > rn.Left && r.Right < rn.Right && r.Top <= rn.Top)
                    {
                        RECT r1 = new RECT();
                        r1.Left = rn.Left;
                        r1.Top = rn.Top;
                        r1.Right = r.Left;
                        r1.Bottom = rn.Bottom;
                        rnList[idx] = r1;

                        if (r.Bottom < rn.Bottom)
                        {
                            RECT r2 = new RECT();
                            r2.Left = r.Left;
                            r2.Top = r.Bottom;
                            r2.Right = r.Right;
                            r2.Bottom = rn.Bottom;
                            rnList.Insert(++idx, r2);
                        }

                        rn.Left = r.Right;
                        rnList.Insert(++idx, rn);
                        continue;
                    }

                    //clip right middle side
                    if (r.Left < rn.Right && r.Top > rn.Top && r.Bottom < rn.Bottom && r.Right >= rn.Right)
                    {
                        RECT r1 = new RECT();
                        r1.Left = rn.Left;
                        r1.Top = rn.Top;
                        r1.Right = rn.Right;
                        r1.Bottom = r.Top;
                        rnList[idx] = r1;

                        if (r.Left > rn.Left)
                        {
                            RECT r2 = new RECT();
                            r2.Left = rn.Left;
                            r2.Top = r.Top;
                            r2.Right = r.Left;
                            r2.Bottom = r.Bottom;
                            rnList.Insert(++idx, r2);
                        }

                        rn.Top = r.Bottom;
                        rnList.Insert(++idx, rn);
                        continue;
                    }

                    //clip buttom middle side
                    if (r.Top < rn.Bottom && r.Left > rn.Left && r.Right < rn.Right && r.Bottom >= rn.Bottom)
                    {
                        RECT r1 = new RECT();
                        r1.Left = rn.Left;
                        r1.Top = rn.Top;
                        r1.Right = r.Left;
                        r1.Bottom = rn.Bottom;
                        rnList[idx] = r1;

                        if (r.Top > rn.Top)
                        {
                            RECT r2 = new RECT();
                            r2.Left = r.Left;
                            r2.Top = rn.Top;
                            r2.Right = r.Right;
                            r2.Bottom = r.Top;
                            rnList.Insert(++idx, r2);
                        }

                        rn.Left = r.Right;
                        rnList.Insert(++idx, rn);
                        continue;
                    }
                }
            }

            return rnList;
        }

        public static WINDOWPLACEMENT GetPlacement(IntPtr hwnd)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            GetWindowPlacement(hwnd, ref placement);
            return placement;
        }

        public static RECT GetWindowRect(IntPtr hwnd)
        {
            RECT placement = new RECT();
            GetWindowRect(hwnd, ref placement);
            return placement;
        }

        public static RECT GetClientRect(IntPtr hwnd)
        {
            RECT placement = new RECT();
            GetClientRect(hwnd, out placement);
            return placement;
        }

        public static RECT GetSubtractRect(RECT lprcSrc1, RECT lprcSrc2)
        {
            RECT result = new RECT();
            SubtractRect(out result, ref lprcSrc1, ref lprcSrc2);
            return result;
        }

        public const int SRCCOPY = 0x00CC0020; // BitBlt dwRop parameter
        public const int GCL_HICONSM = -34;
        public const int GCL_HICON = -14;

        public const int ICON_SMALL = 0;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL2 = 2;

        public const int WM_GETICON = 0x7F;

        public static IntPtr GetClassLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size > 4)
                return GetClassLongPtr64(hWnd, nIndex);
            else
                return new IntPtr(GetClassLongPtr32(hWnd, nIndex));
        }

        [DllImport("user32.dll", EntryPoint = "GetClassLong")]
        private static extern uint GetClassLongPtr32(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "GetClassLongPtr")]
        private static extern IntPtr GetClassLongPtr64(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);

        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImportAttribute("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, GetWindow_Cmd nCmdShow);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("User32")]
        public static extern IntPtr GetTopWindow(IntPtr hWnd);

        [DllImport("User32")]
        public static extern IntPtr GetNextWindow(IntPtr hWnd, uint wCmd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindow_Cmd uCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("gdi32.dll")]
        public static extern int GetClipBox(IntPtr hdc, out RECT lprc);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint GetWindowModuleFileName(IntPtr hwnd, StringBuilder lpszFileName, uint cchFileNameMax);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("gdi32.dll")]
        public static extern int CombineRgn(IntPtr hrgnDest, IntPtr hrgnSrc1, IntPtr hrgnSrc2, int fnCombineMode);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool SubtractRect(out RECT lprcDst, [In] ref RECT lprcSrc1, [In] ref RECT lprcSrc2);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetClientRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("User32.Dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ClientToScreen(IntPtr hwnd, ref System.Drawing.Point lpPoint);

        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
    }
    public enum GetWindow_Cmd : uint
    {
        GW_HWNDFIRST = 0,
        GW_HWNDLAST = 1,
        GW_HWNDNEXT = 2,
        GW_HWNDPREV = 3,
        GW_OWNER = 4,
        GW_CHILD = 5,
        GW_ENABLEDPOPUP = 6
    }

    public enum GetClipBoxReturn : int
    {
        Error = 0,
        NullRegion = 1,
        SimpleRegion = 2,
        ComplexRegion = 3
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public ShowWindowCommands showCmd;
        public System.Drawing.Point ptMinPosition;
        public System.Drawing.Point ptMaxPosition;
        public System.Drawing.Rectangle rcNormalPosition;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    public enum ShowWindowCommands : int
    {
        Hide = 0,
        Normal = 1,
        Minimized = 2,
        Maximized = 3,
    }
}
