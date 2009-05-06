using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Simple.UI.Windows
{
    /// <summary>
    /// Summary description for SingleApp.
    /// </summary>
    public class SingleInstance
    {
        public SingleInstance()
        {

        }
        /// <summary>
        /// Imports 
        /// </summary>

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int IsIconic(IntPtr hWnd);

        /// <summary>
        /// GetCurrentInstanceWindowHandle
        /// </summary>
        /// <returns></returns>
        private static IntPtr GetCurrentInstanceWindowHandle()
        {
            IntPtr hWnd = IntPtr.Zero;
            Process process = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(process.ProcessName);
            foreach (Process _process in processes)
            {
                // Get the first instance that is not this instance, has the
                // same process name and was started from the same file name
                // and location. Also check that the process has a valid
                // window handle in this session to filter out other user's
                // processes.
                if (_process.Id != process.Id &&
                    _process.MainModule.FileName == process.MainModule.FileName &&
                    _process.MainWindowHandle != IntPtr.Zero)
                {
                    hWnd = _process.MainWindowHandle;
                    break;
                }
            }
            return hWnd;
        }
        /// <summary>
        /// SwitchToCurrentInstance
        /// </summary>
        private static void SwitchToCurrentInstance()
        {
            IntPtr hWnd = GetCurrentInstanceWindowHandle();
            if (hWnd != IntPtr.Zero)
            {
                // Restore window if minimised. Do not restore if already in
                // normal or maximised window state, since we don't want to
                // change the current state of the window.
                if (IsIconic(hWnd) != 0)
                {
                    ShowWindow(hWnd, SW_RESTORE);
                }

                // Set foreground window.
                SetForegroundWindow(hWnd);
            }
        }

        /// <summary>
        /// Execute a form base application if another instance already running on
        /// the system activate previous one
        /// </summary>
        /// <param name="frmMain">main form</param>
        /// <returns>true if no previous instance is running</returns>
        public static bool Run(Form mainForm, Type entryPointType)
        {
            if (IsAlreadyRunning(entryPointType))
            {
                //set focus on previously running app
                SwitchToCurrentInstance();
                return false;
            }
            Application.Run(mainForm);
            return true;
        }

        /// <summary>
        /// for console base application
        /// </summary>
        /// <returns></returns>
        public static bool Run(Type type)
        {
            if (IsAlreadyRunning(type))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// check if given exe alread running or not
        /// </summary>
        /// <returns>returns true if already running</returns>
        private static bool IsAlreadyRunning(Type type)
        {
            string mutexName = type.GUID.ToString();
            bool createdNew = false;

            mutex = new Mutex(true, mutexName, out createdNew);
            if (createdNew)
                mutex.ReleaseMutex();

            return !createdNew;
        }

        static Mutex mutex;
        const int SW_RESTORE = 9;
    }

}
