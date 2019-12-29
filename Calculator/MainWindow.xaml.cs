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

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string opLeft = "";//левый операнд в выражении
        string operation = "";//операция
        string opRight = "";//правый операнд в выражении

        bool check_CalculatedopLeft = false;

        private void Button_Event(object sender, RoutedEventArgs e)
        {
            // Получание текста кнопки
            string clickedButton_text = (string)((Button)e.OriginalSource).Content;

            int num;
            // Преобразование в число
            bool result = int.TryParse(clickedButton_text, out num);

            // Если нажато число
            if (result)
            {
                // Если операция не задана
                if (Empty_Operator(operation))
                {
                    // Добавляем к левому операнду
                    if (check_CalculatedopLeft)
                    {
                        opLeft = "";
                        textBlock.Text = "";
                        check_CalculatedopLeft = false;

                        opLeft += clickedButton_text;
                        textBlock.Text += clickedButton_text;
                    }
                    else
                    {
                        opLeft += clickedButton_text;
                        textBlock.Text += clickedButton_text;
                    }
                }
                else
                {
                    if (Empty_Operator(opRight))
                    {
                        textBlock.Text = "";
                    }

                    // Иначе к правому операнду
                    opRight += clickedButton_text;
                    textBlock.Text += clickedButton_text;
                }
            }
            // При нажатии на операцию
            else
            {
                if (Empty_Operator(opLeft))
                {
                    opLeft = "0";
                }

                Run_Operation(clickedButton_text);
                check_CalculatedopLeft = true;
            }
        }

        // Проверка на пустоту
        private bool Empty_Operator(string emptyOperator)
        {
            if (emptyOperator == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Run_Operation(string transferredOperation)
        {

            if (transferredOperation == "C")
            {
                opLeft = "";
                opRight = "";
                operation = "";
                textBlock.Text = "";
            }
            else if (transferredOperation == "=")
            {
                if (!Empty_Operator(opRight))
                {
                    processing(operation);
                    textBlock.Text = "";
                    textBlock.Text += opLeft;
                    operation = "";
                }
            }
            else
            {
                if (!Empty_Operator(opRight))
                {
                    processing(operation);

                    textBlock.Text = "";
                    textBlock.Text += opLeft;
                }

                operation = transferredOperation;
            }
        }

        private void processing(string transferredOperation)
        {
            int num1 = int.Parse(opLeft);
            int num2 = int.Parse(opRight);
            string oldopLeft = opLeft;
            switch (transferredOperation)
            {
                case "+":
                    opLeft = (num1 + num2).ToString();
                    Protocolling(oldopLeft);
                    break;
                case "-":
                    opLeft = (num1 - num2).ToString();
                    Protocolling(oldopLeft);
                    break;
                case "*":
                    opLeft = (num1 * num2).ToString();
                    Protocolling(oldopLeft);
                    break;
                case "/":
                    // Обработка ошибки деления числа на 0
                    if (num2 == 0)
                    {
                        protocolBlock.Text += oldopLeft + " " + operation + " " + opRight + " = Ошибка!\n";
                        protocolBlock.Text += "Нельзя делить на 0!\n";
                        opLeft = "";
                        opRight = "";
                        operation = "";
                    }
                    else
                    {
                        opLeft = (num1 / num2).ToString();
                        Protocolling(oldopLeft);
                    }
                    break;
            }

        }

        // Протоколирование
        private void Protocolling(string num1)
        {
            StringBuilder s = new StringBuilder();
            s.Append(num1);
            s.Append(operation);
            s.Append(opRight.ToString());
            s.Append("=");
            s.Append(opLeft.ToString());

            s.AppendLine();

            protocolBlock.Text += s;

            opRight = "";
            operation = "";

        }
    }
}
