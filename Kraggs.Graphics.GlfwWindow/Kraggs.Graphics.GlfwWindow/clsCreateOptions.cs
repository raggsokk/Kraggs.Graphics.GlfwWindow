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

namespace Kraggs.Graphics.GlfwWindow
{
    internal class clsCreateOptions
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string Title { get; set; }

        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Alpha { get; set; }
        public int Depth { get; set; }
        public int Stencil { get; set; }

        public int GLMajor { get; set; }
        public int GLMinor { get; set; }

        public bool CoreContext { get; set; }
        public bool DebugContext { get; set; }
        public bool ForwaredCompatible { get; set; }
        public bool Fullscreen { get; set; }

        public int RefreshRate { get; set; }
        public int FSAASamples { get; set; }

    }
}
