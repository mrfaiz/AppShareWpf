using AForge.Video;
using ApplicationShare.interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationShare.utilities
{
    public class AppScreenCaptureStream : IVideoSource, ICaptureStream
    {
        private int frameInterval = 50;
        private int framesReceived = 0;

        private Thread thread = null;
        private ManualResetEvent stopEvent = null;
        private IntPtr WindowHandler = IntPtr.Zero;
        private VideoSourceInfo _DeviceInfo;

        public event NewFrameEventHandler NewFrame;

        public event VideoSourceErrorEventHandler VideoSourceError;

        public event PlayingFinishedEventHandler PlayingFinished;


        public virtual string Source
        {
            get { return "Screen Capture"; }
        }

        public int FrameInterval
        {
            get { return frameInterval; }
            set { frameInterval = Math.Max(0, value); }
        }

        public int FramesReceived
        {
            get
            {
                int frames = framesReceived;
                framesReceived = 0;
                return frames;
            }
        }

        public long BytesReceived
        {
            get { return 0; }
        }

        public VideoSourceInfo DeviceInfo
        {
            get { return _DeviceInfo; }
            set { _DeviceInfo = value; }
        }

        public bool IsRunning
        {
            get
            {
                if (thread != null)
                {
                    // check thread status
                    if (thread.Join(0) == false)
                        return true;

                    // the thread is not running, free resources
                    Free();
                }
                return false;
            }
        }

        public AppScreenCaptureStream(VideoSourceInfo deviceInfo)
        {
            this.DeviceInfo = deviceInfo;
        }

        public AppScreenCaptureStream(VideoSourceInfo deviceInfo, int frameInterval)
        {
            this.DeviceInfo = deviceInfo;
            this.FrameInterval = frameInterval;
        }

        public void Start()
        {
            if (!IsRunning)
            {
                framesReceived = 0;

                // create events
                stopEvent = new ManualResetEvent(false);
                WindowHandler = (IntPtr)Int32.Parse(this.DeviceInfo.Key);
                // create and start new thread
                thread = new Thread(new ThreadStart(WorkerThread));
                thread.Name = Source; // mainly for debugging
                thread.Start();
            }
        }

        public void SignalToStop()
        {
            // stop thread
            if (thread != null)
            {
                // signal to stop
                stopEvent.Set();
            }
        }

        public void WaitForStop()
        {
            if (thread != null)
            {
                // wait for thread stop
                thread.Join();
                Free();
            }
        }

        public void Stop()
        {
            if (this.IsRunning)
            {
                stopEvent.Set();
                thread.Abort();
                WaitForStop();
            }
        }

        private void Free()
        {
            thread = null;

            // release events
            stopEvent.Dispose();
            stopEvent = null;
        }

        // Worker thread
        private void WorkerThread()
        {

            // download start time and duration
            DateTime start;
            TimeSpan span;

            while (!stopEvent.WaitOne(0))
            {
                // set dowbload start time
                start = DateTime.Now;
                Bitmap bitmap = null;

                try
                {
                    // increment frames counter
                    framesReceived++;

                    if (WindowHandler != IntPtr.Zero && NativeLibrary.IsWindow(WindowHandler) && NativeLibrary.IsWindowVisible(WindowHandler) && !NativeLibrary.IsIconic(WindowHandler))
                    {
                        RECT rect = NativeLibrary.GetWindowRect(WindowHandler);
                        if (rect.Right > 0 && rect.Bottom > 0)
                        {
                            System.Drawing.Point lt = new System.Drawing.Point(rect.Left, rect.Top);
                            System.Drawing.Point rb = new System.Drawing.Point(rect.Right, rect.Bottom);
                            NativeLibrary.ClientToScreen(WindowHandler, ref lt);
                            NativeLibrary.ClientToScreen(WindowHandler, ref rb);
                            rect.Left = lt.X;
                            rect.Top = lt.Y;
                            rect.Right = rb.X;
                            rect.Bottom = rb.Y;

                            int width = rect.Right - rect.Left;
                            int height = rect.Bottom - rect.Top;

                            IntPtr hdcSrc = NativeLibrary.GetWindowDC(WindowHandler);
                            IntPtr hdcDest = NativeLibrary.CreateCompatibleDC(hdcSrc);
                            // create a bitmap we can copy it to,
                            // using GetDeviceCaps to get the width/height
                            IntPtr hBitmap = NativeLibrary.CreateCompatibleBitmap(hdcSrc, width, height);
                            // select the bitmap object
                            IntPtr hOld = NativeLibrary.SelectObject(hdcDest, hBitmap);
                            // bitblt over
                            NativeLibrary.BitBlt(hdcDest, 0, 0, width, height, hdcSrc, 0, 0, NativeLibrary.SRCCOPY);
                            // restore selection
                            NativeLibrary.SelectObject(hdcDest, hOld);
                            // clean up
                            NativeLibrary.DeleteDC(hdcDest);
                            NativeLibrary.ReleaseDC(WindowHandler, hdcSrc);
                            // get a .NET image object for it
                            bitmap = Image.FromHbitmap(hBitmap);
                            // free up the Bitmap object
                            NativeLibrary.DeleteObject(hBitmap);
                        }
                    }

                    // provide new image to clients
                    if (NewFrame != null)
                    {
                        // notify client
                        NewFrame(this, new NewFrameEventArgs(bitmap));
                    }

                    // wait for a while ?
                    if (frameInterval > 0)
                    {
                        // get download duration
                        span = DateTime.Now.Subtract(start);

                        // miliseconds to sleep
                        int msec = frameInterval - (int)span.TotalMilliseconds;

                        if ((msec > 0) && (stopEvent.WaitOne(msec)))
                            break;
                    }
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception exception)
                {
                    // provide information to clients
                    if (VideoSourceError != null)
                    {
                        VideoSourceError(this, new VideoSourceErrorEventArgs(exception.Message));
                    }
                    // wait for a while before the next try
                    Thread.Sleep(250);
                }
                finally
                {
                    if (bitmap != null)
                    {
                        bitmap.Dispose();
                        bitmap = null;
                    }
                }

                // need to stop ?
                if (stopEvent.WaitOne(0))
                    break;
            }

            NativeLibrary.DeleteObject(WindowHandler);

            if (PlayingFinished != null)
            {
                PlayingFinished(this, ReasonToFinishPlaying.StoppedByUser);
            }
        }
    }
}
