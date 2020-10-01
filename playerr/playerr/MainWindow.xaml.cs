using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.WindowsAPICodePack.Dialogs;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace playerr
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer2 = new DispatcherTimer();
        Timer ttimer = new Timer();
        


        public void fillthelist()
        {
            DirectoryInfo Dİ = new DirectoryInfo(System.IO.Path.GetFullPath(@"..\..\songs"));
            FileInfo[] rgfiles = Dİ.GetFiles();
            foreach (FileInfo fi in rgfiles)
            {
                listwiew.Items.Add(fi.Name);
            }
            
        }
        public MainWindow()
        {
            InitializeComponent();
            
            //DispatcherTimer timer = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(1);
            timer2.Tick += timer2_Tick;
           // timer2.Start();

        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            TimeSpan ts1 = TimeSpan.FromMilliseconds(TimerSlider.Value);
            ts1 = mediaElement1.Position;
            TimerSlider.Minimum = 0;
            if (mediaElement1.NaturalDuration.HasTimeSpan == true)
            {
                TimerSlider.Maximum = (int)mediaElement1.NaturalDuration.TimeSpan.TotalSeconds;
            }
                TimerSlider.Value = mediaElement1.Position.TotalSeconds;
            Currenttime.Text =ts1.ToString(@"hh\:mm\:ss");
            if (mediaElement1.NaturalDuration.HasTimeSpan == true)
            {
                musiclength.Text = mediaElement1.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");
            }
        }
        

        void ttimer_Tick(object sender, EventArgs e)
        {
           // TimerSlider.Value = (double)ttimer;

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void btnfile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            string workingfiledirectory = Environment.CurrentDirectory;
            //MessageBox.Show(System.IO.Path.GetFullPath(@"..\..\songs"));
            string currentfile = System.IO.Path.GetFullPath(@"..\..\songs");

            System.Diagnostics.Process.Start(currentfile);


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            fillthelist();
            
        }

        private void listwiew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MediaPlayer player = new MediaPlayer();
            string songname = listwiew.SelectedItem.ToString();
            string songdirectory = System.IO.Path.GetFullPath(@"..\..\songs\") + songname;
            mediaElement1.Source = new Uri(songdirectory);
            mediaElement1.Play();
            InitializePropertyValues();
            sarkıadı.Text = listwiew.SelectedItem.ToString();
            int ms = (int)TimerSlider.Maximum;
            TimeSpan ts = TimeSpan.FromMilliseconds(ms);
            ttimer.Enabled = false;
            ttimer.Interval = ms;
            musiclength.Text = ts.ToString(@"hh\:mm\:ss");
            int SliderValue = (int)TimerSlider.Value;
            TimeSpan tss = new TimeSpan(0, 0, 0, 0, SliderValue);
            mediaElement1.Position = tss;
            ttimer.Enabled = true;
            timer2.Start();
        }
            
        private void btnplay_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Play();
            
        }
        
        private void TimerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
            
        {
            TimeSpan ts1 = TimeSpan.FromMilliseconds(TimerSlider.Value);
            
            mediaElement1.Position = TimeSpan.FromSeconds(TimerSlider.Value);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement1.Volume = (double)soundchanger.Value;
        }

        private void btnpause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement1.Pause();
          
        }

        private void mediaElement1_Initialized(object sender, EventArgs e)
        {


        }
        void InitializePropertyValues()
        {
            // Set the media's starting Volume and SpeedRatio to the current value of the
            // their respective slider controls.
            mediaElement1.Volume = (double)soundchanger.Value;
            //mediaElement1.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void mediaElement1_MediaOpened(object sender, RoutedEventArgs e)
        {
           //TimerSlider.Maximum = mediaElement1.NaturalDuration.TimeSpan.TotalMilliseconds;
            //musiclength.Text = mediaElement1.NaturalDuration;
           // int ms = (int)TimerSlider.Maximum;
            //TimeSpan ts = TimeSpan.FromMilliseconds(ms);
            //musiclength.Text = ts.ToString(@"hh\:mm\:ss");

            //ttimer.Tick += new EventHandler(ttimer_Tick);

            //ttimer.Interval = (int)mediaElement1.NaturalDuration.TimeSpan.Milliseconds;

            

            //ttimer.Enabled = true;
            //ttimer.Start();
            //timer2.Start();

            //TimerSlider.Value =ttimer

        }


        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            
        }

        private void musiclength_DragLeave(object sender, System.Windows.DragEventArgs e)
        {
            
        }

        private void musiclength_Drop(object sender, System.Windows.DragEventArgs e)
        {
            
        }

        private void mediaElement1_MediaEnded(object sender, RoutedEventArgs e)
        {
            
            int nextIndex = 0;
            if ((listwiew.SelectedIndex >= 0) && (listwiew.SelectedIndex < (listwiew.Items.Count - 1)))
                nextIndex = listwiew.SelectedIndex + 1;
            listwiew.SelectedIndex = nextIndex;
            string songname = listwiew.SelectedItem.ToString();
            string songdirectory = System.IO.Path.GetFullPath(@"..\..\songs\") + songname;
            mediaElement1.Source = new Uri(songdirectory);
            //mediaElement1.Play();

        }

        private void btnplayprevius_Click(object sender, RoutedEventArgs e)
        {
            int nextIndex = 0;
            if ((listwiew.SelectedIndex >= 0) && (listwiew.SelectedIndex < (listwiew.Items.Count - 1)))
                nextIndex = listwiew.SelectedIndex + 1;
            listwiew.SelectedIndex = nextIndex;
        }

        private void mediaElement1_PreviewQueryContinueDrag(object sender, System.Windows.QueryContinueDragEventArgs e)
        {
            //TimerSlider.Value = mediaElement1.Position.Milliseconds;
        }

        private void mediaElement1_QueryContinueDrag(object sender, System.Windows.QueryContinueDragEventArgs e)
        {
           // TimerSlider.Value = mediaElement1.Position.Milliseconds;
        }

        private void TimerSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {

        }

        private void btnplaynext_Click(object sender, RoutedEventArgs e)
        {
            //int a = listwiew.Items.Count;
            if (listwiew.SelectedIndex > 0)
            {
                int nextindex = listwiew.SelectedIndex - 1;
                listwiew.SelectedIndex = nextindex;
            }
            else listwiew.SelectedIndex = Convert.ToInt32( listwiew.Items.Count.ToString())-1;

        }

        private void TimerSlider_GotMouseCapture(object sender, System.Windows.Input.MouseEventArgs e)
        {
           // mediaElement1.Position = TimeSpan.FromMilliseconds( TimerSlider.Value);

        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            fillthelist();
        }
    }
}
