using System;
using System.Drawing;
// Getting access to Marshall class:
//  *Marshall
using System.Runtime.InteropServices;
using System.Windows.Forms;
/// <summary>
/// Libraries for linking OpenGL
/// </summar

// Access to FreeGlut framework

// Getting access to OpenGl classes:
//  *Gl
//  *Glu
using Tao.OpenGl;
// Getting access to OpenGl classes:
//  *Wgl
//  *GDI
//  *User
using Tao.Platform.Windows;
using System.Text;
using System.Drawing.Imaging;


namespace OpenGlForm
{
    public partial class Form1 : Form
    {
        IntPtr Handle3D;
        IntPtr HDC3D;
        IntPtr HRC3D;

        float r = 10;
        float phi = 30;
        float psi = 30;

        // true - рисование с освещением
        // false - рисование без освещения
        bool lightningState = true;

        // Using fonts
        int Font3D = 0;

        // Texture
        // uint Texture = LoadTexture(@"D:\Programing\C#\OpenGl\Practive2\OpenGlForm\OpenGlForm\Grani2.bmp");
        uint Texture = 0;

        public Form1()
        {
            InitializeComponent();

            // Use Form to draw in it
            Handle3D = Handle;
            HDC3D = User.GetDC(Handle3D);
            Gdi.PIXELFORMATDESCRIPTOR PFD = new();
            PFD.nVersion = 1;
            PFD.nSize = (short)Marshal.SizeOf(PFD);
            PFD.dwFlags =
                Gdi.PFD_DRAW_TO_WINDOW |
                Gdi.PFD_SUPPORT_OPENGL |
                Gdi.PFD_DOUBLEBUFFER;
            PFD.iPixelType = Gdi.PFD_TYPE_RGBA;
            PFD.cColorBits = 24;
            PFD.cDepthBits = 32;
            PFD.iLayerType = Gdi.PFD_MAIN_PLANE;

            int nPixelFormat = Gdi.ChoosePixelFormat(HDC3D, ref PFD);
            Gdi.SetPixelFormat(HDC3D, nPixelFormat, ref PFD);

            HRC3D = Wgl.wglCreateContext(HDC3D);
            Wgl.wglMakeCurrent(HDC3D, HRC3D);

            Gl.glEnable(Gl.GL_DEPTH_TEST);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            CreateFont3D(Font);

            Texture = LoadTexture("Grani2.bmp");
            Form1_Resize(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        static uint LoadTexture(string filename)
        {
            uint texObject = 0;

            try
            {
                Bitmap bmp = new(filename);
                bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

                BitmapData bmpdata = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

                texObject = MakeGlTexture(bmpdata.Scan0, bmp.Width, bmp.Height);
                bmp.UnlockBits(bmpdata);
            } catch (Exception e) 
            { 
                Console.WriteLine(e.Message); 
            }
            return texObject;
        }

        static uint MakeGlTexture(IntPtr pixels, int w, int h)
        {
            // Identificator of texture object
            uint texObject = 0;

            // Generating text object
            Gl.glGenTextures(1 /*Objects number*/, out texObject);
            // Setting pixel packing mode
            Gl.glPixelStorei(Gl.GL_UNPACK_ALIGNMENT, 1);
            // Binding to created texture
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, texObject);
            // Setting texture filtration mode
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_NEAREST);
            Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_NEAREST);

            Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, Gl.GL_RGB, w, h, 0, Gl.GL_BGR, Gl.GL_UNSIGNED_BYTE, pixels);

            return texObject;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();

            // Properties of the WinForm
            int w = ClientRectangle.Width - TrackPanel.Width;
            int h = ClientRectangle.Height;

