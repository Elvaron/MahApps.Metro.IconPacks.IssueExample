namespace IssueExample
{
    #region using

    using System.Windows;

    using MahApps.Metro.IconPacks;

    #endregion

    public class LogEntry : DependencyObject
    {
        public static readonly DependencyProperty IconColorProperty =
            DependencyProperty.Register("IconColor", typeof(string), typeof(LogEntry), new UIPropertyMetadata("#FFFFFF"));

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon",
            typeof(PackIconModernKind),
            typeof(LogEntry),
            new UIPropertyMetadata(PackIconModernKind.StarwarsRebel));

        public static readonly DependencyProperty SpinDurationProperty =
            DependencyProperty.Register("SpinDuration", typeof(int), typeof(LogEntry), new UIPropertyMetadata(0));

        public static readonly DependencyProperty SpinProperty = DependencyProperty.Register("Spin", typeof(bool), typeof(LogEntry), new UIPropertyMetadata(true));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(LogEntry), new UIPropertyMetadata(string.Empty));

        public LogEntry(string text, PackIconModernKind icon, string iconColor, bool spin, string uniqueId, int spinDuration)
        {
            Text = text ?? string.Empty;
            Icon = icon;
            IconColor = iconColor ?? "#FFFFFF";
            Spin = spin;
            SpinDuration = spinDuration;
            UniqueId = uniqueId;
        }

        public PackIconModernKind Icon { get => (PackIconModernKind)GetValue(IconProperty); set => SetValue(IconProperty, value); }

        public string IconColor { get => (string)GetValue(IconColorProperty); set => SetValue(IconColorProperty, value); }

        public bool Spin { get => (bool)GetValue(SpinProperty); set => SetValue(SpinProperty, value); }

        public int SpinDuration { get => (int)GetValue(SpinDurationProperty); set => SetValue(SpinDurationProperty, value); }

        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }

        public string UniqueId { get; }
    }
}