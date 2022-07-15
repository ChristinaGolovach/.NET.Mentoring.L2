using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Expressions.Task3.E3SQueryProvider.Models.Translator;

namespace Expressions.Task3.E3SQueryProvider
{
	public class ExpressionToFtsRequestTranslator : ExpressionVisitor
	{
		private readonly StringBuilder _resultStringBuilder;
		private IDictionary<string, ValueTuple<string, string>> bracketNames;

		public ExpressionToFtsRequestTranslator()
		{
			_resultStringBuilder = new StringBuilder();

			bracketNames = new Dictionary<string, ValueTuple<string, string>>
			{
				{ BracketName.EqualsName, ("(", ")") },
				{ BracketName.Contains, ("(*", "*)") },
				{ BracketName.StartsWith, ("(", "*)") },
				{ BracketName.EndsWith, ("(*", ")") },
				{ BracketName.Default, ("(", ")") },
				{ BracketName.AND, (BracketName.AND, string.Empty) }
			};
		}

		public string Translate(Expression exp)
		{
			Visit(exp);

			return _resultStringBuilder.ToString();
		}

		#region protected methods

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (node.Method.DeclaringType == typeof(Queryable)
				&& node.Method.Name == "Where")
			{
				var predicate = node.Arguments[1];
				Visit(predicate);

				return node;
			}
			if (node.Method.DeclaringType == typeof(string) && bracketNames.ContainsKey(node.Method.Name))
			{
				VisitQueryNodes(node.Object, node.Arguments[0], node.Method.Name);

				return node;
			}

			return base.VisitMethodCall(node);
		}

		protected override Expression VisitBinary(BinaryExpression node)
		{
			switch (node.NodeType)
			{
				case ExpressionType.Equal:
					HandleEqualQuery(node);
				break;
				case ExpressionType.AndAlso:
					VisitQueryNodes(node.Left, node.Right, BracketName.AND);
				break;

				default:
				throw new NotSupportedException($"Operation '{node.NodeType}' is not supported");
			};

			return node;
		}

		protected override Expression VisitMember(MemberExpression node)
		{
			_resultStringBuilder.Append(node.Member.Name).Append(":");

			return base.VisitMember(node);
		}

		protected override Expression VisitConstant(ConstantExpression node)
		{
			_resultStringBuilder.Append(node.Value);

			return node;
		}

		#endregion


		private void HandleEqualQuery(BinaryExpression node)
		{
			ValidateBinaryExpression(node);

			var leftNode = node.Left;
			var rightNode = node.Right;

			if (leftNode.NodeType == ExpressionType.MemberAccess)
			{
				VisitQueryNodes(leftNode, rightNode, BracketName.Default);
			}
			else
			{
				VisitQueryNodes(rightNode, leftNode, BracketName.Default);
			}
		}

		private void ValidateBinaryExpression(BinaryExpression node)
		{
			if ((node.Left.NodeType != ExpressionType.MemberAccess && node.Right.NodeType != ExpressionType.Constant) &&
				(node.Left.NodeType != ExpressionType.Constant && node.Right.NodeType != ExpressionType.MemberAccess))
			{
				throw new NotSupportedException($"Left operand should be property/field and Right operand should be constant. Or Left operand should be constant and Right operand should be property/field.");
			}
		}

		private void VisitQueryNodes(Expression firtsNode, Expression secondNode, string bracketName)
		{
			var brackets = bracketNames[bracketName];
			Visit(firtsNode);
			_resultStringBuilder.Append(brackets.Item1);
			Visit(secondNode);
			_resultStringBuilder.Append(brackets.Item2);
		}
	}
}
