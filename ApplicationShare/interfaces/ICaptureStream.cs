using ApplicationShare.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationShare.interfaces
{
    public interface ICaptureStream
    {
        VideoSourceInfo DeviceInfo
        {
            get;
            set;
        }
    }
}
