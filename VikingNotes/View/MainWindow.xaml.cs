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
using ViewModels;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_takeQuiz_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new TakeQuizViewModel();
        }

        private void btn_makeQuiz_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new MakeQuizViewModel();
        }

        private void btn_Statistics_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new StatisticsViewModel();
        }
    }
}
