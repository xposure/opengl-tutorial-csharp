using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace T4___A_colored_cube
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

                // Our vertices. Tree consecutive floats give a 3D vertex; Three consecutive vertices give a triangle.
                // A cube has 6 faces with 2 triangles each, so this makes 6*2=12 triangles, and 12*3 vertices
                var g_vertex_buffer_data = new[] {
                    -1.0f,-1.0f,-1.0f, // triangle 1 : begin
                    -1.0f,-1.0f, 1.0f,
                    -1.0f, 1.0f, 1.0f, // triangle 1 : end
                    1.0f, 1.0f,-1.0f, // triangle 2 : begin
                    -1.0f,-1.0f,-1.0f,
                    -1.0f, 1.0f,-1.0f, // triangle 2 : end
                    1.0f,-1.0f, 1.0f,
                    -1.0f,-1.0f,-1.0f,
                    1.0f,-1.0f,-1.0f,
                    1.0f, 1.0f,-1.0f,
                    1.0f,-1.0f,-1.0f,
                    -1.0f,-1.0f,-1.0f,
                    -1.0f,-1.0f,-1.0f,
                    -1.0f, 1.0f, 1.0f,
                    -1.0f, 1.0f,-1.0f,
                    1.0f,-1.0f, 1.0f,
                    -1.0f,-1.0f, 1.0f,
                    -1.0f,-1.0f,-1.0f,
                    -1.0f, 1.0f, 1.0f,
                    -1.0f,-1.0f, 1.0f,
                    1.0f,-1.0f, 1.0f,
                    1.0f, 1.0f, 1.0f,
                    1.0f,-1.0f,-1.0f,
                    1.0f, 1.0f,-1.0f,
                    1.0f,-1.0f,-1.0f,
                    1.0f, 1.0f, 1.0f,
                    1.0f,-1.0f, 1.0f,
                    1.0f, 1.0f, 1.0f,
                    1.0f, 1.0f,-1.0f,
                    -1.0f, 1.0f,-1.0f,
                    1.0f, 1.0f, 1.0f,
                    -1.0f, 1.0f,-1.0f,
                    -1.0f, 1.0f, 1.0f,
                    1.0f, 1.0f, 1.0f,
                    -1.0f, 1.0f, 1.0f,
                    1.0f,-1.0f, 1.0f
                };



                // This will identify our vertex buffer
                // Generate 1 buffer, put the resulting identifier in vertexbuffer
                var vertexBuffer = GL.GenBuffer();

                // The following commands will talk about our 'vertexbuffer' buffer
                GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);

                // Compute the size of our array
                var vertexBufferSize = new IntPtr(sizeof(float) * g_vertex_buffer_data.Length);

                // Give our vertices to OpenGL
                GL.BufferData(BufferTarget.ArrayBuffer, vertexBufferSize, g_vertex_buffer_data,
                                BufferUsageHint.StaticDraw);

                // One color for each vertex. They were generated randomly.
                var g_color_buffer_data = new[] {
                    0.583f,  0.771f,  0.014f,
                    0.609f,  0.115f,  0.436f,
                    0.327f,  0.483f,  0.844f,
                    0.822f,  0.569f,  0.201f,
                    0.435f,  0.602f,  0.223f,
                    0.310f,  0.747f,  0.185f,
                    0.597f,  0.770f,  0.761f,
                    0.559f,  0.436f,  0.730f,
                    0.359f,  0.583f,  0.152f,
                    0.483f,  0.596f,  0.789f,
                    0.559f,  0.861f,  0.639f,
                    0.195f,  0.548f,  0.859f,
                    0.014f,  0.184f,  0.576f,
                    0.771f,  0.328f,  0.970f,
                    0.406f,  0.615f,  0.116f,
                    0.676f,  0.977f,  0.133f,
                    0.971f,  0.572f,  0.833f,
                    0.140f,  0.616f,  0.489f,
                    0.997f,  0.513f,  0.064f,
                    0.945f,  0.719f,  0.592f,
                    0.543f,  0.021f,  0.978f,
                    0.279f,  0.317f,  0.505f,
                    0.167f,  0.620f,  0.077f,
                    0.347f,  0.857f,  0.137f,
                    0.055f,  0.953f,  0.042f,
                    0.714f,  0.505f,  0.345f,
                    0.783f,  0.290f,  0.734f,
                    0.722f,  0.645f,  0.174f,
                    0.302f,  0.455f,  0.848f,
                    0.225f,  0.587f,  0.040f,
                    0.517f,  0.713f,  0.338f,
                    0.053f,  0.959f,  0.120f,
                    0.393f,  0.621f,  0.362f,
                    0.673f,  0.211f,  0.457f,
                    0.820f,  0.883f,  0.371f,
                    0.982f,  0.099f,  0.879f
                };

                var colorBuffer = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer);

                var colorBufferSize = new IntPtr(sizeof(float) * g_color_buffer_data.Length);
                GL.BufferData(BufferTarget.ArrayBuffer, colorBufferSize, g_color_buffer_data, BufferUsageHint.StaticDraw);

                var programID = LoadShaders("simple.vert", "simple.frag");


                // Projection matrix : 45° Field of View, 4:3 ratio, display range : 0.1 unit <-> 100 units
                var Projection = Matrix4.CreatePerspectiveFieldOfView(0.785398163f, 4.0f / 3.0f, 0.1f, 100.0f);
                // Camera matrix
                var View = Matrix4.LookAt(
                        new Vector3(4, 3, 3), // Camera is at (4,3,3), in world space
                        new Vector3(0, 0, 0), // and looks at the origin
                        new Vector3(0, 1, 0) // head is up (set to 0,-1,0 to look upside-down
                    );
                // Model matrix : an identity matrix (model will be at the origin)
                var Model = Matrix4.Identity;
                // Our ModelViewProjection : multiplication of our 3 matrices
                var MVP = Projection * View * Model;
                MVP = Model * View * Projection;

                // Get a handle for our "MVP" uniform.
                // Only at initialisation time.
                var MatrixID = GL.GetUniformLocation(programID, "MVP");

                // Send our transformation to the currently bound shader,
                // in the "MVP" uniform
                // For each model you render, since the MVP will be different (at least the M part)
                GL.UseProgram(programID);
                GL.UniformMatrix4(MatrixID, false, ref MVP);

                // Enable depth test
                GL.Enable(EnableCap.DepthTest);
                // Accept fragment if it closer to the camera than the former one
                GL.DepthFunc(DepthFunction.Less);
                //variable to store the latest keyboard state
                KeyboardState keyboard;
                do
                {
                    // Clear the screen
                    GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);

                    // Use our shader
                    GL.UseProgram(programID);
                    GL.UniformMatrix4(MatrixID, false, ref MVP);

                    GL.EnableVertexAttribArray(0);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer);
                    GL.VertexAttribPointer(
                        0,                             // attribute 0. No particular reason for 0, 
                        // but must match the layout in the shader.
                        3,                             // size
                        VertexAttribPointerType.Float, // type
                        false,                         // normalized?
                        0,                             // stride
                        0                              // array buffer offset
                    );


                    GL.EnableVertexAttribArray(1);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, colorBuffer);
                    GL.VertexAttribPointer(
                        1,                              // attribute index
                        3,                              // size
                        VertexAttribPointerType.Float,  // type
                        false,                          // normalized?
                        0,                              // stride
                        0                               // offset
                    );

                    // Draw the triangle !
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 12 * 3); // 12*3 indices starting at 0 -> 12 triangles -> 6 squares

                    GL.DisableVertexAttribArray(1);
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

                Console.WriteLine("Linking program {0}, {1}", vertexPath, fragmentPath);
                var programID = GL.CreateProgram();
                GL.AttachShader(programID, vertexID);
                GL.AttachShader(programID, fragmentID);
                GL.LinkProgram(programID);

                var result = 0;
                GL.GetProgram(programID, GetProgramParameterName.LinkStatus, out result);
                if (result != 1)
                {
                    var info = GL.GetProgramInfoLog(programID);
                    Console.WriteLine("FAILED: Linking program with result: {0}, info: {1}", result, info);
                }

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
                if (result != 1)
                {
                    var info = GL.GetShaderInfoLog(shaderID);
                    Console.WriteLine("FAILED: Compiling shader {0}:{1} with result: {2}, info: {3}", shaderType, path, result, info);
                }
            }
        }
    }
}
