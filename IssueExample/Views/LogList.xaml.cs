#region Header

// ----------------------------------------------------------------------------------------------
// (c) Jonas Ströbele
// 
// IssueExample   LogList.xaml.cs
// creation date: 24.04.2017  20:50
// ----------------------------------------------------------------------------------------------

#endregion

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