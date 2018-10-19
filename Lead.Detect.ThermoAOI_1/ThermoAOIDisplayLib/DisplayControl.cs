using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Lead.Detect.FrameworkExtension.platforms.motionPlatforms;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Core;
using SharpGL.SceneGraph.Primitives;

namespace Lead.Detect.ThermoAOIDisplayLib
{
    public partial class DisplayControl : UserControl
    {
        public DisplayControl()
        {
            InitializeComponent();

            openGLControl1.MouseWheel += OnMouseWheel;
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            var gl = openGLControl1.OpenGL;
            gl.Viewport(0, 0, openGLControl1.Width, openGLControl1.Height);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(90, (float)openGLControl1.Width / openGLControl1.Height, 1, 1000);


            gl.LookAt(-200, 0, 50, 0, 0, 0, 0, 0, 1);


            gl.ShadeModel(OpenGL.GL_SMOOTH);
            var mat_specular = new[] { 1.0f, 1.0f, 1.0f, 1.0f };
            var mat_ambient = new[] { 0.2f, 0.2f, 0.2f, 0.2f };
            var mat_diffuse = new[] { 2.0f, 2.0f, 2.0f, 0.1f };
            var mat_shininess = new[] { 100.0f };
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SPECULAR, mat_specular);
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_AMBIENT, mat_ambient);
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_DIFFUSE, mat_diffuse);
            gl.Material(OpenGL.GL_FRONT, OpenGL.GL_SHININESS, mat_shininess);

            var ambientLight = new[] { 0.2f, 0.2f, 0.2f, 0.2f };
            var diffuseLight = new[] { 1.0f, 1.0f, 1.0f, 1.0f };
            var posLight0 = new[] { -200.0f, 200.0f, 200f, 1.0f };
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_AMBIENT, ambientLight);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_DIFFUSE, diffuseLight);
            gl.Light(OpenGL.GL_LIGHT0, OpenGL.GL_POSITION, posLight0);

            gl.Enable(OpenGL.GL_LIGHTING);
            gl.Enable(OpenGL.GL_LIGHT0);
            gl.Enable(OpenGL.GL_COLOR_MATERIAL);
            gl.Enable(OpenGL.GL_DEPTH_TEST);
            gl.Enable(OpenGL.GL_DOUBLEBUFFER);

            ArcBall.SetBounds(openGLControl1.Width, openGLControl1.Height);
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            var gl = openGLControl1.OpenGL;

            gl.ClearColor(0.2f, 0.4f, 0.2f, 0.2f);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);


            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(90, (float)openGLControl1.Width / openGLControl1.Height, 1, 1000);
            gl.LookAt(vx, vy, vz, 0, 0, 0, upx, upy, upz);
            ArcBall.TransformMatrix(gl);

            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.LoadIdentity();

            DrawAxis(gl);
            DrawPos(gl);


            gl.Finish();
            gl.Flush();
        }

        private void openGLControl1_Resized(object sender, EventArgs e)
        {
            var gl = openGLControl1.OpenGL;
            gl.Viewport(0, 0, openGLControl1.Width, openGLControl1.Height);

            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Perspective(90, (float)openGLControl1.Width / openGLControl1.Height, 1, 1000);
            gl.LookAt(vx, vy, vz, 0, 0, 0, upx, upy, upz);



        }

        public void SetLight(float x, float y, float z)
        {
            openGLControl1.OpenGL.Light(LightName.Light0, LightParameter.Position, new[] { x, y, z });
        }

        public float vx { get; set; } = 300;
        public float vy { get; set; } = 300;
        public float vz { get; set; } = 50;
        public float upx { get; set; }
        public float upy { get; set; }
        public float upz { get; set; } = 1;

        public void SetView(float x, float y, float z, float ux = 0f, float uy = 0f, float uz = 1f)
        {
            vx = x;
            vy = y;
            vz = z;

            upx = ux;
            upy = uy;
            upz = uz;
        }

        /// <summary>
        /// The axies, which may be drawn.
        /// </summary>
        private readonly Axies axies = new Axies();

        public void DrawAxis(OpenGL gl, int axisLen = 80, float axisWidth = 1, float arrow = 0.1f, float arrowWidth = 0.05f)
        {
            //axies.Render(gl, RenderMode.Design);

            gl.ColorMaterial(OpenGL.GL_FRONT, OpenGL.GL_AMBIENT_AND_DIFFUSE);

            var obj = gl.NewQuadric();

            gl.QuadricNormals(obj, OpenGL.GLU_SMOOTH);
            gl.QuadricDrawStyle(obj, OpenGL.GLU_FILL);


            gl.PushMatrix();
            gl.Translate(0, 0, 0);


            gl.Color(1.0f, 1.0f, 1.0f);
            gl.Sphere(obj, axisLen / 10f, 16, 16);

            //Z
            gl.PushMatrix();
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Cylinder(obj, axisWidth, axisWidth, axisLen, 10, 5);
            gl.Translate(0, 0, axisLen);
            gl.Cylinder(obj, arrowWidth * axisLen, 0, arrow * axisLen, 10, 5);
            gl.PopMatrix();

            //Y
            gl.PushMatrix();
            gl.Rotate(-90, 1.0, 0.0, 0.0);
            gl.Color(0.0f, 1.0f, 0f);
            gl.Cylinder(obj, axisWidth, axisWidth, axisLen, 10, 5);
            gl.Translate(0, 0, axisLen);
            gl.Cylinder(obj, arrowWidth * axisLen, 0, arrow * axisLen, 10, 5);
            gl.PopMatrix();

            //X
            gl.PushMatrix();
            gl.Rotate(90, 0.0, 1.0, 0.0);
            gl.Color(1.0f, 0f, 0f);
            gl.Cylinder(obj, axisWidth, axisWidth, axisLen, 10, 5);
            gl.Translate(0, 0, axisLen);
            gl.Cylinder(obj, arrowWidth * axisLen, 0, arrow * axisLen, 10, 5);
            gl.PopMatrix();

            gl.PopMatrix();

            gl.DeleteQuadric(obj);
        }


        public void DrawPos(OpenGL gl)
        {
            gl.ColorMaterial(OpenGL.GL_FRONT, OpenGL.GL_AMBIENT_AND_DIFFUSE);

            var obj = gl.NewQuadric();

            gl.QuadricNormals(obj, OpenGL.GLU_SMOOTH);
            gl.QuadricDrawStyle(obj, OpenGL.GLU_FILL);
            gl.Enable(OpenGL.GLU_POINT);
            gl.Enable(OpenGL.GL_POINT_SMOOTH);
            gl.Color(1.0f, 1.0f, 1.0f);

            gl.PushMatrix();

            gl.PointSize(10f);
            gl.Begin(BeginMode.Points);
            foreach (var p in _posDisplay)
            {
                if (p.Status)
                {
                    gl.Color(1.0, 1.0, 1.0);
                    gl.Vertex(p.X, p.Y, p.Z);
                }
                else
                {
                    gl.Color(1.0, 0, 0);
                    gl.Vertex(p.X, p.Y, p.Z);
                }
            }

            gl.End();

            gl.PopMatrix();
        }

        public void UpdatePos(List<PosXYZ> pos)
        {
            var center = new PosXYZ(pos.Select(p => p.X).Average(), pos.Select(p => p.Y).Average(), pos.Select(p => p.Z).Average());
            _posDisplay = pos.Select(p => new PosXYZ(p.X - center.X, p.Y - center.Y, p.Z - center.Z) { Status = p.Status }).ToList();
        }

        private List<PosXYZ> _posDisplay = new List<PosXYZ>();


        #region mouse

        public ArcBall ArcBall = new ArcBall();

        private bool _isMouseDown;

        private void openGLControl1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _isMouseDown = true;
                ArcBall.MouseDown(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Right)
            {
                ArcBall = new ArcBall();
            }
        }

        private void openGLControl1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ArcBall.MouseUp(e.X, e.Y);
                _isMouseDown = false;
            }
        }

        private void openGLControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isMouseDown)
            {
                ArcBall.MouseMove(e.X, e.Y);

                if (openGLControl1.RenderTrigger == RenderTrigger.Manual)
                {
                    openGLControl1.DoRender();
                }
            }
        }

        private void openGLControl1_Scroll(object sender, ScrollEventArgs e)
        {
        }

        private void openGLControl1_MouseHover(object sender, EventArgs e)
        {
            openGLControl1.Focus();
        }


        private readonly float ratio = 0.05f;

        private void OnMouseWheel(object sender, MouseEventArgs e)
        {
            float current = 1;
            if (e.Delta < 0)
            {
                if (current > ratio)
                {
                    current -= ratio;

                    vx *= current;
                    vy *= current;
                    vz *= current;

                    openGLControl1.DoRender();
                }
            }
            else
            {
                current += ratio;

                vx *= current;
                vy *= current;
                vz *= current;

                openGLControl1.DoRender();
            }
        }

        #endregion
    }
}