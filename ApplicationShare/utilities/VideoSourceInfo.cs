using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ApplicationShare.utilities
{
    public class VideoSourceInfo
    {
        public const int X_CROP = 180;
        public const int Y_CROP = 50;
        //X=>124/180
        //Y=>6/50
        public int DeviceType;
        public videoDataType DataType;
        public uint OrientationType;
        public String Name;
        public String Key;
        public int Width;
        public int Height;
        public int X;
        public int Y;

        public VideoSourceInfo()
        {
            this.DeviceType = AppConstants.MONITOR_SCREEN;
            this.DataType = videoDataType.screen;
            this.OrientationType = 0;
            this.Name = "Monitor Screen";
            this.Key = "-999";
            this.Width = 640;
            int th = (int)((SystemParameters.VirtualScreenHeight * 640) / SystemParameters.VirtualScreenWidth);
            this.Height = th - (th % 2);
            this.X = 0;
            this.Y = 0;
        }

        public VideoSourceInfo(FilterInfo filterInfo)
        {
            this.DeviceType = AppConstants.WEB_CAMERA;
            this.DataType = videoDataType.camera;
            this.OrientationType = 2;
            this.Name = filterInfo.Name;
            this.Key = filterInfo.MonikerString;
            this.Width = 640;
            this.Height = 480;
            this.X = VideoSourceInfo.X_CROP;
            this.Y = 0;
        }

        public VideoSourceInfo(string sourceName, int SourceID)
        {
            this.DeviceType = AppConstants.APP_SCREEN;
            this.DataType = videoDataType.screen;
            this.OrientationType = 0;
            this.Name = sourceName;
            this.Key = SourceID.ToString();
            this.Width = 640;
            this.Height = 480;
            this.X = 0;
            this.Y = 0;
        }

        public override string ToString()
        {
            return "VideoSourceInfo [DeviceType=" + this.DeviceType + ", Name=" + this.Name + ", Key=" + this.Key + "]";
        }

        public static List<VideoSourceInfo> GetDeviceList()
        {
            List<VideoSourceInfo> deviceList = new List<VideoSourceInfo>();

            FilterInfoCollection collection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo fi in collection)
            {
                deviceList.Add(new VideoSourceInfo(fi));
            }
            deviceList.Add(new VideoSourceInfo());

            return deviceList;
        }
    }
    public enum videoDataType
    {
        camera = 51,
        screen = 52
    }
}
