namespace IssueExample
{
    #region using

    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;

    #endregion

    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ExampleLogWindow
    {
        private readonly LogWindowModel model;

        public ExampleLogWindow()
        {
            model = new LogWindowModel { ProgressValue = LogWindowModel.MinValue, CloseButtonVisible = Visibility.Visible, OkButtonVisible = Visibility.Visible };

            DataContext = model;

            InitializeComponent();

            var list = new LogList(model);

            TransitioningContent.Content = list;
        }

        public void RaiseError(string source, string message, string detailInformation)
        {
            if (null == model)
            {
                return;
            }

            Action<string, string, string> addMethod = model.RaiseError;
            Application.Current.Dispatcher.BeginInvoke(addMethod, source, message, detailInformation);
        }

        public void RaiseInfo(string message, string uniqueId, InfoState state)
        {
            if (null == model)
            {
                return;
            }

            Action<string, string, InfoState> addMethod = model.RaiseInfo;
            Application.Current.Dispatcher.BeginInvoke(addMethod, message, uniqueId, state);
        }

        public void RaiseWarning(string source, string message, string detailInformation)
        {
            if (null == model)
            {
                return;
            }

            Action<string, string, string> addMethod = model.RaiseWarning;
            Application.Current.Dispatcher.BeginInvoke(addMethod, source, message, detailInformation);
        }

        public void SetProgress(int value, bool aborted)
        {
            model.ProgressValue = aborted ? LogWindowModel.MaxValue : value;
        }

        private void cancelButtonClick(object sender, RoutedEventArgs e)
        {
            model?.CancelButtonAction?.Invoke();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void detailInformationButtonClick(object sender, RoutedEventArgs e)
        {
            if (null == model)
            {
                return;
            }

            // This is sort of hacky, but whatever
            string myValue = ((Button)sender)?.Tag?.ToString();

            string[] segments = myValue?.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (2 != segments?.Length)
            {
                return;
            }

            Alert alert = null;

            if (string.Equals(segments[0], LogWindowModel.WarningSuffix, StringComparison.OrdinalIgnoreCase))
            {
                foreach (Alert warning in model.Warnings)
                {
                    if (!warning.UniqueId.Equals(myValue, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    alert = warning;
                    break;
                }
            }
            else
            {
                foreach (Alert error in model.Errors)
                {
                    if (!error.UniqueId.Equals(myValue, StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    alert = error;
                    break;
                }
            }

            if (null == alert)
            {
                return;
            }

            Clipboard.SetText($"{alert.Source}:{Environment.NewLine}{alert.Text}{Environment.NewLine}{alert.DetailInformation}", TextDataFormat.UnicodeText);
        }

        private void FlyoutButtonClick(object sender, RoutedEventArgs e)
        {
            if (!Notifications.IsOpen)
            {
                Notifications.IsOpen = true;
            }
        }

        private void metroWindowClosing(object sender, CancelEventArgs e)
        {
            if (!model.Complete)
            {
                e.Cancel = true;
            }
        }

        private void okButtonClick(object sender, RoutedEventArgs e)
        {
            RaiseInfo("Test", Guid.NewGuid().ToString(), InfoState.Wait);
        }
    }
}