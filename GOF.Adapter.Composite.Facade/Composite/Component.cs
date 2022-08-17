using System;

namespace Composite
{
	public abstract class Component
	{
		protected string componentName;
		public string ComponentName { get => componentName; }

		public Component(string componentName)
		{
			this.componentName = componentName;
		}

		public abstract string ConvertToString();

		public virtual void Add(Component component) => throw new NotImplementedException();

		public virtual void Remove(Component component) => throw new NotImplementedException();

		public virtual bool IsComposite() => true;

	}
}
