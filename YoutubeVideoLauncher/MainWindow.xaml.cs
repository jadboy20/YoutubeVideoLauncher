using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YoutubeSearch;

namespace YoutubeVideoLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Any variable placed in here can be accessed anywhere on this page:
        string[] VideoArray = new string[10];

        public MainWindow()
        {
            InitializeComponent();

            // For initializing, we put it in here:
            VideoArray[0] = "First Element";
            VideoArray[1] = "Second Element";
            VideoArray[2] = "Third Element";
            VideoArray[3] = "Fourth Element";
            VideoArray[4] = "Fifth Element";
            VideoArray[5] = "Sixth Element";
            VideoArray[6] = "Seventh Element";
            VideoArray[7] = "Eighth Element";
            VideoArray[8] = "Ninth Element";
            VideoArray[9] = "Tenth Element";

            for(int i = 0; i < 10; i++)
            {
                videoList.Items.Add(VideoArray[i]);
            }


        }

        private void videoList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox lb = sender as ListBox;
            MessageBox.Show("You clicked " + lb.SelectedItem);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // First we make the search. 
            var items = new VideoSearch();

            List<VideoInformation> list = items.SearchQuery("Minecraft", 1);

            videoList.Items.Clear();

            foreach(VideoInformation video in list)
            {
                videoList.Items.Add(video.Url);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // When size is changed, change the height of the List
            Window window = sender as Window;
            double heightDiff = e.NewSize.Height - e.PreviousSize.Height;
            
            // videoList.Height += heightDiff;

            windowHeight.Content = "Window Height: " + e.NewSize.Height.ToString();
            listHeight.Content = "List Height: " + videoList.Height.ToString();
        }
    }
}
