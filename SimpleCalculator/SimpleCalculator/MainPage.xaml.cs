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


/* Get Zero working
 * 
*/
namespace SimpleCalculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int numberCounter = 1;
        string numString;
        string numStringArray;
        Button btn;
        bool started = false;
        bool operationBool = false;
        string[] numbers = new string[100];
        string[] operations = new string[100];
        int operationCounter = 0;

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

            //rows
            for (int i = 0; i < 4; i++)
            {   
                btnGrid.RowDefinitions.Add(new RowDefinition());
                btnGrid.ColumnDefinitions.Add(new ColumnDefinition());

            //cols
                for (int j = 0; j < 4; j++)
                {

                    switch (j)
                    {
                        case 3:
                            btn = new Button();
                            btn.Height = 150;
                            btn.Width = 150;
                            btn.VerticalAlignment = VerticalAlignment.Center;
                            btn.HorizontalAlignment = HorizontalAlignment.Center;
                            btn.FontFamily = new FontFamily("Sans-Serif");
                            btn.FontSize = 40;
                            btn.Foreground = new SolidColorBrush(Colors.White);
                            btn.Background = new SolidColorBrush(Color.FromArgb(155, (byte)0, (byte)38, (byte)45));
                            btn.BorderThickness = new Thickness(0);

                            if (i == 0)
                            {
                                btn.Content = "+";
                                btn.Tag = "+";
                            }
                            else if (i == 1)
                            {
                                btn.Content = "-";
                                btn.Tag = "-";
                            }
                            else if (i == 2)
                            {
                                btn.Content = "*";
                                btn.Tag = "*";
                            }
                            else if (i == 3)
                            {
                                btn.Content = "/";
                                btn.Tag = "/";
                            }

                            btn.SetValue(Grid.RowProperty, i);
                            btn.SetValue(Grid.ColumnProperty, j);
                            btn.Tapped += Btn_Tapped;
                            btnGrid.Children.Add(btn);
                            break;

                        default:
                            btn = new Button();
                            btn.Height = 150;
                            btn.Width = 150;
                            btn.VerticalAlignment = VerticalAlignment.Center;
                            btn.HorizontalAlignment = HorizontalAlignment.Center;
                            //btn.Background = new SolidColorBrush(Colors.White);
                            btn.SetValue(Grid.RowProperty, i);
                            btn.SetValue(Grid.ColumnProperty, j);

                            string value = "";

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
                                        value += numberCounter;
                                        btn.Tag = "0";
                                        break;

                                    case 12:
                                        btn.Content = "=";
                                        btn.Tag = "=";
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
                            break;

                    }

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

            if (btn.Tag != "=")
            {

                if (btn.Tag == "+" || btn.Tag == "-" || btn.Tag == "*" || btn.Tag == "/")
                {
                    numbers[operationCounter] = numStringArray;
                    //numStringArray = "";
                    operations[operationCounter] = (string)btn.Tag;
                    System.Diagnostics.Debug.WriteLine(numbers[operationCounter]);
                    System.Diagnostics.Debug.WriteLine(operations[operationCounter]);
                    operationCounter++;

                }

                if (started)
                {
                    outputGrid.Children.Remove(txBl);
                }

                value = (string)btn.Tag;

                if (value == "+" || value == "-" || value == "*" || value == "/")
                {
                    numString += value;
                    value = "";
                    numStringArray = "";
                }
                else
                {
                    numStringArray += value;
                    numString += value;
                }

                txBl.Text = numString;
                txBl.SetValue(Grid.RowProperty, 1);
                txBl.HorizontalAlignment = HorizontalAlignment.Right;
                txBl.Foreground = new SolidColorBrush(Colors.White);
                txBl.FontSize = 45;

                System.Diagnostics.Debug.WriteLine("Tapped: " + value);
                outputGrid.Children.Add(txBl);

                started = true;
                operationBool = false;

                //txBl.Text = value;
            }
            else
            {
                float values = 0;
                int counter = 0;
                float storedValue = 0;

                numbers[operationCounter] = numStringArray;

                //System.Diagnostics.Debug.WriteLine(float.Parse(numbers[0]));
                //System.Diagnostics.Debug.WriteLine(float.Parse(numbers[1]));

                //values = float.Parse(numbers[0]) + float.Parse(numbers[1]);

                //System.Diagnostics.Debug.WriteLine(values);
                //System.Diagnostics.Debug.WriteLine(operationCounter);

                for (int i = 0; i < operationCounter; i++)
                {
                    if(operations[counter] == "+"){
                        if(counter == 0)
                        {
                            values = float.Parse(numbers[counter]) + float.Parse(numbers[counter + 1]);
                            storedValue = values;                    
                        }
                        else
                        {
                            values = storedValue + float.Parse(numbers[counter + 1]);
                            storedValue = values;
                        }

                    }

                    else if (operations[counter] == "-")
                    {
                        if (counter == 0)
                        {
                            values = float.Parse(numbers[counter]) - float.Parse(numbers[counter + 1]);
                            storedValue = values;
                        }
                        else
                        {
                            values = storedValue - float.Parse(numbers[counter + 1]);
                            storedValue = values;
                        }
                    }

                    else if (operations[counter] == "*")
                    {
                        if (counter == 0)
                        {
                            values = float.Parse(numbers[counter]) * float.Parse(numbers[counter + 1]);
                            storedValue = values;
                        }
                        else
                        {
                            values = storedValue * float.Parse(numbers[counter + 1]);
                            storedValue = values;
                        }
                    }

                    else if (operations[counter] == "/")
                    {
                        if (counter == 0)
                        {
                            values = float.Parse(numbers[counter]) / float.Parse(numbers[counter + 1]);
                            storedValue = values;
                        }
                        else
                        {
                            values = storedValue / float.Parse(numbers[counter + 1]);
                            storedValue = values;
                        }
                    }

                    counter++;
                }

                outputGrid.Children.Remove(txBl);
                txBl.Text = values.ToString();
                outputGrid.Children.Add(txBl);

            }

        }

    }
}

//private void Btn_TappedEquals(object sender, TappedRoutedEventArgs e)
//{
//    float values;
//    Button btn = (Button)sender;
//    //System.Diagnostics.Debug.WriteLine(btn.Tag);
//    Grid outputGrid = FindName("DisplayGrid") as Grid;
//    outputGrid.Children.Remove(txBl);

//    //System.Diagnostics.Debug.WriteLine(numbers[0]);
//    System.Diagnostics.Debug.WriteLine(numbers[1]);

//    //values = float.Parse(numbers[0]) + float.Parse(numbers[1]);

//    //txBl.Text = values.ToString();

//    outputGrid.Children.Add(txBl);
//}


//for (int i = 0; i < operations.Length; i++)
//{
//    System.Diagnostics.Debug.WriteLine(operations[i]);
//    System.Diagnostics.Debug.WriteLine(numbers[i]);
//}

//System.Diagnostics.Debug.WriteLine(operationCounter);