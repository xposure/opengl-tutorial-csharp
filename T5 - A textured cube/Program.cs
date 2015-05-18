using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.IO;

namespace T5___A_textured_cube
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new GameWindow(1024, 768,                     //ask for this window size
                                             GraphicsMode.Default,          //opentk picks the default format
                                             "Tutorial 05 - A textured cube",//title in the window
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

                // Two UV coordinatesfor each vertex. They were created with Blender. You'll learn shortly how to do this yourself.
                var g_uv_buffer_data = new[] {
                    0.000059f, 1.0f-0.000004f,
                    0.000103f, 1.0f-0.336048f,
                    0.335973f, 1.0f-0.335903f,
                    1.000023f, 1.0f-0.000013f,
                    0.667979f, 1.0f-0.335851f,
                    0.999958f, 1.0f-0.336064f,
                    0.667979f, 1.0f-0.335851f,
                    0.336024f, 1.0f-0.671877f,
                    0.667969f, 1.0f-0.671889f,
                    1.000023f, 1.0f-0.000013f,
                    0.668104f, 1.0f-0.000013f,
                    0.667979f, 1.0f-0.335851f,
                    0.000059f, 1.0f-0.000004f,
                    0.335973f, 1.0f-0.335903f,
                    0.336098f, 1.0f-0.000071f,
                    0.667979f, 1.0f-0.335851f,
                    0.335973f, 1.0f-0.335903f,
                    0.336024f, 1.0f-0.671877f,
                    1.000004f, 1.0f-0.671847f,
                    0.999958f, 1.0f-0.336064f,
                    0.667979f, 1.0f-0.335851f,
                    0.668104f, 1.0f-0.000013f,
                    0.335973f, 1.0f-0.335903f,
                    0.667979f, 1.0f-0.335851f,
                    0.335973f, 1.0f-0.335903f,
                    0.668104f, 1.0f-0.000013f,
                    0.336098f, 1.0f-0.000071f,
                    0.000103f, 1.0f-0.336048f,
                    0.000004f, 1.0f-0.671870f,
                    0.336024f, 1.0f-0.671877f,
                    0.000103f, 1.0f-0.336048f,
                    0.336024f, 1.0f-0.671877f,
                    0.335973f, 1.0f-0.335903f,
                    0.667969f, 1.0f-0.671889f,
                    1.000004f, 1.0f-0.671847f,
                    0.667979f, 1.0f-0.335851f
                };

                var uvBuffer = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ArrayBuffer, uvBuffer);

                var uvBufferSize = new IntPtr(sizeof(float) * g_uv_buffer_data.Length);
                GL.BufferData(BufferTarget.ArrayBuffer, uvBufferSize, g_uv_buffer_data, BufferUsageHint.StaticDraw);

                var textureID = loadBMP_custom("uvtemplate.bmp");
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

                    // Use our texture
                    GL.BindTexture(TextureTarget.Texture2D, textureID);

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
                    GL.BindBuffer(BufferTarget.ArrayBuffer, uvBuffer);
                    GL.VertexAttribPointer(
                        1,                              // attribute index
                        2,                              // size
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

        public static int loadBMP_custom(string filepath)
        {
            // Data read from the header of the BMP file
            var header = new byte[54]; // Each BMP file begins by a 54-bytes header
            var dataPos = 0;     // Position in the file where the actual data begins
            var width = 0;
            var height = 0;
            var imageSize = 0;   // = width*height*3

            if (!System.IO.File.Exists(filepath))
            {
                Console.WriteLine("Image [{0}] could not be found.", filepath);
                return 0;
            }

            var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            try
            {
                if (fs.Read(header, 0, 54) != 54)
                {
                    // If not 54 bytes read : problem
                    Console.WriteLine("Not a correct BMP file [{0}]", filepath);
                    return 0;
                }

                if (header[0] != 'B' || header[1] != 'M')
                {
                    Console.WriteLine("Not a correct BMP file [{0}]", filepath);
                    return 0;
                }

                // Read ints from the byte array
                dataPos = BitConverter.ToInt32(header, 0x0A);
                imageSize = BitConverter.ToInt32(header, 0x22);
                width = BitConverter.ToInt32(header, 0x12);
                height = BitConverter.ToInt32(header, 0x16);

                // Some BMP files are misformatted, guess missing information
                if (imageSize == 0) imageSize = width * height * 3; // 3 : one byte for each Red, Green and Blue component
                if (dataPos == 0) dataPos = 54; // The BMP header is done that way

                // Actual RGB data
                var data = new byte[imageSize];
                fs.Read(data, 0, imageSize);

                // Create one OpenGL texture
                var textureID = GL.GenTexture();

                // "Bind" the newly created texture : all future texture functions will modify this texture
                GL.BindTexture(TextureTarget.Texture2D, textureID);

                // Give the image to OpenGL
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgb, width, height, 0, PixelFormat.Rgb, PixelType.UnsignedByte, data);

                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, new[] { (uint)TextureMagFilter.Nearest });
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new[] { (uint)TextureMinFilter.Nearest });

                return textureID;

            }
            finally
            {
                fs.Close();
                fs.Dispose();
            }
        }
    }
}
