#region License
/*
Copyright (c) 2012 Jarle Hansen <jarle.hansen@gmail.com>

This software is provided 'as-is', without any express or implied
warranty. In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would
   be appreciated but is not required.

2. Altered source versions must be plainly marked as such, and must not
   be misrepresented as being the original software.

3. This notice may not be removed or altered from any source
   distribution.
*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kraggs.Graphics.Glfw2;

namespace Kraggs.Graphics.GlfwWindow
{
    public abstract class absGlfwWindow : IDisposable
    {
        private bool flagRunning;
        private bool flagCreated;

        private clsCreateOptions pCreateOptions;

        #region Constructors

        protected absGlfwWindow(int Width = 640, int Height = 480, string Title = "GLFW OpenGL Window",
            int red = 0, int green = 0, int blue = 0, int alpha = 0, int depth = 0, int stencil = 0,
            int GLMajor = 4, int GLMinor = 3, bool CoreProfile = true, bool DebugContext = false, bool Fullscreen = false)
        {
            // handles initializing of glfw library.
            absGlfwWindow.Initialize();

            pCreateOptions = new clsCreateOptions()
            {
                Width = Width,
                Height = Height,
                Title = Title,
                Red = red,
                Green = green,
                Blue = blue,
                Alpha = alpha,
                Depth = depth,
                Stencil = stencil,
                GLMajor = GLMajor,
                GLMinor = GLMinor,
                CoreContext = CoreProfile,
                DebugContext = DebugContext,
                ForwaredCompatible = !CoreProfile,
                Fullscreen = Fullscreen,
                RefreshRate = 0,
                FSAASamples = 0,
            };


            // validate create options.
        }

        #endregion        

        #region Public

        public int Width
        {
            get
            {
                return pCreateOptions.Width;
            }
        }

        public int Height
        {
            get
            {
                return pCreateOptions.Height;
            }
        }


        public string Title
        {
            get
            {
                return pCreateOptions.Title;
            }
            set
            {
                pCreateOptions.Title = value;

                if(flagCreated)
                    glfw2.SetWindowTitle(value);
            }
        }

        public int RedBits
        {
            get
            {
                if (flagCreated)
                    return glfw2.GetWindowParam(GlfwWindowParameterName.RedBits);
                else
                    return pCreateOptions.Red;
            }
        }

        public int GreenBits
        {
            get
            {
                if(flagCreated)
                    return glfw2.GetWindowParam(GlfwWindowParameterName.GreenBits);
                else
                    return pCreateOptions.Green;
            }
        }

        public int BlueBits
        {
            get
            {
                if (flagCreated)
                    return glfw2.GetWindowParam(GlfwWindowParameterName.BlueBits);
                else
                    return pCreateOptions.Blue;
            }
        }
        public int AlphaBits
        {
            get
            {
                if (flagCreated)
                    return glfw2.GetWindowParam(GlfwWindowParameterName.AlphaBits);
                else
                    return pCreateOptions.Alpha;
            }
        }

        public int DepthBits
        {
            get
            {
                if (flagCreated)
                    return glfw2.GetWindowParam(GlfwWindowParameterName.DepthBits);
                else
                    return pCreateOptions.Depth;
            }
        }
        public int StencilBits
        {
            get
            {
                if (flagCreated)
                    return glfw2.GetWindowParam(GlfwWindowParameterName.StencilBits);
                else
                    return pCreateOptions.Stencil;
            }
        }

        public bool IsActive
        {
            get
            {
                if (flagCreated)
                    return glfw2.GetWindowParam(GlfwWindowParameterName.IsActive) != 0;
                else
                    return false;
            }
        }

        public bool IsIconified
        {
            get
            {
                if (flagCreated)
                    return glfw2.GetWindowParam(GlfwWindowParameterName.IsIconified) != 0;
                else
                    return false;
            }
        }

        public int RefreshRate
        {
            get
            {
                if (flagCreated)
                    return glfw2.GetWindowParam(GlfwWindowParameterName.RefreshRate);
                else
                    return pCreateOptions.RefreshRate;
            }
        }

        public int FSAASamples
        {
            get
            {
                if (flagCreated)
                    return glfw2.GetWindowParam(GlfwWindowParameterName.FSAASamples);
                else
                    return pCreateOptions.FSAASamples;
            }
        }

        public int SwapInterval
        {
            set
            {
                glfw2.SwapInterval(value);
            }
        }

        public void Run()
        {
            if (pCreateOptions.GLMajor > 2)
            {
                glfw2.OpenWindowHint(GlfwHintTarget.OpenGLVersionMajor, pCreateOptions.GLMajor);

                if (pCreateOptions.GLMinor > 0)
                    glfw2.OpenWindowHint(GlfwHintTarget.OpenGLVersionMinor, pCreateOptions.GLMinor);
            }

            if (pCreateOptions.CoreContext)
            {
                glfw2.OpenWindowHintProfile(GlfwOpenGLProfile.Core);                
            }
            else
            {
                glfw2.OpenWindowHintProfile(GlfwOpenGLProfile.Compatible);
            }

            if (pCreateOptions.ForwaredCompatible)
                glfw2.OpenWindowHint(GlfwHintTarget.OpenGLForwaredCompatible, 1);

            if(pCreateOptions.DebugContext)
                glfw2.OpenWindowHint(GlfwHintTarget.OpenGLDebugContext, 1);





            if (glfw2.OpenWindow(pCreateOptions.Width, pCreateOptions.Height, pCreateOptions.Red, pCreateOptions.Green, pCreateOptions.Blue, pCreateOptions.Alpha, pCreateOptions.Depth, pCreateOptions.Stencil,
                pCreateOptions.Fullscreen == true ? GlfwWindowMode.Fullscreen : GlfwWindowMode.Window) == 0)
            {
                //absGlfwWindow.Terminate();
                throw new ArgumentException("Failed to create opengl window.");
            }
            else
            {
                glfw2.SetWindowTitle(pCreateOptions.Title);

                int w, h;
                glfw2.GetWindowSize(out w, out h);
                pCreateOptions.Width = w;
                pCreateOptions.Height = h;

                //int major, minor, rev;
                //glfw2.GetOpenGLVersion(out major, out minor, out rev);
                //pCreateOptions.
                //int red, green, blue, alpha, depth, stencil;
                //pCreateOptions.Red = glfw2.GetWindowParam(GlfwWindowParameterName.RedBits);
                //pCreateOptions.Green = glfw2.GetWindowParam(GlfwWindowParameterName.GreenBits);
                //pCreateOptions.Blue = glfw2.GetWindowParam(GlfwWindowParameterName.BlueBits);
                //pCreateOptions.Alpha = glfw2.GetWindowParam(GlfwWindowParameterName.AlphaBits);
                //pCreateOptions.Depth = glfw2.GetWindowParam(GlfwWindowParameterName.DepthBits);
                //pCreateOptions.Stencil = glfw2.GetWindowParam(GlfwWindowParameterName.StencilBits);
                //pCreateOptions.Red = glfw2.GetWindowParam(GlfwWindowParameterName.RedBits);

                flagCreated = true;
            }            

            OnLoad();

            glfw2.SetWindowSizeCallback(new glfw2.delWindowResizeCallback(this.OnResize));
            glfw2.SetWindowCloseCallback(new glfw2.delWindowCloseCallback(this.OnClosingPrivate));
            //glfw2.SetMouseButtonCallback(new glfw2.delMouseButtonCallback(

            // mouse callbacks.
            glfw2.SetMouseButtonCallback(new glfw2.delMouseButtonCallback(OnMouseButton));
            glfw2.SetMousePositionCallback(new glfw2.delMousePositionCallback(OnMousePosition));
            glfw2.SetMouseWheelCallback(new glfw2.delMouseWheelCallback(OnMouseWheel));
            // keys.
            glfw2.SetCharCallback(new glfw2.delWindowCharCallback(OnUnicodeChar));
            glfw2.SetKeyCallback(new glfw2.delWindowKeyCallback(OnKey));

            flagRunning = flagCreated;

            while (flagRunning)
            {
                flagRunning = glfw2.GetWindowParam(GlfwWindowParameterName.IsOpened) != 0;

                if(flagRunning)
                    OnRender();

                
            }

            this.Dispose();
        }

        protected void SwapBuffers()
        {
            glfw2.SwapBuffers();
        }

        #endregion

        #region OnHandlers

        private int OnClosingPrivate()
        {
            var close = this.OnClosing();

            flagRunning = !close;

            //if (close)
            //    return 1;
            //else
                return 0;
        }

        protected virtual bool OnClosing()
        {
            return true;
        }

        protected virtual void OnMouseButton(GlfwMouseButton button, GlfwActionState action)
        {
            // since this is not overriden, disable this.
            glfw2.SetMouseButtonCallback(null);
        }

        protected virtual void OnMousePosition(int x, int y)
        {
            // since this is not overriden, disable this.
            glfw2.SetMousePositionCallback(null);
        }

        protected virtual void OnMouseWheel(int pos)
        {
            // since this is not overriden, disable this.
            glfw2.SetMouseWheelCallback(null);
        }

        protected virtual void OnUnicodeChar(int UnicodeChar, GlfwActionState action)
        {
            // since this is not overriden, disable this.
            glfw2.SetCharCallback(null);
        }

        protected virtual void OnKey(GlfwKeys key, GlfwActionState action)
        {
            // since this is not overriden, disable this.
            //glfw2.SetKeyCallback(null);
            if (key == GlfwKeys.Escape && action == GlfwActionState.Released)
                flagRunning = false;
        }

        protected abstract void OnLoad();

        protected abstract void OnResize(int Width, int Height);

        protected abstract void OnRender();

        #endregion

        #region IDisposable

        protected bool flagDisposed;

        public void Dispose()
        {
            if (!flagDisposed)
            {
                lock (this)
                {
                    if (!flagDisposed)
                    {
                        flagDisposed = OnDispose(true);

                        if (flagDisposed)
                            GC.SuppressFinalize(this);
                    }
                }
            }
        }

        protected bool OnDispose(bool disposing)
        {
            if (disposing)
            {
            }

            if(flagCreated)
                glfw2.CloseWindow();


            absGlfwWindow.Terminate();

            return true;
        }

        ~absGlfwWindow()
        {
            OnDispose(false);
        }

        #endregion

        #region Static

        internal static volatile int sWindowCount;

        static absGlfwWindow()
        {
            Initialize();
        }

        internal static void Initialize()
        {
            // called every time a class is created

            if(sWindowCount == 0)
                glfw2.Init();
            else
                sWindowCount++;
                        
        }

        internal static void Terminate()
        {
            // should be called every time a class is removed.
            sWindowCount--;

            if (sWindowCount == 0)
                glfw2.Terminate();
        }

        #endregion

    }
}
