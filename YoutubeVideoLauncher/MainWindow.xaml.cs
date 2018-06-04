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
        private List<Classes.Video> vidList;
        bool start = false;
        string placeHolder = "Please enter search...";

        public MainWindow()
        {
            InitializeComponent();
            /*
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
            */
            searchTextBox.Text = placeHolder;
        }

        private void videoList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBox lb = sender as ListBox;
            Classes.Video vid = lb.SelectedItem as Classes.Video;
            System.Diagnostics.Process.Start(vid.Url);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchFunction();
        }

        private void SearchFunction()
        {
            // First we make the search. 
            var items = new VideoSearch();

            // Get the search item.
            if (searchTextBox.Text.Trim().Equals("") || searchTextBox.Text.Equals(placeHolder))
            {
                MessageBox.Show("Make sure you have entered something in the Search Field");
            }
            else
            {
                string searchQuery = searchTextBox.Text;
                List<VideoInformation> list = items.SearchQuery(searchQuery, 1);

                vidList = new List<Classes.Video>();

                VideoInformation video = new VideoInformation();

             
                foreach (VideoInformation vi in list)
                {
                    Classes.Video vid = new Classes.Video()
                    {
                        Description = vi.Description,
                        Title = vi.Title,
                        Url = vi.Url,
                        Duration = vi.Duration,
                        Author = vi.Author,
                        Thumbnail = vi.Thumbnail

                    };
                    vidList.Add(vid);
                }
                videoList.ItemsSource = vidList;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // When size is changed, change the height of the List
            Window window = sender as Window;
            double heightDiff = e.NewSize.Height - e.PreviousSize.Height;

            if (start == true)
            {
               videoList.Height += heightDiff;
            }
            start = true;

            windowHeight.Content = "Window Height: " + e.NewSize.Height.ToString();
            listHeight.Content = "List Height: " + videoList.Height.ToString();
        }

        private void searchTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // When got focus check if it equals placeholder. If so, then remove it.
            TextBox tb = sender as TextBox;

            if(tb.Text.Equals(placeHolder))
            {
                // We remove the text.
                tb.Text = "";
            }

        }

        private void searchTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if(tb.Text.Trim().Equals(""))
            {
                tb.Text = placeHolder;
            }
        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                SearchFunction();
            }
        }
    }
}
