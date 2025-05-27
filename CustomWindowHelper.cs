using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;

namespace WPF_Projekt
{
    public static class CustomWindowHelper
    {
        // ===================== Maximize-Fix (WindowChromeFixer) =====================

        public static void AttachMaximizeFix(Window window)
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

        // ===================== Header-Interaktion (Drag, Doppelklick, Ziehen aus maximiert) =====================

        private static bool isMouseDown = false;
        private static Point mouseDownScreenPos;
        private static bool hasDraggedFromMaximized = false;
        private static double xRatio;
        private static double yRatio;

        public static void AttachHeaderInteraction(FrameworkElement headerArea, Window window)
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
                    mouseDownScreenPos = e.GetPosition(null); // relativ zur Screen-Wurzel
                    xRatio = e.GetPosition(headerArea).X / headerArea.ActualWidth;
                    yRatio = e.GetPosition(headerArea).Y / headerArea.ActualHeight;
                }
            };

            headerArea.MouseMove += (s, e) =>
            {
                if (!isMouseDown)
                    return;

                if (Mouse.LeftButton != MouseButtonState.Pressed)
                {
                    isMouseDown = false;
                    hasDraggedFromMaximized = false;
                    return;
                }

                var currentScreen = e.GetPosition(null);
                var dx = currentScreen.X - mouseDownScreenPos.X;
                var dy = currentScreen.Y - mouseDownScreenPos.Y;
                double delta = Math.Sqrt(dx * dx + dy * dy);

                const double dragThreshold = 4.0;
                if (!hasDraggedFromMaximized && delta < dragThreshold)
                    return;

                if (!hasDraggedFromMaximized)
                {
                    if (window.WindowState == WindowState.Maximized)
                    {
                        var screenPos = PointToScreen(headerArea, e.GetPosition(headerArea));
                        double newTop = screenPos.Y - (window.Height * yRatio);
                        double maxTop = SystemParameters.WorkArea.Top;

                        window.WindowState = WindowState.Normal;
                        window.Left = screenPos.X - (window.Width * xRatio);
                        window.Top = Math.Max(newTop, maxTop);
                    }

                    hasDraggedFromMaximized = true;
                }

                try
                {
                    if (Mouse.LeftButton == MouseButtonState.Pressed)
                        window.DragMove();
                }
                catch (InvalidOperationException) { }
            };

            headerArea.MouseLeftButtonUp += (s, e) =>
            {
                isMouseDown = false;
                hasDraggedFromMaximized = false;
            };
        }

        private static Point PointToScreen(Visual relativeTo, Point point)
        {
            return relativeTo.PointToScreen(point);
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

        // ===================== Fensterbutton-Ereignisse =====================

        public static void AttachButtonEvents(Window window, Button closeButton, Button minimizeButton, Button maximizeButton)
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

        // ===================== Resize Border je nach Zustand =====================

        public static void EnableSmartResizeBorder(Window window, Thickness normalThickness)
        {
            window.StateChanged += (_, _) =>
            {
                var chrome = WindowChrome.GetWindowChrome(window);
                if (chrome != null)
                {
                    if (window.WindowState == WindowState.Maximized)
                        chrome.ResizeBorderThickness = new Thickness(0);
                    else
                        chrome.ResizeBorderThickness = normalThickness;
                }
            };
        }

        // ===================== Haupt-API zum Anhängen aller Funktionen =====================

        public static void AttachAll(Window window,
                                     FrameworkElement header,
                                     Button closeButton,
                                     Button minimizeButton,
                                     Button maximizeButton,
                                     Thickness? resizeBorderThickness = null)
        {
            AttachMaximizeFix(window);
            AttachHeaderInteraction(header, window);
            AttachButtonEvents(window, closeButton, minimizeButton, maximizeButton);
            EnableSmartResizeBorder(window, resizeBorderThickness ?? new Thickness(5));
        }
    }
}
