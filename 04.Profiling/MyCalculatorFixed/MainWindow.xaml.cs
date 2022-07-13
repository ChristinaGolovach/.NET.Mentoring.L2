// Decompiled with JetBrains decompiler
// Type: MyCalculatorv1.MainWindow
// Assembly: MyCalculatorv1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8E4247A5-25E4-47A6-84F4-A414933F7536
// Assembly location: C:\Users\Krystsina_Halavach\Downloads\DumpHomework\MyCalculator.exe

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;

namespace MyCalculatorv1
{
	public partial class MainWindow : Window, IComponentConnector
	{
		private static string[] operations = new[] { "+", "-", "/", "*" };

		public MainWindow()
		{
			this.InitializeComponent();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			string input = ((Button)sender).Content.ToString();
			string curentTextValue = this.tb.Text;
			bool isInputDigit = input.All(Char.IsDigit);

			if (curentTextValue == "0" && isInputDigit)
			{
				this.tb.Text = input;
			}
			else if (!isInputDigit && !curentTextValue.StartsWith("-") && operations.Any(curentTextValue.Contains))
			{
				Result();
				this.tb.Text += input;
			}
			else
			{
				this.tb.Text += ((Button)sender).Content.ToString();
			}
		}

		private void Result_click(object sender, RoutedEventArgs e) => this.Result();

		private void Result()
		{
			tb.Text = Calculator.Calculate(this.tb.Text).ToString();
		}

		private void Off_Click_1(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

		private void Del_Click(object sender, RoutedEventArgs e) => this.tb.Text = "";

		private void R_Click(object sender, RoutedEventArgs e)
		{
			if (this.tb.Text.Length <= 0)
				return;
			this.tb.Text = this.tb.Text.Substring(0, this.tb.Text.Length - 1);
		}
	}
}
