using System.ComponentModel;

namespace PropertyBindingSample.Models
{
    public interface IApplicationModel : INotifyPropertyChanged
    {
        string Title { get; set; }
    }
}