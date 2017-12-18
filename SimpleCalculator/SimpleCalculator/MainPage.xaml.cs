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



namespace SimpleCalculator
{
    /// <summary>
    /// A Simple Calculator that does the necessary simple calculations quickly
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //Variables
        int numberCounter = 1;
        string numString;
        string numStringArray;
        Button btn;
        bool started = false;
        string[] numbers = new string[100];
        string[] operations = new string[100];
        int operationCounter = 0;
        int inputCounter = 0;
        bool equalsPressed = false;
        bool invalidFirstInput = false;
        bool contin = false;
        string savedNum;

        public MainPage()
        {
            this.InitializeComponent();

            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(720, 1280));
            createButtons();
        }

        //Board & Button creator
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

            //Creating Rows
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
                        //Setting Operation Buttons
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

                            //Default button is a number
                        default:
                            btn = new Button();
                            btn.Height = 150;
                            btn.Width = 150;
                            btn.VerticalAlignment = VerticalAlignment.Center;
                            btn.HorizontalAlignment = HorizontalAlignment.Center;
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
                                //Buttons for . 0 and =
                                switch (numberCounter)
                                {
                                    case 10:
                                        btn.Content = ".";
                                        value += ".";
                                        btn.Tag = ".";
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

                            //tapped event
                            btn.Tapped += Btn_Tapped;
                            btnGrid.Children.Add(btn);

                            numberCounter++;
                            break;
                    }

                }

            }

            btnGrid.Background = new SolidColorBrush(Color.FromArgb(255, (byte)0, (byte)38, (byte)45));
            btnGrid.SetValue(Grid.RowProperty, 1);
            MainHolder.Children.Add(btnGrid);
            
        }

        TextBlock txBl = new TextBlock();

        //Tapped event
        private void Btn_Tapped(object sender, TappedRoutedEventArgs e)
        {

            string value = " ";
            Button btn = (Button)sender;
            Grid outputGrid = FindName("DisplayGrid") as Grid;

            //Resets arrays when = is tapped
            if (equalsPressed == true && (btn.Tag == "*" || btn.Tag == "/" || btn.Tag == "+" || btn.Tag == "-"))
            {
                numbers = new string[100];
                operations = new string[100];
                value = "";
                numStringArray = "";
                numString = "";
                operationCounter = 0;
                inputCounter = 0;
                contin = true;
            }
            else if(equalsPressed == true)
            {
                numbers = new string[100];
                operations = new string[100];
                value = "";
                numStringArray = "";
                numString = "";
                operationCounter = 0;
                inputCounter = 0;
                contin = false;
            }

            //if first input is an operation throw error
            if (inputCounter == 0 && (btn.Tag == "*" || btn.Tag == "/" || btn.Tag == "+" || btn.Tag == "-") && contin == false)
            {
                txBl.Text = "Invalid First Input!";
                txBl.SetValue(Grid.RowProperty, 1);
                txBl.HorizontalAlignment = HorizontalAlignment.Right;
                txBl.Foreground = new SolidColorBrush(Colors.White);
                txBl.FontSize = 25;
                outputGrid.Children.Remove(txBl);  
                outputGrid.Children.Add(txBl);
                invalidFirstInput = true;
                inputCounter = 0;
            }
            else { 
                //If tag is not = then add button or operation to their arrays
                if (btn.Tag != "=")
                {
                    //resetting display in case of first input error
                    if (invalidFirstInput == true)
                    {
                        outputGrid.Children.Remove(txBl);
                        invalidFirstInput = false;
                        operationCounter = 0;
                    }

                    //If its an operation add to the array
                    if ((btn.Tag == "+" || btn.Tag == "-" || btn.Tag == "*" || btn.Tag == "/") && contin == false)
                    {
                        numbers[operationCounter] = numStringArray;
                        operations[operationCounter] = (string)btn.Tag;
                        operationCounter++;

                    }
                    else if ((btn.Tag == "+" || btn.Tag == "-" || btn.Tag == "*" || btn.Tag == "/") && contin == true)
                    {
                        numbers[operationCounter] = savedNum;
                        operations[operationCounter] = (string)btn.Tag;
                        operationCounter++;
                    }

                    //resetting display on next input after equals once calculation is complete
                    if (started || equalsPressed == true)
                    {
                        outputGrid.Children.Remove(txBl);
                        equalsPressed = false;
                    }


                    value = (string)btn.Tag;

                    //Adds to Display but not to the array
                    if (value == "+" || value == "-" || value == "*" || value == "/")
                    {
                        numString += value;
                        value = "";
                        numStringArray = "";
                    }
                    //Adds to the array var and display
                    else
                    {
                        numStringArray += value;
                        numString += value;
                    }
                    if (contin)
                    {
                        txBl.Text = savedNum + "" + numString;
                    }else
                    {
                        txBl.Text = numString;
                    }
                    
                    txBl.SetValue(Grid.RowProperty, 1);
                    txBl.HorizontalAlignment = HorizontalAlignment.Right;
                    txBl.Foreground = new SolidColorBrush(Colors.White);
                    txBl.FontSize = 45;

                    outputGrid.Children.Add(txBl);

                    started = true;
                    inputCounter++;
                }
                //else is equals ==> do calculation
                else
                {
                    float values = 0;
                    int counter = 0;
                    float storedValue = 0;

                    //adds last number in the string to the array
                    numbers[operationCounter] = numStringArray;

                    //Runs through the arrays
                    for (int i = 0; i < operationCounter; i++)
                    {   
                        //If operation == + then add first and second item in the array
                        if (operations[counter] == "+")
                        {
                            //if its the first calculation add first and second then stored value = value
                            if (counter == 0)
                            {
                                values = float.Parse(numbers[counter]) + float.Parse(numbers[counter + 1]);
                                storedValue = values;
                            }
                            //else  if not first calculation add stored value with the third/fourth number....etc
                            else
                            {
                                values = storedValue + float.Parse(numbers[counter + 1]);
                                storedValue = values;
                            }

                        }

                        //Same as Plus operator but -
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

                        //Same as Plus operator but *
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

                        //Same as Plus operator but /
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
                    equalsPressed = true;

                    savedNum = values.ToString();
                }
            }

        }

    }
}
