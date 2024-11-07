using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tringle
{
    public partial class Form2 : Form
    {
        private Triangle triangle;
        private Point[] trianglePoints;
        private Panel drawingPanel;

        public Form2()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 450);
            this.Text = "Form1";

            InitializeCustomComponents();
            Side();

            triangle = new Triangle(3, 4, 5);
            DisplayTriangleInfo();
            CalculateTrianglePoints();
        }

        private void InitializeCustomComponents()
        {
            drawingPanel = new Panel
            {
                Size = new Size(400, 250),
                Location = new Point(10, 10),
                BackColor = Color.White
            };
            drawingPanel.Paint += DrawingPanel_Paint;
            Controls.Add(drawingPanel);

            Button startButton = new Button
            {
                Text = "Alusta",
                Font = new Font("Arial", 28, FontStyle.Regular),
                Size = new Size(150, 80),
                BackColor = Color.LightSeaGreen,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(515, 350),
                Cursor = Cursors.Hand
            };

            startButton.Click += StartButton_Click;

            Controls.Add(startButton);
        }

        private void DisplayTriangleInfo()
        {
            DataGridView dataGridView = new DataGridView
            {
                ColumnCount = 2,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                ColumnHeadersVisible = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells,
                ScrollBars = ScrollBars.None,
                BackgroundColor = Color.White
            };

            dataGridView.Size = new Size(400, 250);
            dataGridView.Location = new Point(420, 10);

            dataGridView.Columns[0].Name = "Väli";
            dataGridView.Columns[1].Name = "Väärtus";
            dataGridView.DefaultCellStyle.Padding = new Padding(5);

            DataGridViewCellStyle headerStyle = new DataGridViewCellStyle
            {
                Font = new Font("Arial", 10, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.MediumPurple,
                Alignment = DataGridViewContentAlignment.MiddleCenter
            };
            dataGridView.ColumnHeadersDefaultCellStyle = headerStyle;
            dataGridView.ColumnHeadersHeight = 40;

            dataGridView.Rows.Add("Küljepikkus a", " ");
            dataGridView.Rows.Add("Küljepikkus b", " ");
            dataGridView.Rows.Add("Küljepikkus c", " ");
            dataGridView.Rows.Add("Perimeeter", " ");
            dataGridView.Rows.Add("Pindala", " ");
            dataGridView.Rows.Add("Eksisteerib?", " ");
            dataGridView.Rows.Add("Tüüp", "");

            Controls.Add(dataGridView);
        }

        private void Side()
        {
            Label labelA = new Label
            {
                Text = "Külg A:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                Location = new Point(10, 270),
                AutoSize = true
            };
            Controls.Add(labelA);

            TextBox textBoxA = new TextBox
            {
                Name = "textBoxA",
                Font = new Font("Arial", 12, FontStyle.Regular),
                Location = new Point(150, 270),
                Size = new Size(100, 30)
            };
            Controls.Add(textBoxA);

            Label labelB = new Label
            {
                Text = "Külg B:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                Location = new Point(10, 310),
                AutoSize = true
            };
            Controls.Add(labelB);

            TextBox textBoxB = new TextBox
            {
                Name = "textBoxB",
                Font = new Font("Arial", 12, FontStyle.Regular),
                Location = new Point(150, 310),
                Size = new Size(100, 30)
            };
            Controls.Add(textBoxB);

            Label labelC = new Label
            {
                Text = "Külg C:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                ForeColor = Color.DarkOrange,
                Location = new Point(10, 350),
                AutoSize = true
            };
            Controls.Add(labelC);

            TextBox textBoxC = new TextBox
            {
                Name = "textBoxC",
                Font = new Font("Arial", 12, FontStyle.Regular),
                Location = new Point(150, 350),
                Size = new Size(100, 30)
            };
            Controls.Add(textBoxC);
        }

        private void StartButton_Click(object? sender, EventArgs e)
        {
            double a, b, c;

            if (double.TryParse(Controls["textBoxA"].Text, out a) &&
                double.TryParse(Controls["textBoxB"].Text, out b) &&
                double.TryParse(Controls["textBoxC"].Text, out c))
            {
                triangle = new Triangle(a, b, c);
                CalculateTrianglePoints();

                Controls.OfType<DataGridView>().FirstOrDefault()?.Rows.Clear();

                DataGridView dataGridView = Controls.OfType<DataGridView>().FirstOrDefault();
                if (dataGridView != null)
                {
                    dataGridView.Rows.Add("Küljepikkus a", triangle.GetSetA);
                    dataGridView.Rows.Add("Küljepikkus b", triangle.GetSetB);
                    dataGridView.Rows.Add("Küljepikkus c", triangle.GetSetC);
                    dataGridView.Rows.Add("Perimeeter", triangle.Perimeter());
                    dataGridView.Rows.Add("Pindala", triangle.Area());
                    dataGridView.Rows.Add("Eksisteerib", triangle.ExistTriangle ? "Eksisteerib" : "Ei eksisteeri");
                    dataGridView.Rows.Add("Tüüp", triangle.TriangleType);
                }

                drawingPanel.Invalidate();
            }
            else
            {
                MessageBox.Show("Palun sisestage külgede jaoks kehtivad väärtused.");
            }
        }

        private void DrawingPanel_Paint(object sender, PaintEventArgs e)
        {
            if (trianglePoints != null && trianglePoints.Length == 3)
            {
                using Pen pen = new(Color.Red, 3);
                e.Graphics.DrawPolygon(pen, trianglePoints);
            }
        }

        private void CalculateTrianglePoints()
        {
            if (triangle != null)
            {
                double a = triangle.GetSetA;
                double b = triangle.GetSetB;
                double c = triangle.GetSetC;

                // Check if the triangle is valid
                if (a + b <= c || a + c <= b || b + c <= a)
                {
                    trianglePoints = null; // Set to null to prevent drawing
                    return; // Exit if the triangle is invalid
                }

                double s = (a + b + c) / 2;
                double area = Math.Sqrt(s * (s - a) * (s - b) * (s - c));
                double height = (2 * area) / a;

                // Determine the maximum height and base lengths that fit 80% within the panel
                int panelWidth = (int)(drawingPanel.Width * 0.4);
                int panelHeight = (int)(drawingPanel.Height * 0.4);

                // Scale factors to fit within 80% of the drawing panel
                double scaleX = Math.Min(panelWidth / (a * 20), panelWidth / (b * 20));
                double scaleY = Math.Min(panelHeight / (height * 20), panelHeight / (height * 20));
                double scale = Math.Min(scaleX, scaleY);

                // Calculate the center position for the triangle
                int centerX = drawingPanel.Location.X + drawingPanel.Width / 2;
                int centerY = drawingPanel.Location.Y + drawingPanel.Height / 2; // Center vertically

                // Define triangle points scaled to fit within the panel and centered
                trianglePoints = new Point[3];
                trianglePoints[0] = new Point(centerX, centerY - (int)(height * 20 * scale)); // Top point
                trianglePoints[1] = new Point(centerX - (int)(b * 20 * scale / 2), centerY + (int)(height * 20 * scale / 2)); // Bottom left
                trianglePoints[2] = new Point(centerX + (int)(a * 20 * scale / 2), centerY + (int)(height * 20 * scale / 2)); // Bottom right
            }
        }
    }
}