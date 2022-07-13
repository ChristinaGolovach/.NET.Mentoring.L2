using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculatorv1
{
	public static class Calculator
	{
		private static char[] operationSymbols = new[] { '+', '-', '/', '*' };

		public static double Calculate(string mathematicalExpression)
		{
			string operation;
			int operationIndex;
			double firstOperand;
			double secondOperand;

			if (mathematicalExpression.StartsWith("-"))
			{
				var firstArg = mathematicalExpression.Substring(0, mathematicalExpression.IndexOfAny(operationSymbols, 1));
				firstOperand = Convert.ToDouble(firstArg);
				operation = mathematicalExpression.Substring(firstArg.Length, 1);
				secondOperand = Convert.ToDouble(mathematicalExpression.Substring(firstArg.Length + operation.Length));
			}
			else
			{
				operationIndex = mathematicalExpression.IndexOfAny(operationSymbols);
				operation = mathematicalExpression.Substring(operationIndex, 1);
				firstOperand = Convert.ToDouble(mathematicalExpression.Substring(0, operationIndex));
				secondOperand = Convert.ToDouble(mathematicalExpression.Substring(operationIndex + 1, mathematicalExpression.Length - operationIndex - 1));
			}

			return Calculate(firstOperand, secondOperand, operation);
		}

		private static double Calculate(double firstOperand, double secondOperand, string operation)
		{
			if (operation == "+")
			{
				return firstOperand + secondOperand;
			}
			else if (operation == "-")
			{
				return firstOperand - secondOperand;
			}
			else if (operation == "*")
			{
				return firstOperand * secondOperand;
			}
			else
			{
				return firstOperand / secondOperand;
			}
		}
	}
}
