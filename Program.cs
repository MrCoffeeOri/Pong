using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Pong
{
    class Program : GameWindow
    {
        float xDaBola = 0, yDaBola = 0, velocidadeDaBolaEmX = 3, velocidadeDaBolaEmY = 3;
        int tamanhoDaBola = 20, yDoJogador1 = 0, yDoJogador2 = 0;

        int XDoJogador1 => -ClientSize.Width / 2 + larguraDosJogadores/ 2;

        int xDoJogador2 => ClientSize.Width / 2 - larguraDosJogadores/ 2;

        int larguraDosJogadores => tamanhoDaBola;

        int alturaDosJogadores => 3 * tamanhoDaBola;

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            xDaBola += velocidadeDaBolaEmX;
            yDaBola += velocidadeDaBolaEmY;

            if (xDaBola + tamanhoDaBola / 2 > xDoJogador2- larguraDosJogadores/ 2
             && yDaBola - tamanhoDaBola / 2 < yDoJogador2 + alturaDosJogadores/ 2
             && yDaBola + tamanhoDaBola / 2 > yDoJogador2 - alturaDosJogadores/ 2)
            {
                velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
            }

            if (xDaBola - tamanhoDaBola / 2 < XDoJogador1+ larguraDosJogadores/ 2
             && yDaBola - tamanhoDaBola / 2 < yDoJogador1 + alturaDosJogadores/ 2
             && yDaBola + tamanhoDaBola / 2 > yDoJogador1 - alturaDosJogadores/ 2)
            {
                velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
            }

            if (yDaBola + tamanhoDaBola / 2 > ClientSize.Height / 2) velocidadeDaBolaEmY = -velocidadeDaBolaEmY;

            if (yDaBola - tamanhoDaBola / 2 < -ClientSize.Height / 2) velocidadeDaBolaEmY = -velocidadeDaBolaEmY;

            if (xDaBola < -ClientSize.Width / 2 || xDaBola > ClientSize.Width / 2)
            {
                xDaBola = 0;
                yDaBola = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Key.W)) yDoJogador1 += 5;

            if (Keyboard.GetState().IsKeyDown(Key.S)) yDoJogador1 -= 5;

            if (Keyboard.GetState().IsKeyDown(Key.Up)) yDoJogador2 += 5;

            if (Keyboard.GetState().IsKeyDown(Key.Down)) yDoJogador2 -= 5;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Desenhos dos retângulos em game..
            DesenharRetangulo((int)xDaBola, (int)yDaBola, tamanhoDaBola, tamanhoDaBola, 1.0f, 1.0f, 0.0f);
            DesenharRetangulo(XDoJogador1, yDoJogador1, larguraDosJogadores, alturaDosJogadores, 1.0f, 0.0f, 0.0f);
            DesenharRetangulo(xDoJogador2, yDoJogador2, larguraDosJogadores, alturaDosJogadores, 0.0f, 0.0f, 1.0f);

            SwapBuffers();
        }

        void DesenharRetangulo(int x, int y, int largura, int altura, float r, float g, float b)
        {
            GL.Color3(r, g, b);
            GL.Begin(PrimitiveType.Quads);

            // Multiplicação transforma, e soma translada..
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
            GL.End();
        }

        static void Main()
        {
            new Program().Run();
        }
    }
}