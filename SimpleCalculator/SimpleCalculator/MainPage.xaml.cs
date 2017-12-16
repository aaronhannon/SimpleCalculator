using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
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
        int numberCounter = 1;
        string numString;
        Button btn;
        bool started = false;

        public MainPage()
        {
            this.InitializeComponent();

            //ApplicationView.PreferredLaunchViewSize = new Size(720, 1280);
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(720, 1280));
            createButtons();
        }

        private void createButtons()
        {
            StackPanel topStk = new StackPanel();
            topStk.Background = new SolidColorBrush(Colors.DarkSlateGray);
            topStk.SetValue(Grid.RowProperty, 0);
            MainHolder.Children.Add(topStk);

            Grid displayGrid = new Grid();
            displayGrid.Name = "DisplayGrid";
            displayGrid.Height = 300;
            displayGrid.Width = 500;

            for (int k = 0; k < 3; k++)
            {
                displayGrid.RowDefinitions.Add(new RowDefinition());
            }

            topStk.Children.Add(displayGrid);

            Grid btnGrid = new Grid();
            btnGrid.Name = "btnGrid";

            for (int i = 0; i < 4; i++)
            {   
                btnGrid.RowDefinitions.Add(new RowDefinition());
                btnGrid.ColumnDefinitions.Add(new ColumnDefinition());

                for (int j = 0; j < 3; j++)
                {
                    btn = new Button();
                    btn.Height = 150;
                    btn.Width = 150;
                    btn.VerticalAlignment = VerticalAlignment.Center;
                    btn.HorizontalAlignment = HorizontalAlignment.Center;
                    //btn.Background = new SolidColorBrush(Colors.White);
                    btn.SetValue(Grid.RowProperty, i);
                    btn.SetValue(Grid.ColumnProperty, j);

                    string value ="";

                    if (numberCounter < 10)
                    {
                        btn.Content = numberCounter;
                        value += numberCounter;
                        btn.Tag = value;
                    }
                    else
                    {
                        switch (numberCounter)
                        {
                            case 10:
                                btn.Content = ".";
                                value += numberCounter;
                                btn.Tag = value;
                                break;

                            case 11:
                                btn.Content = 0;
                                break;

                            case 12:
                                btn.Content = "=";
                                break;
                        }
                    }

                    btn.FontFamily = new FontFamily("Sans-Serif");
                    btn.FontSize = 40;
                    btn.Foreground = new SolidColorBrush(Colors.White);
                    btn.BorderThickness = new Thickness(0);

                    btn.Tapped += Btn_Tapped;
                    btnGrid.Children.Add(btn);

                    numberCounter++;
                }

            }

            
            System.Diagnostics.Debug.WriteLine("Tapped: ");

            btnGrid.Background = new SolidColorBrush(Color.FromArgb(255, (byte)0, (byte)38, (byte)45));
            btnGrid.SetValue(Grid.RowProperty, 1);
            MainHolder.Children.Add(btnGrid);
            
        }

        TextBlock txBl = new TextBlock();

        private void Btn_Tapped(object sender, TappedRoutedEventArgs e)
        {
     
            string value = " ";    
            Button btn = (Button)sender;
            Grid outputGrid = FindName("DisplayGrid") as Grid;

            if (started)
            {
                outputGrid.Children.Remove(txBl);
            }

            value = (string)btn.Tag;

            numString += value;
            //txBl = null;

            txBl.Text = numString;
            txBl.SetValue(Grid.RowProperty, 1);
            txBl.HorizontalAlignment = HorizontalAlignment.Right;
            txBl.Foreground = new SolidColorBrush(Colors.White);
            txBl.FontSize = 45;

            System.Diagnostics.Debug.WriteLine("Tapped: " + value);
            outputGrid.Children.Add(txBl);

            started = true;

            //txBl.Text = value;
        }

    }
}
