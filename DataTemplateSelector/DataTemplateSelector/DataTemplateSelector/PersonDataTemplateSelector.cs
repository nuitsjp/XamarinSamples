using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DataTemplateSelector
{
	public class PersonDataTemplateSelector : Xamarin.Forms.DataTemplateSelector
	{
		public DataTemplate MaleTemplate { get; set; }
		public DataTemplate FemaleTemplate { get; set; }
		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			return (item as Person)?.Gender == Gender.Male
				? MaleTemplate
				: FemaleTemplate;
		}
	}
}
