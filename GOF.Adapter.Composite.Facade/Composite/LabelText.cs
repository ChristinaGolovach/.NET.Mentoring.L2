using System;

namespace Composite
{
	public class LabelText : Component
	{
		private static readonly  string _elementName = "label";
		private string _value;

		public LabelText(string value) : base(_elementName)
		{
			_value = value;
		}

		public override string ConvertToString()
		{
			return $"<{componentName} value='{_value}'/> {Environment.NewLine}";
		}
	}
}
