using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace T2___The_VAO
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new GameWindow(1024, 768,                     //ask for this window size
                                             GraphicsMode.Default,          //opentk picks the default format
                                             "Tutorial 02 - The VAO",                 //title in the window
                                             GameWindowFlags.Default,       //creates a default window style
                                             DisplayDevice.Default,         //tells it to pick the default monitor
                                             3, 3,                          //we want opengl 3.3
                                             GraphicsContextFlags.Default   //default context
                                            ))
            {
                game.Visible = true;   //show the window

                var vertexArrayID = GL.GenVertexArray();
                GL.BindVertexArray(vertexArrayID);

                var g_vertex_buffer_data = new[] {
                    -1.0f, -1.0f, 0.0f,
                    1.0f,  -1.0f, 0.0f,
                    0.0f,   1.0f, 0.0f,
                };

                var vertexBuffer = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
                GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(sizeof(float) * g_vertex_buffer_data.Length), g_vertex_buffer_data, BufferUsageHint.StaticDraw);

                var programID = LoadShaders("simple.vert", "simple.frag");

                //variable to store the latest keyboard state
                KeyboardState keyboard;
                do
                {
                    // Draw something !

                    GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

                    GL.UseProgram(programID);

                    GL.EnableVertexAttribArray(0);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
                    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                    GL.DisableVertexAttribArray(0);
                    // Swap buffers

                    game.SwapBuffers();
                    game.ProcessEvents();

                    //we need to poll the current keyboard state
                    keyboard = OpenTK.Input.Keyboard.GetState();
                }   // Check if the ESC key was pressed or the window was closed
                // Also make sure they didn't click the "X" to close the window
                while (!game.IsExiting && !keyboard[Key.Escape]);
            }
        }

        private static int LoadShaders(string vertexPath, string fragmentPath)
        {
            var vertexID = GL.CreateShader(ShaderType.VertexShader);
            var fragmentID = GL.CreateShader(ShaderType.FragmentShader);
            try
            {
                LoadShader(vertexID, vertexPath, ShaderType.VertexShader);
                LoadShader(fragmentID, fragmentPath, ShaderType.FragmentShader);

                Console.WriteLine("Linking program...");
                var programID = GL.CreateProgram();
                GL.AttachShader(programID, vertexID);
                GL.AttachShader(programID, fragmentID);
                GL.LinkProgram(programID);

                var result = 0;
                GL.GetProgram(programID, GetProgramParameterName.LinkStatus, out result);
                var info = GL.GetProgramInfoLog(programID);
                Console.WriteLine("Linked program with result: {0}, info: {1}", result, info);
                
                return programID;
            }
            finally
            {
                GL.DeleteShader(vertexID);
                GL.DeleteShader(fragmentID);
            }
        }

        private static void LoadShader(int shaderID, string path, ShaderType shaderType)
        {
            using (var sr = new StreamReader(path))
            {
                //var shaderID = GL.CreateShader(shaderType);
                Console.WriteLine("Compiling shader {0}:{1}", shaderType, path);

                var code = sr.ReadToEnd();
                GL.ShaderSource(shaderID, code);
                GL.CompileShader(shaderID);

                var result = 0;
                GL.GetShader(shaderID, ShaderParameter.CompileStatus, out result);
                var info = GL.GetShaderInfoLog(shaderID);

                Console.WriteLine("Compiled shader {0}:{1} with result: {2}, info: {3}", shaderType, path, result, info);
            }
        }
    }
}
