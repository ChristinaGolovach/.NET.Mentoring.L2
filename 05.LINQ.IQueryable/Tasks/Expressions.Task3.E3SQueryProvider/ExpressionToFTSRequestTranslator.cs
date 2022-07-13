using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Expressions.Task3.E3SQueryProvider
{
	public class ExpressionToFtsRequestTranslator : ExpressionVisitor
	{
		private readonly StringBuilder _resultStringBuilder;
		private IDictionary<string, ValueTuple<string, string>> stringMethodBrackets;

		public ExpressionToFtsRequestTranslator()
		{
			_resultStringBuilder = new StringBuilder();

			stringMethodBrackets = new Dictionary<string, ValueTuple<string, string>>
			{
				{ "Equals", ("(", ")") },
				{ "Contains", ("(*", "*)") },
				{ "StartsWith", ("(", "*)") },
				{ "EndsWith", ("(*", ")") },
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
			if (node.Method.DeclaringType == typeof(string) && stringMethodBrackets.ContainsKey(node.Method.Name))
			{
				VisitStringMethodCallNodes(node.Object, node.Arguments[0], node.Method.Name);

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
				var leftNode = node.Left;
				var rightNode = node.Right;

				Visit(leftNode);
				Visit(rightNode);

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
				VisitEqualQueryNodes(leftNode, rightNode);
			}
			else
			{
				VisitEqualQueryNodes(rightNode, leftNode);
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

		private void VisitEqualQueryNodes(Expression firtsNode, Expression secondNode)
		{
			Visit(firtsNode);
			_resultStringBuilder.Append("(");
			Visit(secondNode);
			_resultStringBuilder.Append(")");
		}

		private void VisitStringMethodCallNodes(Expression firtsNode, Expression secondNode, string MethodName)
		{
			var brackets = stringMethodBrackets[MethodName];
			Visit(firtsNode);
			_resultStringBuilder.Append(brackets.Item1);
			Visit(secondNode);
			_resultStringBuilder.Append(brackets.Item2);
		}
	}
}
