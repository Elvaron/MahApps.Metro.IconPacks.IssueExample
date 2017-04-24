namespace IssueExample
{
    #region using

    using System;
    using System.Windows;

    #endregion

    public class Program
    {
        public static ExampleLogWindow LogWindow { get; set; }

        [STAThread]
        public static void Main(string[] args)
        {
            var app = new Application();

            LogWindow = new ExampleLogWindow();

            app.Run(LogWindow);
        }
    }
}