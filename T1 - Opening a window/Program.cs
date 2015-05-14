using System;
using OpenTK;
using OpenTK.Input;
using OpenTK.Graphics;

namespace T1___Opening_a_window
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new GameWindow(1024, 768,                         //ask for this window size
                                             GraphicsMode.Default,              //opentk picks the default format
                                             "Tutorial 01 - Opening a window",  //title in the window
                                             GameWindowFlags.Default,           //creates a default window style
                                             DisplayDevice.Default,             //tells it to pick the default monitor
                                             3, 3,                              //we want opengl 3.3
                                             GraphicsContextFlags.Default       //default context
                                            ))
            {
                game.Visible = true;   //show the window

                //variable to store the latest keyboard state
                KeyboardState keyboard;
                do
                {
                    // Draw nothing, see you in tutorial 2 !

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
    }
}
