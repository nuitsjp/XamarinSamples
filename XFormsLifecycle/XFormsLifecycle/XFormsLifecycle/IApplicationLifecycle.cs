using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XFormsLifecycle
{
    public interface IApplicationLifecycle
    {
        void OnSleep();

        void OnResume();
    }
}
