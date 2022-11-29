using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isEqualsPressed;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText("1");
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText("2");
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText("3");
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText("4");
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText("5");
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText("6");
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText(button7.Content.ToString());
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText("8");
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText("9");
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText("0");
        }

        private void ButtonComma_Click(object sender, RoutedEventArgs e)
        {
            if (!operatorsPriority.ContainsKey(enterFormula.Text[enterFormula.Text.Length - 1])
                && enterFormula.Text[enterFormula.Text.Length - 1] != ','
                || enterFormula.Text[enterFormula.Text.Length - 2] != ',')
            {
                enterFormula.AppendText(",");
            }
        }

        private void ButtonDivision_Click(object sender, RoutedEventArgs e)
        {
            if (!operatorsPriority.ContainsKey(enterFormula.Text[enterFormula.Text.Length - 1]) && enterFormula.Text[enterFormula.Text.Length - 1] != ',')
            {
                enterFormula.AppendText("/");
            }
        }

        private void ButtonMultiplication_Click(object sender, RoutedEventArgs e)
        {
            if (!operatorsPriority.ContainsKey(enterFormula.Text[enterFormula.Text.Length - 1]) && enterFormula.Text[enterFormula.Text.Length - 1] != ',')
            {
                enterFormula.AppendText("*");
            }
        }

        private void ButtonSubtracting_Click(object sender, RoutedEventArgs e)
        {
            if (!operatorsPriority.ContainsKey(enterFormula.Text[enterFormula.Text.Length - 1]) && enterFormula.Text[enterFormula.Text.Length - 1] != ',')
            {
                enterFormula.AppendText("-");
            }
        }

        private void ButtonAddition_Click(object sender, RoutedEventArgs e)
        {
            if (!operatorsPriority.ContainsKey(enterFormula.Text[enterFormula.Text.Length - 1]) && enterFormula.Text[enterFormula.Text.Length - 1] != ',')
            {
                enterFormula.AppendText("+");
            }
        }

        private void ButtonLeftBracket_Click(object sender, RoutedEventArgs e)
        {
            if (isEqualsPressed || enterFormula.Text == "0")
            {
                enterFormula.Clear();
                isEqualsPressed = false;
            }

            enterFormula.AppendText("(");
        }

        private void ButtonRightBracket_Click(object sender, RoutedEventArgs e)
        {
            if (enterFormula.Text.Contains("("))
            {
                enterFormula.AppendText(")");
            }
        }

        private void ButtonEquals_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder postfix = DijkstrasAlgorithm(enterFormula.Text);

            StringBuilder result = Calculating(postfix);

            enterFormula.Clear();

            if (result.ToString() == "∞")
            {
                enterFormula.AppendText("Деление на ноль невозможно");
            }
            else
            {
                enterFormula.AppendText(result.ToString());
            }


            isEqualsPressed = true;
        }

        /// <summary>
        /// Calculating postfix string
        /// </summary>
        /// <param name="postfix"></param>
        /// <returns></returns>
        private StringBuilder Calculating(StringBuilder postfix)
        {
            StringBuilder output = new StringBuilder();
            Stack<double> stack = new Stack<double>();

            for (int i = 0; i < postfix.Length; i++)
            {
                string number = String.Empty;

                if (char.IsDigit(postfix[i]))
                {
                    while (postfix[i] != ' ')
                    {
                        number += postfix[i];
                        i++;
                    }

                    stack.Push(Convert.ToDouble(number));
                }
                else if (operatorsPriority.ContainsKey(postfix[i]))
                {
                    double second = stack.Pop();
                    double first = stack.Pop();

                    stack.Push(Execute(first, postfix[i], second));
                }
            }

            return output.Append(stack.Pop());
        }

        private double Execute(double first, char op, double second)
        {
            double result = 0;

            switch (op)
            {
                case '+':
                    result = Math.Round(first + second, 2);
                    break;
                case '-':
                    result = Math.Round(first - second, 2);
                    break;
                case '*':
                    result = Math.Round(first * second, 2);
                    break;
                case '/':
                    result = Math.Round(first / second, 2);
                    break;
            }

            return result;
        }

        private Dictionary<char, int> operatorsPriority = new Dictionary<char, int>()
        {
            {'(', 0},
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2}
        };

        private StringBuilder DijkstrasAlgorithm(string input)
        {
            StringBuilder output = new StringBuilder();
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]) || input[i] == ',')
                {
                    output.Append(input[i]);
                }
                else if (input[i] == '(')
                {
                    stack.Push(input[i]);
                }
                else if (input[i] == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        output.Append(" ");
                        output.Append(stack.Pop());
                    }

                    stack.Pop();
                }
                else if (operatorsPriority.ContainsKey(input[i]))
                {
                    output.Append(" ");
                    if (stack.Count > 0 && (operatorsPriority[stack.Peek()] >= operatorsPriority[input[i]]))
                    {
                        output.Append(stack.Pop());
                    }

                    stack.Push(input[i]);
                }
            }

            while (stack.Count > 0)
            {
                output.Append(" ").Append(stack.Pop());
            }

            return output;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            enterFormula.Clear();
            enterFormula.AppendText("0");
        }
    }
}
