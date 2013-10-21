using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NR.QueueWatcher.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<LogEntry> FilteredEntries; 
        public ObservableCollection<LogEntry> LogEntries;

        public ObservableCollection<string> FilterApplication; 
        public ObservableCollection<string> FilterLevels; 
    
        public MainWindow()
        {
            InitializeComponent();
            FilteredEntries = new ObservableCollection<LogEntry>();
            LogEntries = new ObservableCollection<LogEntry>();
            FilterApplication = new ObservableCollection<string>();
            FilterLevels = new ObservableCollection<string>();
            FetchMessages();
            MessageGrid.ItemsSource = FilteredEntries;
            cmbAppFilter.ItemsSource = FilterApplication;
            cmbLevelFilter.ItemsSource = FilterLevels;

            cmbAppFilter.SelectionChanged += UpdateFilter;
            cmbLevelFilter.SelectionChanged += UpdateFilter;
        }

        private void UpdateFilter(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            FilteredEntries = applyFilter(LogEntries);
            MessageGrid.ItemsSource = FilteredEntries;
        }

        private void FetchMessages_OnClick(object sender, RoutedEventArgs e)
        {
            FetchMessages();
        }
       
        private ObservableCollection<LogEntry> applyFilter(IEnumerable<LogEntry> items)
        {
            var result = items.AsQueryable();
            if (cmbAppFilter.SelectedValue != null && !string.IsNullOrEmpty(cmbAppFilter.SelectedValue.ToString()))
            {
                result = result.Where(r => r.Application == cmbAppFilter.SelectedValue.ToString());
            }
            if (cmbLevelFilter.SelectedValue != null && !string.IsNullOrEmpty(cmbLevelFilter.SelectedValue.ToString()))
            {
                result = result.Where(r => r.Level == cmbLevelFilter.SelectedValue.ToString());
            }
            return new ObservableCollection<LogEntry>(result.ToList());
        } 

        private void FetchMessages()
        {
            var watcher = new QueueWatcher();
            var entries = watcher.GetLogEntries().ToList();
            LogEntries.Clear();

            foreach (var entry in entries)
            {
                LogEntries.Add(entry);
            }

            var applications = new []{String.Empty}.Union(entries.GroupBy(f => f.Application).Select(d => d.Key));
            FilterApplication = new ObservableCollection<string>(applications);
            var levels = new []{String.Empty}.Union(entries.GroupBy(f => f.Level).Select(d => d.Key));
            FilterLevels = new ObservableCollection<string>(levels);

            FilteredEntries = applyFilter(LogEntries);
        }
    }
}
