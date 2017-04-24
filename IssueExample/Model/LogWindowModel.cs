namespace IssueExample
{
    #region using

    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    using IssueExample.Annotations;

    using MahApps.Metro.IconPacks;

    #endregion

    public class LogWindowModel : INotifyPropertyChanged
    {
        public const string ErrorSuffix = "Error";

        public const int MaxValue = 100;

        public const int MinValue = 0;

        public const string WarningSuffix = "Warning";

        public Action CancelButtonAction;

        public Action OkButtonAction;

        private int alertCount;

        private Visibility alertVisible = Visibility.Hidden;

        private Visibility cancelButtonVisible = Visibility.Hidden;

        private Visibility closeButtonVisible = Visibility.Hidden;

        private bool complete;

        private bool isIndeterminate;

        private Visibility okButtonVisible = Visibility.Hidden;

        private string progressBarColor = "#0097fb";

        private int progressValue;

        private string statusMessage = string.Empty;

        private int warningCount;

        private int warningIconPosition = 2;

        private Visibility warningVisible = Visibility.Hidden;

        public event PropertyChangedEventHandler PropertyChanged;

        public int AlertCount
        {
            get => alertCount;
            set
            {
                alertCount = value;
                OnPropertyChanged("AlertCount");
            }
        }

        public Visibility AlertVisible
        {
            get => alertVisible;
            set
            {
                alertVisible = value;
                OnPropertyChanged("AlertVisible");
            }
        }

        public Visibility CancelButtonVisible
        {
            get => cancelButtonVisible;
            set
            {
                cancelButtonVisible = value;
                OnPropertyChanged("CancelButtonVisible");
            }
        }

        public Visibility CloseButtonVisible
        {
            get => closeButtonVisible;
            set
            {
                closeButtonVisible = value;
                OnPropertyChanged("CloseButtonVisible");
            }
        }

        public bool Complete
        {
            get => complete;
            set
            {
                complete = value;
                OnPropertyChanged("Complete");
            }
        }

        public ObservableCollection<Alert> Errors { get; } = new ObservableCollection<Alert>();

        public bool IsIndeterminate
        {
            get => isIndeterminate;
            set
            {
                isIndeterminate = value;
                OnPropertyChanged("IsIndeterminate");
            }
        }

        public ObservableCollection<LogEntry> LogEntries { get; } = new ObservableCollection<LogEntry>();

        public Visibility OkButtonVisible
        {
            get => okButtonVisible;
            set
            {
                okButtonVisible = value;
                OnPropertyChanged("OkButtonVisible");
            }
        }

        public string ProgressBarColor
        {
            get => progressBarColor;
            set
            {
                progressBarColor = value;
                OnPropertyChanged("ProgressBarColor");
            }
        }

        // private string runText;
        public int ProgressValue
        {
            get => progressValue;
            set
            {
                if (MinValue > value || value > MaxValue)
                {
                    return;
                }

                progressValue = value;

                OnPropertyChanged("ProgressValue");

                Complete = progressValue >= MaxValue;
                IsIndeterminate = progressValue < MaxValue;
            }
        }

        public string StatusMessage
        {
            get => statusMessage;
            set
            {
                statusMessage = value;
                OnPropertyChanged("StatusMessage");
            }
        }

        public int WarningCount
        {
            get => warningCount;
            set
            {
                warningCount = value;
                OnPropertyChanged("WarningCount");
            }
        }

        public int WarningIconPosition
        {
            get => warningIconPosition;
            set
            {
                warningIconPosition = value;
                OnPropertyChanged("WarningIconPosition");
            }
        }

        public ObservableCollection<Alert> Warnings { get; } = new ObservableCollection<Alert>();

        public Visibility WarningVisible
        {
            get => warningVisible;
            set
            {
                warningVisible = value;
                OnPropertyChanged("WarningVisible");
            }
        }

        public void RaiseError(string source, string message, string detailInformation)
        {
            if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(source))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(detailInformation))
            {
                detailInformation = "No details available.";
            }

            if (WarningIconPosition == 2)
            {
                WarningIconPosition = 1;
            }

            var error = new Alert(source, message, detailInformation);
            error.UniqueId = $"{ErrorSuffix}.{error.UniqueId}";
            Errors.Add(error);
            AlertVisible = Visibility.Visible;
            AlertCount = Errors.Count;
        }

        public void RaiseInfo(string message, string uniqueId, InfoState state)
        {
            LogEntry logEntry = LogEntries.FirstOrDefault(l => l.UniqueId.Equals(uniqueId, StringComparison.OrdinalIgnoreCase));

            PackIconModernKind icon = getIconForState(state);
            string color = getColorForState(state);
            bool spin = isSpin(state);

            if (null != logEntry)
            {
                if (!string.IsNullOrWhiteSpace(message))
                {
                    logEntry.Text = message;
                }

                logEntry.Icon = icon;
                logEntry.IconColor = color;
                logEntry.Spin = spin;
                logEntry.SpinDuration = spin ? 2 : 1;
            }
            else if (!string.IsNullOrWhiteSpace(message))
            {
                LogEntries.Add(new LogEntry(message, icon, color, /*spin*/true, uniqueId, spin ? 2 : 1));
            }
        }

        public void RaiseWarning(string source, string message, string detailInformation)
        {
            if (string.IsNullOrWhiteSpace(message) || string.IsNullOrWhiteSpace(source))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(detailInformation))
            {
                detailInformation = "No details available.";
            }

            var warning = new Alert(source, message, detailInformation);
            warning.UniqueId = $"{WarningSuffix}.{warning.UniqueId}";
            Warnings.Add(warning);
            WarningVisible = Visibility.Visible;
            WarningCount = Warnings.Count;
        }

        public void SetLastState(InfoState state)
        {
            LogEntry lastLogEntry = LogEntries.LastOrDefault();

            if (null == lastLogEntry)
            {
                return;
            }

            lastLogEntry.Icon = getIconForState(state);
            lastLogEntry.IconColor = getColorForState(state);
            lastLogEntry.Spin = isSpin(state);
            lastLogEntry.SpinDuration = isSpin(state) ? 2 : 1;
        }

        internal string getColorForState(InfoState state)
        {
            switch (state)
            {
                case InfoState.Positive: return "#006442";
                case InfoState.Negative: return "#ff3600";
                case InfoState.Wait: return "#19b5fe";
                default: return "#FFFFFF";
            }
        }

        internal PackIconModernKind getIconForState(InfoState state)
        {
            switch (state)
            {
                case InfoState.Positive: return PackIconModernKind.Check;
                case InfoState.Negative: return PackIconModernKind.Close;
                case InfoState.Wait: return PackIconModernKind.OsChromium;
                default: return PackIconModernKind.Acorn;
            }
        }

        internal bool isSpin(InfoState state)
        {
            return state == InfoState.Wait;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}