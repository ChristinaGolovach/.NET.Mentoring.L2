using System;
using System.Collections.Generic;
using System.Text;

namespace Composite
{
	public class Form : Component
	{
		private static readonly string _elementName = "form";
		private string _name;
		private IList<Component> _components;

		public Form(string name) : base(_elementName)
		{
			_name = name;
			_components = new List<Component>();
		}

		public override string ConvertToString()
		{
			StringBuilder formConvertResult = new StringBuilder();
			formConvertResult.Append($"<{componentName} name='{_name}'> {Environment.NewLine}");

			foreach (Component component in _components)
			{
				var componentConverResult = component.ConvertToString();
				formConvertResult.Append(componentConverResult);
			}

			formConvertResult.Append($"</{_elementName}>");

			return formConvertResult.ToString();
		}

		public override void Add(Component component)
		{
			_components.Add(component);
		}

		public override void Remove(Component component)
		{
			_components.Remove(component);
		}
	}
}
