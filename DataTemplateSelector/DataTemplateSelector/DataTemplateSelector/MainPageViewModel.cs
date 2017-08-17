using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTemplateSelector
{
	public class MainPageViewModel
	{
		private Person _selectedPerson;
		public IList<Person> Persons { get; } = new List<Person>();

		public MainPageViewModel()
		{
			Persons.Add(new Person { Name = "山田　太郎", Gender = Gender.Male, Weight = 65 });
			Persons.Add(new Person { Name = "鈴木　花子", Gender = Gender.Female, Height = 158 });
			Persons.Add(new Person { Name = "斎藤　次郎", Gender = Gender.Male, Weight = 58 });
		}

		public Person SelectedPerson
		{
			get => _selectedPerson;
			set
			{
				if(Equals(_selectedPerson, value)) return;

				if (_selectedPerson != null)
					_selectedPerson.IsSelected = false;

				_selectedPerson = value;
				_selectedPerson.IsSelected = true;
			}
		}
	}
}