            Glu.gluPerspective(30, (double)w / h, 2, 20000);
            Gl.glViewport(0, 0, w, h);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Gl.glClearColor(1.0f, 1.0f, 1.0f, 1); // Dummy zeros
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);

            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
            Gl.glTranslatef(0, 0, -r);
            Gl.glRotatef(phi, 1f, 0, 0);
            Gl.glRotatef(psi, 0, 1f, 0);

            DrawScene(); // Dummy

            Gl.glFinish();
            Gdi.SwapBuffers(HDC3D);
        }

        private void DrawScene()
        {
            if (lightningState)
            {
                // ----------- Включаем освещенность объектов -----------
                Gl.glEnable(Gl.GL_LIGHTING);
                Gl.glEnable(Gl.GL_LIGHT0);
                // ------------------------------------------------------

                // ----------- Включаем коррекцию нормалей для рисования с освещением -----------
                Gl.glEnable(Gl.GL_NORMALIZE);
                // ----------- Олучшаем собсвтенный цвет объектов в освещении -----------
                Gl.glEnable(Gl.GL_COLOR_MATERIAL);
                // ----------- Модель освещения -----------
                Gl.glLightModeli(Gl.GL_LIGHT_MODEL_TWO_SIDE, 1);
                // -----------------------------------------------------------------------------
            }

            // ----------- Начало координат -----------
            Gl.glPointSize(10);
            Gl.glEnable(Gl.GL_POINT_SMOOTH);

            Gl.glBegin(Gl.GL_POINTS);
            Gl.glColor3f(0.0f, 0.0f, 0.0f);
            Gl.glVertex3d(0.0, 0.0, 0.0);
            Gl.glEnd();
            // ----------------------------------------

            //// ------------ Оси координат -------------
            Gl.glLineWidth(1);
            Gl.glBegin(Gl.GL_LINES);
            Gl.glColor3f(1, 0, 0);
            Gl.glVertex3d(0, 0, 0); // Ось х
            Gl.glVertex3d(2, 0, 0);

            Gl.glColor3f(0, 1, 0);
            Gl.glVertex3d(0, 0, 0); // Ось у
            Gl.glVertex3d(0, 2, 0);

            Gl.glColor3f(0, 0, 1);
            Gl.glVertex3d(0, 0, 0); // Ось z
            Gl.glVertex3d(0, 0, 2);
            Gl.glEnd();

            outText3D(2, 0, 0, "x");
            outText3D(0, 2, 0, "y");
            outText3D(0, 0, 2, "z");
            //// ----------------------------------------



            //// ----------- Пунктирная линия из центар ккординат -----------
            //Gl.glLineWidth(1);
            //Gl.glEnable(Gl.GL_LINE_STIPPLE); // Пунктирная линия
            //Gl.glLineStipple(1, 0x00CC);

            //Gl.glBegin(Gl.GL_LINES);
            //Gl.glColor3f(0.5f, 0.5f, 0.5f);
            //Gl.glVertex3d(0, 0, 0);
            //Gl.glVertex3d(1, 1, 1);
            //Gl.glEnd();
            //Gl.glDisable(Gl.GL_LINE_STIPPLE);
            //Gl.glDisable(Gl.GL_POINT_SMOOTH);
            //// ------------------------------------------------------------


            //// ----------- Рисуем огранку куба -----------
            if (lightningState)
            {
                Gl.glLineWidth(1);
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_LINE);
                SetGlColor(Color.Black);
                Gl.glEnable(Gl.GL_POLYGON_OFFSET_FILL);
                Gl.glPolygonOffset(1.0f, 1.0f);
                DrawCube();
            }

            //// ----------- Рисуем стороны куба -----------
            if (lightningState)
            {
                Gl.glPolygonMode(Gl.GL_FRONT_AND_BACK, Gl.GL_FILL);
                SetGlColor(Color.LightGray);
                DrawCube();
                Gl.glDisable(Gl.GL_BLEND);

                Gl.glDisable(Gl.GL_POLYGON_OFFSET_FILL);
            }
            //// -------------------------------------------
            //// -------------------------------------------

            if (lightningState)
            {
                // ----------- Отключение освещения -----------
                Gl.glDisable(Gl.GL_LIGHT0);
                Gl.glDisable(Gl.GL_LIGHTING);
                Gl.glDisable(Gl.GL_COLOR_MATERIAL);
                // --------------------------------------------
            }


            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glTexEnvf(Gl.GL_TEXTURE_ENV, Gl.GL_TEXTURE_ENV_MODE, Gl.GL_DECAL);
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, Texture);
            Gl.glBegin(Gl.GL_QUADS);
            // +Z face
            Gl.glNormal3f(0.0f, 0.0f, 1.0f);
            Gl.glTexCoord2f(0, 0);
            Gl.glVertex3f(-1.0f, -1.0f, 1.0f);
            Gl.glTexCoord2f(0.125f, 0);
            Gl.glVertex3f(1.0f, -1.0f, 1.0f);
            Gl.glTexCoord2f(0.125f, 1f);
            Gl.glVertex3f(1.0f, 1.0f, 1.0f);
            Gl.glTexCoord2f(0, 1.0f);
            Gl.glVertex3f(-1.0f, 1.0f, 1.0f);
            // +X face
            Gl.glNormal3f(1.0f, 0.0f, 0.0f);
            Gl.glTexCoord2f(0.125f, 0);
            Gl.glVertex3f(1.0f, -1.0f, 1.0f);
            Gl.glTexCoord2f(0.250f, 0);
            Gl.glVertex3f(1.0f, -1.0f, -1.0f);
            Gl.glTexCoord2f(0.250f, 1f);
            Gl.glVertex3f(1.0f, 1.0f, -1.0f);
            Gl.glTexCoord2f(0.125f, 1.0f);
            Gl.glVertex3f(1.0f, 1.0f, 1.0f);
            // +Y face
            Gl.glNormal3f(0.0f, 1.0f, 0.0f);
            Gl.glTexCoord2f(0.250f, 0);
            Gl.glVertex3f(1.0f, 1.0f, 1.0f);
            Gl.glTexCoord2f(0.375f, 0);
            Gl.glVertex3f(1.0f, 1.0f, -1.0f);
            Gl.glTexCoord2f(0.375f, 1f);
            Gl.glVertex3f(-1.0f, 1.0f, -1.0f);
            Gl.glTexCoord2f(0.250f, 1.0f);
            Gl.glVertex3f(-1.0f, 1.0f, 1.0f);
            // -Z face
            Gl.glNormal3f(0.0f, 0.0f, -1.0f);
            Gl.glTexCoord2f(0.375f, 0);
            Gl.glVertex3f(1.0f, -1.0f, -1.0f);
            Gl.glTexCoord2f(0.500f, 0);
            Gl.glVertex3f(-1.0f, -1.0f, -1.0f);
            Gl.glTexCoord2f(0.500f, 1f);
            Gl.glVertex3f(-1.0f, 1.0f, -1.0f);
            Gl.glTexCoord2f(0.375f, 1.0f);
            Gl.glVertex3f(1.0f, 1.0f, -1.0f);
            // -X face
            Gl.glNormal3f(-1.0f, 0.0f, 0.0f);
            Gl.glTexCoord2f(0.500f, 0);
            Gl.glVertex3f(-1.0f, -1.0f, -1.0f);
            Gl.glTexCoord2f(0.625f, 0);
            Gl.glVertex3f(-1.0f, -1.0f, 1.0f);
            Gl.glTexCoord2f(0.625f, 1f);
            Gl.glVertex3f(-1.0f, 1.0f, 1.0f);
            Gl.glTexCoord2f(0.500f, 1.0f);
            Gl.glVertex3f(-1.0f, 1.0f, -1.0f);
            // -Y face
            Gl.glNormal3f(0.0f, -1.0f, 0.0f);
            Gl.glTexCoord2f(0.625f, 0);
            Gl.glVertex3f(-1.0f, -1.0f, -1.0f);
            Gl.glTexCoord2f(0.750f, 0);
            Gl.glVertex3f(1.0f, -1.0f, -1.0f);
            Gl.glTexCoord2f(0.750f, 1f);
            Gl.glVertex3f(1.0f, -1.0f, 1.0f);
            Gl.glTexCoord2f(0.625f, 1.0f);
            Gl.glVertex3f(-1.0f, -1.0f, 1.0f);

            Gl.glEnd();
            Gl.glDisable(Gl.GL_TEXTURE_2D);
            //// Enable repeat 
            ////Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_S, Gl.GL_REPEAT);
            ////Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_WRAP_T, Gl.GL_REPEAT);
        }

        private void DrawCube()
        {

            Gl.glBegin(Gl.GL_QUADS);
            Gl.glNormal3f(0.0f, 0.0f, 1.0f);
            Gl.glVertex3d(-1, -1, 1);
            Gl.glVertex3d(1, -1, 1);
            Gl.glVertex3d(1, 1, 1);
            Gl.glVertex3d(-1, 1, 1);

            Gl.glNormal3f(0.0f, 0.0f, -1.0f);
            Gl.glVertex3d(1, -1, -1);
            Gl.glVertex3d(-1, -1, -1);
            Gl.glVertex3d(-1, 1, -1);
            Gl.glVertex3d(1, 1, -1);

            Gl.glNormal3f(1.0f, 0.0f, 0.0f);
            Gl.glVertex3d(1, -1, 1);
            Gl.glVertex3d(1, -1, -1);
            Gl.glVertex3d(1, 1, -1);
            Gl.glVertex3d(1, 1, 1);

            Gl.glNormal3f(-1.0f, 0.0f, 0.0f);
            Gl.glVertex3d(-1, -1, -1);
            Gl.glVertex3d(-1, -1, 1);
            Gl.glVertex3d(-1, 1, 1);
            Gl.glVertex3d(-1, 1, -1);

            Gl.glNormal3f(0.0f, 1.0f, 0.0f);
            Gl.glVertex3d(-1, 1, 1);
            Gl.glVertex3d(1, 1, 1);
            Gl.glVertex3d(1, 1, -1);
            Gl.glVertex3d(-1, 1, -1);

            Gl.glNormal3f(0.0f, -1.0f, 0.0f);
            Gl.glVertex3d(1, -1, 1);
            Gl.glVertex3d(-1, -1, 1);
            Gl.glVertex3d(-1, -1, -1);
            Gl.glVertex3d(1, -1, -1);
            Gl.glEnd();
        }

        private void SetGlColor(Color color)
        {
            Gl.glColor3ub(color.R, color.G, color.B);
        }

        void CreateFont3D(Font font)
        {
            Gdi.SelectObject(HDC3D, font.ToHfont());
            Font3D = Gl.glGenLists(256);

            Wgl.wglUseFontBitmapsA(HDC3D, 0, 256, Font3D);
        }

        void outText3D(float x, float y, float z, string text)
        {
            Gl.glRasterPos3f(x, y, z);
            Gl.glPushAttrib(Gl.GL_LIST_BIT);
            Gl.glListBase(Font3D);
            byte[] bText = MyGl.RussianEncoding.GetBytes(text);

            Gl.glCallLists(text.Length, Gl.GL_UNSIGNED_BYTE, bText);
            Gl.glPopAttrib();
        }

        void DeleteFont3D()
        {
            if (Font3D != 0)
            {
                Gl.glDeleteLists(Font3D, 256);
            }
        }

        private void SetGlColor(Color color, Byte Alpha)
        {
            Gl.glEnable(Gl.GL_BLEND);

            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);

            Gl.glColor4ub(color.R, color.G, color.B, Alpha);
        }

        private void InvalidateRect()
        {
            MyGl.InvalidateRect(Handle, IntPtr.Zero, false);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Wgl.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
            Wgl.wglDeleteContext(HRC3D);
            User.ReleaseDC(Handle3D, HDC3D);
            DeleteFont3D();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == MyGl.WM_ERASEBKGND)
            {
                InvalidateRect();
            }
        }

        private void TrackBarPsi_Scroll(object sender, EventArgs e)
        {
            this.psi = TrackBarPsi.Value;
            InvalidateRect();
        }

        private void TrackBarPhi_Scroll(object sender, EventArgs e)
        {
            this.phi = TrackBarPhi.Value;
            InvalidateRect();
        }

        private void TrackBarR_Scroll(object sender, EventArgs e)
        {
            this.r = TrackBarR.Value / 10.0f;
            InvalidateRect();
        }
    }
}
