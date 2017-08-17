using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataTemplateSelector
{
	public class Person : INotifyPropertyChanged
	{
		private bool _isSelected;
		public string Name { get; set; }
		public Gender Gender { get; set; }
		public int Height { get; set; }
		public double Weight { get; set; }

		public bool IsSelected
		{
			get => _isSelected;
			set => SetProperty(ref _isSelected, value);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
		{
			if(Equals(field, value)) return false;

			field = value;
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
			return true;
		}
	}
}
