using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace WPF_Projekt
{
    public static class CustomWindowHelper
    {
        // ========== WindowChromeFixer (Maximize-Bereich korrekt setzen) ==========
        public static void AttachChromeFix(Window window)
        {
            var hwnd = new WindowInteropHelper(window).Handle;
            HwndSource.FromHwnd(hwnd).AddHook(WindowProc);
        }

        private static IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_GETMINMAXINFO = 0x0024;
            if (msg == WM_GETMINMAXINFO)
            {
                WmGetMinMaxInfo(hwnd, lParam);
                handled = true;
            }
            return IntPtr.Zero;
        }

        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);
            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO();
                monitorInfo.cbSize = Marshal.SizeOf(typeof(MONITORINFO));
                GetMonitorInfo(monitor, ref monitorInfo);

                MINMAXINFO mmi = Marshal.PtrToStructure<MINMAXINFO>(lParam);

                RECT workArea = monitorInfo.rcWork;
                RECT monitorArea = monitorInfo.rcMonitor;

                mmi.ptMaxPosition.x = Math.Abs(workArea.left - monitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(workArea.top - monitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(workArea.right - workArea.left);
                mmi.ptMaxSize.y = Math.Abs(workArea.bottom - workArea.top);

                Marshal.StructureToPtr(mmi, lParam, true);
            }
        }

        private const int MONITOR_DEFAULTTONEAREST = 2;

        [DllImport("user32.dll")]
        private static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        [StructLayout(LayoutKind.Sequential)]
        private struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        private struct MONITORINFO
        {
            public int cbSize;
            public RECT rcMonitor;
            public RECT rcWork;
            public int dwFlags;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        // ========== WindowInteractionHelper (Drag, Doppelklick, aus Max ziehen) ==========

        private static bool isMouseDown = false;
        private static Point mouseDownScreenPosition;
        private static bool hasDraggedFromMaximized = false;

        public static void AttachHeaderBehavior(FrameworkElement headerArea, Window window)
        {
            headerArea.MouseLeftButtonDown += (s, e) =>
            {
                if (e.OriginalSource is DependencyObject source &&
                    FindParent<Button>(source) is not null)
                    return;

                if (e.ClickCount == 2)
                {
                    window.WindowState = window.WindowState == WindowState.Maximized
                        ? WindowState.Normal
                        : WindowState.Maximized;
                    return;
                }

                if (e.ButtonState == MouseButtonState.Pressed)
                {
                    isMouseDown = true;
                    hasDraggedFromMaximized = false;
                    mouseDownScreenPosition = headerArea.PointToScreen(e.GetPosition(headerArea));
                }
            };

            headerArea.MouseMove += (s, e) =>
            {
                if (!isMouseDown || hasDraggedFromMaximized)
                    return;

                Point currentScreen = headerArea.PointToScreen(e.GetPosition(headerArea));
                Vector delta = currentScreen - mouseDownScreenPosition;

                if (window.WindowState == WindowState.Maximized && delta.Length > 5)
                {
                    double xRatio = e.GetPosition(headerArea).X / headerArea.ActualWidth;
                    double yRatio = e.GetPosition(headerArea).Y / headerArea.ActualHeight;

                    window.WindowState = WindowState.Normal;
                    window.Left = currentScreen.X - (window.Width * xRatio);
                    window.Top = currentScreen.Y - (window.Height * yRatio);

                    hasDraggedFromMaximized = true;
                    window.DragMove();
                }
            };

            headerArea.MouseLeftButtonUp += (s, e) =>
            {
                isMouseDown = false;
                hasDraggedFromMaximized = false;
            };
        }

        private static T? FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (child != null)
            {
                if (child is T parent)
                    return parent;
                child = VisualTreeHelper.GetParent(child);
            }
            return null;
        }

        // ========== Fenster-Button-Events ==========

        public static void AttachDefaultButtonEvents(Window window,
                                                     Button closeButton,
                                                     Button minimizeButton,
                                                     Button maximizeButton)
        {
            closeButton.Click += (_, _) => window.Close();
            minimizeButton.Click += (_, _) => window.WindowState = WindowState.Minimized;
            maximizeButton.Click += (_, _) =>
            {
                window.WindowState = window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            };
        }
    }
}
