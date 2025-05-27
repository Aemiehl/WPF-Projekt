using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace WPF_Projekt
{
    public static class WindowInteractionHelper
    {
        private static bool isMouseDown = false;
        private static Point mouseDownScreenPosition;
        private static bool hasDraggedFromMaximized = false;

        public static void AttachDragBehavior(FrameworkElement headerArea, Window window)
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
    }
}
