Kraggs.Graphics.GlfwWindow
==========================

A simple GLFW Window abstract class.
For now using Kraggs.Graphics.Glfw2 for GLFW v2.x functionality.
Will on some later time be upgraded to GLFW v3.x when its released.

A work derived from the GLFW project at http://www.glfw.org

Howto Use:
--------------------------

Create a new Console Application:

Add to references:
	Kraggs.Graphics.GlfwWindow.dll
	Kraggs.Graphics.OpenGL.dll
	
Add to this code to Program.cs

using System;
using System.Collections.Generic;
using System.Text;

using Kraggs.Graphics.GlfwWindow;
using Kraggs.Graphics.OpenGL;	

namespace Sample
{
	public class Program : absGlfwWindow
	{
		public Program() 
			: base(Title: "GLFW Sample Program")
		{
			// Note: Window is not created yet.
		}
		
		// window is created but opengl functions is not loaded yet.		
		protected override void OnLoad()
		{		
			new GL().Initialize();
			// now all opengl functions is loaded 
			
			// do opengl function loading here.
		}
		
		protected override void OnResize(int Width, int Height)
		{
			GL.ClearColor(0.2f, 0.3f, 0.4f, 1.0f);
			// do setup glviewport here.
		}
		
		protected override void OnRender()
        {
			// rendering.
            GL.Clear(ClearBufferFlags.ColorBufferBit | ClearBufferFlags.DepthBufferBit | ClearBufferFlags.StencilBufferBit);

            System.Threading.Thread.Sleep(10);

            this.SwapBuffers();
        }
        static void Main(string[] args)
        {
            using (var glfw = new Program())
            {
                glfw.Run();
            }
        }		
	}
}