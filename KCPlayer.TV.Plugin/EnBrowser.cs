using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KCPlayer.TV.Plugin
{
    public class EnBrowser : WebBrowser
    {
        private AxHost.ConnectionPointCookie cookie;
        private WebBrowserExtendedEvents events;

        //This method will be called to give you a chance to create your own event sink
        protected override void CreateSink()
        {
            //MAKE SURE TO CALL THE BASE or the normal events won't fire
            base.CreateSink();
            events = new WebBrowserExtendedEvents(this);
            cookie = new AxHost.ConnectionPointCookie(ActiveXInstance, events, typeof (DWebBrowserEvents2));
        }

        protected override void DetachSink()
        {
            if (null != cookie)
            {
                cookie.Disconnect();
                cookie = null;
            }
            base.DetachSink();
        }

        //This new event will fire when the page is navigating
        public event EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNavigate;
        public event EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNewWindow;

        protected void OnBeforeNewWindow(string url, out bool cancel)
        {
            EventHandler<WebBrowserExtendedNavigatingEventArgs> h = BeforeNewWindow;
            var args = new WebBrowserExtendedNavigatingEventArgs(url, null);
            if (null != h)
            {
                h(this, args);
            }
            cancel = args.Cancel;
        }

        protected void OnBeforeNavigate(string url, string frame, out bool cancel)
        {
            EventHandler<WebBrowserExtendedNavigatingEventArgs> h = BeforeNavigate;
            var args = new WebBrowserExtendedNavigatingEventArgs(url, frame);
            if (null != h)
            {
                h(this, args);
            }
            //Pass the cancellation chosen back out to the events
            cancel = args.Cancel;
        }

        //This class will capture events from the WebBrowser

        [ComImport, Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D"),
         InterfaceType(ComInterfaceType.InterfaceIsIDispatch),
         TypeLibType(TypeLibTypeFlags.FHidden)]
        public interface DWebBrowserEvents2
        {
            [DispId(250)]
            void BeforeNavigate2(
                [In,
                 MarshalAs(UnmanagedType.IDispatch)] object pDisp,
                [In] ref object URL,
                [In] ref object flags,
                [In] ref object targetFrameName, [In] ref object postData,
                [In] ref object headers,
                [In,
                 Out] ref bool cancel);

            [DispId(273)]
            void NewWindow3(
                [In,
                 MarshalAs(UnmanagedType.IDispatch)] object pDisp,
                [In, Out] ref bool cancel,
                [In] ref object flags,
                [In] ref object URLContext,
                [In] ref object URL);
        }

        private class WebBrowserExtendedEvents : StandardOleMarshalObject, DWebBrowserEvents2
        {
            private readonly EnBrowser _Browser;

            public WebBrowserExtendedEvents(EnBrowser browser)
            {
                _Browser = browser;
            }

            //Implement whichever events you wish
            public void BeforeNavigate2(object pDisp, ref object URL, ref object flags, ref object targetFrameName,
                                        ref object postData, ref object headers, ref bool cancel)
            {
                _Browser.OnBeforeNavigate((string) URL, (string) targetFrameName, out cancel);
            }

            public void NewWindow3(object pDisp, ref bool cancel, ref object flags, ref object URLContext,
                                   ref object URL)
            {
                _Browser.OnBeforeNewWindow((string) URL, out cancel);
            }
        }
    }

    public class WebBrowserExtendedNavigatingEventArgs : CancelEventArgs
    {
        private readonly string _Frame;
        private readonly string _Url;

        public WebBrowserExtendedNavigatingEventArgs(string url, string frame)
        {
            _Url = url;
            _Frame = frame;
        }

        public string Url
        {
            get { return _Url; }
        }

        public string Frame
        {
            get { return _Frame; }
        }
    }
}