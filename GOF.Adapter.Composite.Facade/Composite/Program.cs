using System;

namespace Composite
{
	class Program
	{
		static void Main(string[] args)
		{
			var label = new LabelText("cityLabel");
			var inputText = new InputText("city", "Gdansk");
			var form = new Form("cityForm");

			form.Add(label);
			form.Add(inputText);

			Console.WriteLine(form.ConvertToString());

			Console.ReadKey();
		}
	}
}
