using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace Wpf___Start_simulator

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer _lightTimer = new();
        private readonly DispatcherTimer _goTimer = new();
        private int _currentLight = 0;
        private readonly Stopwatch _stopwatch = new();
        private readonly SolidColorBrush _redOn = new(Color.FromRgb(220, 0, 0));
        private readonly SolidColorBrush _redOff = new(Color.FromRgb(80, 0, 0));

        public MainWindow()
        {
            InitializeComponent();
            _lightTimer.Interval = TimeSpan.FromSeconds(1);
            _lightTimer.Tick += _lightTimer_Tick;
        }

        private void _lightTimer_Tick(object? sender, EventArgs e)
        {
            _currentLight++;

            var lights = new[] { Light1, Light2, Light3, Light4, Light5 };

            if (_currentLight <= 5)
            {
                lights[_currentLight - 1].Fill = _redOn;

            }

            if (_currentLight == 5)
            {
                var random = new Random();
                int delay = random.Next(1000, 3001);

                _goTimer.Interval = TimeSpan.FromMilliseconds(delay);
                _goTimer.Tick += GoTimer_Tick;
                _goTimer.Start();

            }
        }

        private void GoTimer_Tick(object? sender, EventArgs e)
        {
            _stopwatch.Start();
            Reset();
            _goTimer.Stop();

        }

        private void Reset()
        {
            _currentLight = 0;
            _lightTimer.Stop();

            foreach (var light in new[] { Light1, Light2, Light3, Light4, Light5 })
            {
                light.Fill = _redOff;

            }
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Reset();
            _stopwatch.Reset();
            _lightTimer.Start();

            _currentLight = 0;
            TimerLabel.Text = "--- . --- s";
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            _stopwatch.Stop();
            TimerLabel.Text = $"{_stopwatch.Elapsed.TotalSeconds:F3} seconds";
            Reset();

            if (_stopwatch.Elapsed.TotalSeconds < 0.2)
            {
                MessageBox.Show("Uliaty štart!");

            }

            if (_stopwatch.Elapsed.TotalSeconds >= 0.2 && _stopwatch.Elapsed.TotalSeconds < 0.5)
            {
                MessageBox.Show("Dobry start!");

            }
        }
    }
}