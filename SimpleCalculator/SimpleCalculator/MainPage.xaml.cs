using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SimpleCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            createButtons();
        }

        private void createButtons()
        {
            StackPanel topStk = new StackPanel();
            topStk.Background = new SolidColorBrush(Colors.SlateGray);
            topStk.SetValue(Grid.RowProperty, 0);
            MainHolder.Children.Add(topStk);

            Grid btnGrid = new Grid();
            btnGrid.Name = "btnGrid";

            Button btn;

            for (int i = 0; i < 3; i++)
            {   
                btnGrid.RowDefinitions.Add(new RowDefinition());
                btnGrid.ColumnDefinitions.Add(new ColumnDefinition());

                for (int j = 0; j < 3; j++)
                {
                    btn = new Button();
                    btn.Height = 25;
                    btn.Width = 25;
                    btn.VerticalAlignment = VerticalAlignment.Center;
                    btn.HorizontalAlignment = HorizontalAlignment.Center;
                    btn.Background = new SolidColorBrush(Colors.White);
                    btn.SetValue(Grid.RowProperty, i);
                    btn.SetValue(Grid.ColumnProperty, j);

                    btnGrid.Children.Add(btn);
                }


            }

            btnGrid.Background = new SolidColorBrush(Colors.Black);
            btnGrid.SetValue(Grid.RowProperty, 1);
            MainHolder.Children.Add(btnGrid); 
        }
    }
}
