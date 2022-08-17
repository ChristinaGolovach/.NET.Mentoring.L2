using System;

namespace Composite
{
	public class InputText : Component
	{
		private static readonly string _elementName = "inputText";
		private string _name;
		private string _value;

		public InputText(string name, string value) : base (_elementName)
		{
			_name = name;
			_value = value;
		}

		public override string ConvertToString()
		{
			return $"<{componentName} name='{_name}' value='{_value}'/> {Environment.NewLine}";
		}
	}
}
