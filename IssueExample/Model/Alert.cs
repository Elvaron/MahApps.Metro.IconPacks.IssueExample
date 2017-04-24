namespace IssueExample
{
    #region using

    using System;
    using System.Windows;

    #endregion

    public class Alert : DependencyObject
    {
        public static readonly DependencyProperty DetailInformationProperty = DependencyProperty.Register(
            "DetailInformation",
            typeof(string),
            typeof(Alert),
            new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty ShortTextProperty =
            DependencyProperty.Register("ShortText", typeof(string), typeof(Alert), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(string), typeof(Alert), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(Alert), new UIPropertyMetadata(string.Empty));

        public static readonly DependencyProperty UniqueIdProperty =
            DependencyProperty.Register("UniqueId", typeof(string), typeof(Alert), new UIPropertyMetadata(string.Empty));

        public Alert(string source, string message, string detailInformation)
        {
            Text = message ?? string.Empty;
            Source = source ?? string.Empty;
            DetailInformation = detailInformation;
            UniqueId = Guid.NewGuid().ToString();
        }

        public string DetailInformation { get => (string)GetValue(DetailInformationProperty); set => SetValue(DetailInformationProperty, value); }

        public string ShortText { get => (string)GetValue(ShortTextProperty); set => SetValue(ShortTextProperty, value); }

        public string Source { get => (string)GetValue(SourceProperty); set => SetValue(SourceProperty, value); }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);
                ShortText = getShortText(value, 60);
            }
        }

        public string UniqueId { get => (string)GetValue(UniqueIdProperty); set => SetValue(UniqueIdProperty, value); }

        private string getShortText(string text, int length)
        {
            return text.Length <= length ? text : $"{text.Substring(0, length)}...";
        }
    }
}