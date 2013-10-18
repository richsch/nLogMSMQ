using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace NR.QueueWatcher.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<LogEntry> LogEntries;
 
        public MainWindow()
        {
            InitializeComponent();

            LogEntries = new ObservableCollection<LogEntry>();
            FetchMessages();
            MessageGrid.ItemsSource = LogEntries;
        }

        private void FetchMessages_OnClick(object sender, RoutedEventArgs e)
        {
            FetchMessages();
        }

        private void FetchMessages()
        {
            var watcher = new QueueWatcher();
            var entries = watcher.GetLogEntries();
            LogEntries.Clear();

            foreach (var entry in entries)
            {
                LogEntries.Add(entry);
            }
        }
    }
}
