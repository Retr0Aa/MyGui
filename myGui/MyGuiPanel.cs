using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGui
{
    class MyGuiPanel
    {
        private Panel panel;
        private Label label;

        private string m_Title;
        private Point m_Position;
        private Size m_Size;
        //private Panel m_HoveredPanelDock;
        private Form m_Form;

        private DockStyle currentDock;

        private Control activeControl;
        private Point previousLocation;

        private List<ToolStripItem> dockPlaces = new List<ToolStripItem>();

        private Panel contentPanel;
        private Panel topPanel;

        private ToolStrip stripMenu;
        private ToolStripDropDownButton dockDropdown;

        private bool canMove = true;

        public void addControl(Control control)
        {
            contentPanel.Controls.Add(control);
        }

        public MyGuiPanel(string title, Size size, DockStyle startDock, Form form)
        {
            m_Title = title;
            m_Position = new Point(0, 0);
            m_Size = size;
            m_Form = form;
            currentDock = startDock;

            /*dockPlaces = new Panel[5] {
                new Panel() {
                    Dock = DockStyle.Top,
                    BackColor = Color.Gray,
                    Margin = new Padding(10, 10, 10, 10),
                    Padding = new Padding(10, 10, 10, 10)
                },
                new Panel() {
                    Dock = DockStyle.Right,
                    BackColor = Color.Red,
                    Margin = new Padding(10, 10, 10, 10),
                    Padding = new Padding(10, 10, 10, 10)
                },
                new Panel() {
                    Dock = DockStyle.Left,
                    BackColor = Color.Blue,
                    Margin = new Padding(10, 10, 10, 10),
                    Padding = new Padding(10, 10, 10, 10)
                },
                new Panel() {
                    //Location = new Point(m_Form.Size.Width / 2, (m_Form.Size.Height / 2) - 5),
                    Dock = DockStyle.Bottom,
                    BackColor = Color.Yellow,
                    Margin = new Padding(10, 10, 10, 10),
                    Padding = new Padding(10, 10, 10, 10)
                },
                new Panel() {
                    //Location = new Point(m_Form.Size.Width / 2, m_Form.Size.Height / 2),
                    Dock = DockStyle.Fill,
                    BackColor = Color.Green,
                    Size = new Size(30, 30),
                    Margin = new Padding(10, 10, 10, 10),
                    Padding = new Padding(10, 10, 10, 10)
                },
            };*/
        }

        public void SetDock(DockStyle newDock)
        {
            currentDock = newDock;
            panel.Dock = newDock;
        }

        public void Init()
        {
            panel = new Panel() {
                Size = m_Size,
                Location = m_Position,
                Dock = currentDock,

                BackColor = Color.LightGray
            };
            label = new Label() {
                Dock = DockStyle.Fill,
                Font = new Font("Verdana", 13),
                ForeColor = Color.White,
                Text = m_Title
            };
            topPanel = new Panel() {
                Dock = DockStyle.Top,
                Size = new Size(10, 20),
                BackColor = Color.Gray
            };

            stripMenu = new ToolStrip() {
                Dock = DockStyle.Right,
                Text = "Dock",
                Size = new Size(20, 20),
                LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow,
                GripStyle = ToolStripGripStyle.Hidden,
                BackColor = Color.Gray
            };

            dockDropdown = new ToolStripDropDownButton() {
                Dock = DockStyle.Right,
                Text = "Dock",
                Size = new Size(20, 20),
                ForeColor = Color.White,
                ShowDropDownArrow = false
            };

            contentPanel = new Panel() {
                Dock = DockStyle.Fill
            };

            panel.Controls.Add(contentPanel);
            panel.Controls.Add(topPanel);

            topPanel.Controls.Add(label);
            topPanel.Controls.Add(stripMenu);

            stripMenu.Items.Add(dockDropdown);

            dockPlaces.Add(dockDropdown.DropDownItems.Add("None"));
            dockPlaces.Add(dockDropdown.DropDownItems.Add("Top"));
            dockPlaces.Add(dockDropdown.DropDownItems.Add("Bottom"));
            dockPlaces.Add(dockDropdown.DropDownItems.Add("Left"));
            dockPlaces.Add(dockDropdown.DropDownItems.Add("Right"));
            dockPlaces.Add(dockDropdown.DropDownItems.Add("Fill"));

            foreach (ToolStripItem item in dockPlaces)
            {
                item.BackColor = Color.Gray;
                item.ForeColor = Color.White;
                item.Click += OnDockItemClick;
            }

            /*dockPlaces[0].MouseEnter += OnDockMouseEnter;
            dockPlaces[1].MouseEnter += OnDockMouseEnter;
            dockPlaces[2].MouseEnter += OnDockMouseEnter;
            dockPlaces[3].MouseEnter += OnDockMouseEnter;
            dockPlaces[4].MouseEnter += OnDockMouseEnter;*/

            label.MouseDown += OnMouseDown;
            label.MouseMove += OnMouseMove;
            label.MouseUp += OnMouseUp;

            //panel.Draggable(true);

            m_Form.Controls.Add(panel);

            /*foreach (Panel item in dockPlaces)
            {
                m_Form.Controls.Add(item);
                item.Visible = false;

                //item.MouseUp += OnDockMouseUp;

                Console.WriteLine("Workmi!");
            }
            dockPlaces[0].MouseUp += OnDockMouseUp;
            dockPlaces[1].MouseUp += OnDockMouseUp;
            dockPlaces[2].MouseUp += OnDockMouseUp;
            dockPlaces[3].MouseUp += OnDockMouseUp;
            dockPlaces[4].MouseUp += OnDockMouseUp;*/
        }

        private void OnDockItemClick(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Text == "None")
            {
                SetDock(DockStyle.None);

            } else if (((ToolStripMenuItem)sender).Text == "Top")
            {
                SetDock(DockStyle.Top);

            } else if (((ToolStripMenuItem)sender).Text == "Bottom")
            {
                SetDock(DockStyle.Bottom);

            } else if (((ToolStripMenuItem)sender).Text == "Left")
            {
                SetDock(DockStyle.Left);

            } else if (((ToolStripMenuItem)sender).Text == "Right")
            {
                SetDock(DockStyle.Right);

            } else if (((ToolStripMenuItem)sender).Text == "Fill")
            {
                SetDock(DockStyle.Fill);
            }

            canMove = false;

        }

        /*private void OnDockMouseMove(object sender, MouseEventArgs e)
        {
            m_HoveredPanelDock = (Panel)sender;
        }
        
        private void OnDockMouseEnter(object sender, EventArgs e)
        {
            m_HoveredPanelDock = (Panel)sender;
        }

        private void OnDockMouseUp(object sender, MouseEventArgs e)
        {
            Console.WriteLine("Working!");
            SetDock(m_HoveredPanelDock.Dock);
            panel.Draggable(false);
        }*/

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            /*foreach (Panel item in dockPlaces)
            {
                item.Visible = false;
            }

            if (panel.Dock != DockStyle.None)
            {
                panel.Draggable(false);
            }

            var back1 = panel.GetNextControl((Control)sender, false);
            var back2 = back1.GetNextControl(back1, false);

            SetDock(panel.GetNextControl((Control)sender, false).Dock);
            Console.WriteLine(back2);*/

            activeControl = null;
            Cursor.Current = Cursors.Default;
        }

        // Dragging
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            activeControl = panel as Control;
            previousLocation = e.Location;
            Cursor.Current = Cursors.SizeAll;

            if (!canMove)
            {
                canMove = true;
                panel.Dock = DockStyle.None;
            }

            /*dockPlaces[0].Visible = true;
            dockPlaces[1].Visible = true;
            dockPlaces[2].Visible = true;
            dockPlaces[3].Visible = true;
            dockPlaces[4].Visible = true;*/
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (activeControl == null)
                return;

            if (canMove)
            {
            var location = activeControl.Location;
            location.Offset(e.Location.X - previousLocation.X, e.Location.Y - previousLocation.Y);
            activeControl.Location = location;
            }

            //dockPlaces[0].Visible = true;
            //dockPlaces[1].Visible = true;
            //dockPlaces[2].Visible = true;
            //dockPlaces[3].Visible = true;
            //dockPlaces[4].Visible = true;
        }
    }
}
