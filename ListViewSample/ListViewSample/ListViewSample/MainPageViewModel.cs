using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViewSample
{
    public class MainPageViewModel
    {
        public IList<string> Colors { get; } = new List<string> { "Red", "Blue", "Green" };
        private string _selectedColor;
        public string SelectedColor
        {
            set { _selectedColor = value; }
        }
    }
}
