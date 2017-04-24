namespace IssueExample
{
    /// <summary>
    /// Interaction logic for LogList.xaml
    /// </summary>
    public partial class LogList
    {
        public LogList(LogWindowModel model)
        {
            Model = model;
            DataContext = Model;
            InitializeComponent();
        }

        private LogWindowModel Model { get; }
    }
}